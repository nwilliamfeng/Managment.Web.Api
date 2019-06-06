using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace EM.Management.Service
{
    public interface IAuthService
    {
        Task<JsonResultData<LoginResult>> Login(string userId ,string password);

        Task<JsonResultData<bool>> Logout(string userId, string token);


        Task<JsonResultData<bool>> Validate(string userId, string token);
    }
}
