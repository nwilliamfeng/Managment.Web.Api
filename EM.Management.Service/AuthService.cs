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
        private IUserService _userService;

        public AuthService(ILoginInfoRepository loginInfoRepository,IUserService userService)
        {
            this._loginInfoRepository = loginInfoRepository;
            this._userService = userService;
        }

        public async Task<JsonResultData<bool>> Logout(string accessToken)
        {
            try
            {
                var jsr = await this.DecodeAndValidateFromToken(accessToken);

                if (!jsr.Item2)
                    return false.ToJson("非法的令牌，注销失败").SetStatusCode(StatusCodes.TOKEN_INVALID);
                await _loginInfoRepository.Update(jsr.Item1, DateTime.MinValue);
                return true.ToJson();
            }
            catch (TokenExpiredException)
            {
                return false.ToJson("token已过期").SetStatusCode(StatusCodes.TOKEN_EXPIRE);
            }
            catch (SignatureVerificationException)
            {
                return false.ToJson("token无效的签名").SetStatusCode(StatusCodes.TOKEN_INVALID);
            }
            catch (Exception)
            {
                return false.ToJson("非法的token").SetStatusCode(StatusCodes.TOKEN_INVALID);
            }


        }

        

 
        public async Task<JsonResultData<LoginResult>> Login(string userId, string password)
        {
            var jur = await this._userService.Load(userId,password);
            var user = jur.Data;
            if (user == null)
                throw new ArgumentException("不存在的用户名或错误的密码。");

            var time = DateTime.Now;

            var accessToken = new JwtBuilder().WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(SECRET)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(12)
                .ToUnixTimeSeconds())
                .AddClaim("userId", userId)
                .AddClaim("timestamp", time.ToUnixTime())
                .Build();

            await this._loginInfoRepository.Update(userId, time);

            var loginResult = new LoginResult { LoginTime = time, AccessToken = accessToken, UserId = userId, UserName = user.Name };
            return loginResult.ToJsonResultData();
        }

        private async Task<Tuple<string,bool>> DecodeAndValidateFromToken(string accessToken)
        {
            var payload = new JwtBuilder().WithSecret(SECRET).MustVerifySignature().Decode(accessToken);
            var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(payload);
            var userId = dic["userId"].Value<string>();
            var timestamp = dic["timestamp"].Value<int>();
            var oldTimestamp = await this._loginInfoRepository.GetLoginTimeStamp(userId);
            return new Tuple<string,bool>(userId, oldTimestamp == timestamp);
        }

        public async Task<JsonResultData<bool>> Validate( string accessToken)
        {
            try
            {
                var info =await this.DecodeAndValidateFromToken(accessToken);
                 
                if (!info.Item2 )
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
