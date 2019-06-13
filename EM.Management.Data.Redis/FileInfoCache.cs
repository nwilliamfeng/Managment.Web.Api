using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;
using Newtonsoft.Json;


namespace EM.Management.Data.Redis
{
    public class FileInfoCache : RedisCache, IFileInfoRepository
    {
        private const string KEY = "hash_file";

        public async Task<FileInfo> Add (FileInfo fileInfo)
        {
            
            var action = string.IsNullOrEmpty(user.Id)? "create" : "modify";
            if (string.IsNullOrEmpty(user.Id))
                user.Id = Guid.NewGuid().ToString("N");
            var result = await this.Database.HashSetAsync(KEY, user.Nickname, JsonConvert.SerializeObject(user));
            $"{action} user : {Newtonsoft.Json.JsonConvert.SerializeObject(user)} , the result is {result}".Log();
            return user;

        }

      
       
        public async Task<FileInfo> Load(string id)
        {
            var value = await this.Database.HashGetAsync(KEY, nickname);
            if (!string.IsNullOrEmpty(value))
            {
                var user = JsonConvert.DeserializeObject<User>(value);
                return user.Password == password ? user : null;
            }
                  
            return null;

        }

        public Task<bool> Remove(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
