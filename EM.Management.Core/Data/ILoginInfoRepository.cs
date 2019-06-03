using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace EM.Management.Data
{
    public interface ILoginInfoRepository : IRepository
    {
        Task<LoginInfo> Load(string userId, string token);

        Task<bool> Save(LoginInfo loginInfo);
    }
}
