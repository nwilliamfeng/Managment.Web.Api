using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace EM.Management.Data
{
    public interface ILoginInfoService
    {
        Task<JsonResultData<LoginInfo>> Load(string userId, string token);

        Task<JsonResultData<bool>> Save(LoginInfo loginInfo);
    }
}
