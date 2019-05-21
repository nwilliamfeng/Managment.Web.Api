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

        public Task<IEnumerable<PlatformModel>> GetPlatforms()
        {
            return Task.Run<IEnumerable<PlatformModel>>(() =>
            {
                var lst = new List<PlatformModel>();
                lst.Add(new PlatformModel { PlatformID = 0, Name = "全部平台", CreateTime = DateTime.Now, IsEnabled = 1 });
                lst.Add(new PlatformModel { PlatformID = 1, Name = "基金", CreateTime = DateTime.Now, IsEnabled = 1 });
                lst.Add(new PlatformModel { PlatformID = 2, Name = "证券", CreateTime = DateTime.Now, IsEnabled = 1 });
                return lst;
            });
        }
    }
}
