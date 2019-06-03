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

        public string Password { get; set; }

        public DateTime LoginTime { get; set; }

        public string TokenKey { get; set; }


        public string ToToken()
        {
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(TokenKey))
                throw new InvalidOperationException("有空的值，无法生成token");
            var hc = UserId.GetHashCode() * 137 + Password.GetHashCode() * 37 + LoginTime.GetHashCode() * 13 + TokenKey.GetHashCode();
            return CommonUtils.EncryptUtils.DESEnCode(hc.ToString(), TokenKey);
        }
    }
}
