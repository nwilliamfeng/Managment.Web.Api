using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace EM.Management.Data.Redis
{
    public class PlatformCache:RedisCache,IPlatformRepository
    {
        private const string KEY = "set_platform";

        public async Task<bool> AddPlatforms(params Platform[] models)
        {
          return await  this.Database.SetAddAsync(KEY, models.Select(x => (RedisValue)JsonConvert.SerializeObject(x)).ToArray())>0;
        }

        public async Task<IEnumerable<Platform>> GetPlatforms()
        {
            var values = await this.Database.SetMembersAsync(KEY);
            return values.Select(x => JsonConvert.DeserializeObject<Platform>(x));
        }
    }
}
