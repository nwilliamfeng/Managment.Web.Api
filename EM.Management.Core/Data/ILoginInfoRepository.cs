using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace EM.Management.Data
{
    public interface ILoginInfoRepository : IRepository
    {
        Task<int> GetLoginTimeStamp(string userId);

      

        Task<bool> Update(string userId,DateTime time);
    }
}
