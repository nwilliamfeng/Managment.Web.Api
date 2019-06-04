using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Data
{
    public interface IPlatformRepository:IRepository
    {
        Task<IEnumerable<Platform>> GetPlatforms();

        Task<bool> AddPlatforms(params Platform[] models);
 
    }
}
