using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Data
{
    public interface IPlatformRepository
    {
        Task<IEnumerable<PlatformModel>> GetPlatforms();
    }
}
