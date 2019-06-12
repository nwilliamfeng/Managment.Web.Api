using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace EM.Management.Data.Redis
{
    public class LoginInfoCache : RedisCache, ILoginInfoRepository
    {
     
        private const string KEY = "hash_loginInfos";
 
        public async Task<LoginInfo> Load(string userId)
        {
            if (!this.Database.HashExists(KEY, userId))
                return null;
            var value =await this.Database.HashGetAsync(KEY,userId);
            return JsonConvert.DeserializeObject<LoginInfo>( value);        
        }

        public async Task<bool> Save(LoginInfo loginInfo)
        {
            return await this.Database.HashSetAsync(KEY, loginInfo.UserId, JsonConvert.SerializeObject(loginInfo));       
        }

      
    }
}
