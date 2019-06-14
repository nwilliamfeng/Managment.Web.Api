using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;
 

namespace EM.Management.Service
{
    public interface IAuthService
    {
        Task<JsonResultData<LoginResult>> Login(string userId ,string password);

        Task<JsonResultData<bool>> Logout(string accessToken);


        Task<JsonResultData<bool>> Validate(string accessToken);
    }
}
