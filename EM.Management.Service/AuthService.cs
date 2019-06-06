using EM.Management.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EM.Management.Data.Redis
{
    public class AuthService :  IAuthService
    {
 
        private ILoginInfoRepository _loginInfoRepository;
 
        public AuthService(ILoginInfoRepository loginInfoRepository)
        {
            this._loginInfoRepository = loginInfoRepository;
        }

        public async Task<JsonResultData<bool>> Logout(string userId,string token)
        {
            var loginInfo = await this._loginInfoRepository.Load(userId);
            if (loginInfo == null)
                throw new ArgumentException("不存在的用户。");
            var result =await this.Validate(userId, token);
            if (!result.Data)
                return false.ToJson("无效的token。");
            return true.ToJson();
        }


        public async Task<JsonResultData<LoginResult>> Login(string userId, string password)
        {
            var loginInfo = await this._loginInfoRepository.Load(userId);
            if (loginInfo == null)
                throw new ArgumentException("不存在的用户名。");
            if(loginInfo.Password!=password)
                throw new ArgumentException("错误的密码。");
            loginInfo.LoginTime = DateTime.Now;
            loginInfo.TokenKey = new Random(10).Next(100,9999).ToString();
            await this._loginInfoRepository.Save(loginInfo);
        
            return loginInfo.ToResult().ToJsonResultData();
        }

        public async Task<JsonResultData<bool>> Validate(string userId, string token)
        {
            var result = await this._loginInfoRepository.Validate(userId,token);
            return result.ToJsonResultData();
        }
    }
}
