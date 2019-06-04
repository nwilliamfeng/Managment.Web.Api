using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management
{
    public sealed class LoginInfo
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime LoginTime { get; set; }

        public string TokenKey { get; set; }


        public string GetAccessToken()
        {
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(TokenKey))
                throw new InvalidOperationException("有空的值，无法生成token");
            var hc = UserId.GetHashCode() * 137 + Password.GetHashCode() * 37 + LoginTime.GetHashCode() * 13 + TokenKey.GetHashCode();
            return CommonUtils.SimpleEncryptUtils.EncryptString(hc.ToString(), TokenKey);
        }

        public LoginResult ToResult()
        {
            return new LoginResult { UserId = this.UserId, UserName = UserName, LoginTime = this.LoginTime, AccessToken = this.GetAccessToken() };
        }
    }
}
