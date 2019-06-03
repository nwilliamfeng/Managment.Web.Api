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

        public int Hash { get; set; }
    }
}
