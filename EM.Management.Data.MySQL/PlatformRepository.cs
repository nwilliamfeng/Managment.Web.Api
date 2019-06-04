using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Data;

namespace EM.Management.Data.MySQL
{
    public class PlatformRepository : IPlatformRepository
    {
        public bool IsCache => false;

        public Task<bool> AddPlatforms(params Platform[] models)
        {
            return null;
        }

        public Task<IEnumerable<Platform>> GetPlatforms()
        {
            return Task.Run<IEnumerable<Platform>>(() =>
            {
                var lst = new List<Platform>();
                lst.Add(new Platform { PlatformID = 0, Name = "全部平台", CreateTime = DateTime.Now, IsEnabled = 1 });
                lst.Add(new Platform { PlatformID = 1, Name = "基金", CreateTime = DateTime.Now, IsEnabled = 1 });
                lst.Add(new Platform { PlatformID = 2, Name = "证券", CreateTime = DateTime.Now, IsEnabled = 1 });
                return lst;
            });
        }

        
    }
}
