using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management
{
    public class LoginResult
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string AccessToken { get; set; }

        public DateTime LoginTime { get; set; }
    }
}
