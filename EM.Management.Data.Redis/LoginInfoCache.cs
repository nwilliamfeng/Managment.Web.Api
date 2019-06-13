using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;
using CommonUtils;

namespace EM.Management.Data.Redis
{
    public class LoginInfoCache : RedisCache, ILoginInfoRepository
    {
     
        private const string KEY = "hash_loginInfos";
 
        public async Task<int> GetLoginTimeStamp(string userId)
        {
            if (!this.Database.HashExists(KEY, userId))
                return -1;
            var value =await this.Database.HashGetAsync(KEY,userId);
            return (int) JsonConvert.DeserializeObject<DateTime>( value).ToUnixTime();        
        }

        public async Task<bool> Update(string userId,DateTime time)
        {
            return await this.Database.HashSetAsync(KEY, userId, JsonConvert.SerializeObject(time));       
        }

      
    }
}
