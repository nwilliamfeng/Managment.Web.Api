using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management
{
    public sealed class StatusCodes
    {
        public const int FAIL = 0;

        public const int SUCCESS = 1;

        public const int ACTION_ERROR = 2;

        public const int TOKEN_EXPIRE = 3;

        public const int TOKEN_NOT_FOUND = 4;

        public const int TOKEN_INVALID = 5;

        public const int ERROR = 99;
    }
}
