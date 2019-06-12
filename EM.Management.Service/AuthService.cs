using EM.Management.Service;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtils;


namespace EM.Management.Data.Redis
{
    public class AuthService : IAuthService
    {
        private const string SECRET = "KVN_20190612";
        private ILoginInfoRepository _loginInfoRepository;

        public AuthService(ILoginInfoRepository loginInfoRepository)
        {
            this._loginInfoRepository = loginInfoRepository;
        }

        public async Task<JsonResultData<bool>> Logout(string accessToken)
        {
            var payload = new JwtBuilder().WithSecret(SECRET).MustVerifySignature().Decode(accessToken);
            var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(payload);
            var userId = dic["userId"].Value<string>();
            var timestamp = dic["timestamp"].Value<int>();
            var loginInfo = await this._loginInfoRepository.Load(userId);
            if (loginInfo == null || loginInfo.UpdateTime.ToUnixTime() != timestamp)
                return false.ToJson("无效的token").SetStatusCode(StatusCodes.TOKEN_INVALID);     
            loginInfo.UpdateTime = DateTime.Now; //重新更新时间
            await this._loginInfoRepository.Save(loginInfo);
            return true.ToJson();
        }


        public async Task<JsonResultData<LoginResult>> Login(string userId, string password)
        {
            var loginInfo = await this._loginInfoRepository.Load(userId); ?修改逻辑，为空时判断
            if (loginInfo == null)
                throw new ArgumentException("不存在的用户名。");
            if (loginInfo.Password != password)
                throw new ArgumentException("错误的密码。");
            loginInfo.UpdateTime = DateTime.Now;

            var accessToken = new JwtBuilder().WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(SECRET)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(12)
                .ToUnixTimeSeconds())
                .AddClaim("userId", userId)
                .AddClaim("timestamp",loginInfo.UpdateTime.ToUnixTime())
                .Build();

            await this._loginInfoRepository.Save(loginInfo);

            var loginResult = new LoginResult { LoginTime=loginInfo.UpdateTime, AccessToken= accessToken, UserId=loginInfo.UserId, UserName=loginInfo.UserName };
            return loginResult.ToJsonResultData();
        }

        public async Task<JsonResultData<bool>> Validate( string accessToken)
        {
            try
            {
                var payload = new JwtBuilder().WithSecret(SECRET).MustVerifySignature().Decode(accessToken);
                var  dic=  Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(payload);
                var userId = dic["userId"].Value<string>();
                var timestamp = dic["timestamp"].Value<int>();
                var loginInfo = await this._loginInfoRepository.Load(userId);
                if (loginInfo == null || loginInfo.UpdateTime.ToUnixTime() != timestamp)
                    return false.ToJson("无效的token").SetStatusCode(StatusCodes.TOKEN_INVALID);           
                return true.ToJson();
            }
            catch (TokenExpiredException)
            {
                return false.ToJson("token已过期").SetStatusCode(StatusCodes.TOKEN_EXPIRE)  ;
            }
            catch (SignatureVerificationException)
            {
                return false.ToJson("token无效的签名").SetStatusCode(StatusCodes.TOKEN_INVALID);
            }
            catch(Exception)
            {
                return false.ToJson("非法的token").SetStatusCode(StatusCodes.TOKEN_INVALID);
            }
        }
    }
}
