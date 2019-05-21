using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EM.Management.Data.Redis
{
    public class PlatformCache:RedisCache
    {
        private const string KEY = "set_platform";

      

        public async Task<IEnumerable<PlatformModel>> GetPlatforms()
        {
            var values = await this.Database.SetMembersAsync(KEY);
            return values.Select(x => JsonConvert.DeserializeObject<PlatformModel>(x));
        }
    }
}
