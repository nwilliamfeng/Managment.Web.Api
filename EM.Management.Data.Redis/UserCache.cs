using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;
using Newtonsoft.Json;


namespace EM.Management.Data.Redis
{
    public class UserCache : RedisCache, IUserRepository
    {
        private const string KEY = "hash_users";

        public async Task<User> AddOrUpdate(User user)
        {
            if (string.IsNullOrEmpty(user.Nickname))
                throw new ArgumentException("用户的nickname不能为空。");
            
            var action = string.IsNullOrEmpty(user.Id)? "create" : "modify";
            if (string.IsNullOrEmpty(user.Id))
                user.Id = Guid.NewGuid().ToString("N");
            var result = await this.Database.HashSetAsync(KEY, user.Nickname, JsonConvert.SerializeObject(user));
            $"{action} user : {Newtonsoft.Json.JsonConvert.SerializeObject(user)} , the result is {result}".Log();
            return user;

        }

      
       
        public async Task<User> Load(string nickname,string password)
        {
            var value = await this.Database.HashGetAsync(KEY, nickname);
            if (!string.IsNullOrEmpty(value))
            {
                var user = JsonConvert.DeserializeObject<User>(value);
                return user.Password == password ? user : null;
            }
                  
            return null;

        }
    }
}
