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
      

        private const string KEY = "hash_loginInfo_list";
 
        public async Task<LoginInfo> Load(string userId, string token)
        {
        
        
        }

        public async Task<bool> Save(LoginInfo loginInfo)
        {
            var result = false;

            if (!this.Database.HashExists(KEY, loginInfo.UserId))
                result = await this.Database.HashSetAsync(KEY, loginInfo.UserId, JsonConvert.SerializeObject(loginInfo));
            else
                result = await this.Database.HashSetAsync();
            return task;

        }
    }
}
