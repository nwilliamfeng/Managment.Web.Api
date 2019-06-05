using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtils;
using Newtonsoft.Json;

namespace EM.Management.Data.Redis
{
    public class PointTaskTagCache :RedisCache, IPointTaskTagRepository
    {
        private const string KEY = "hash_taskTags";
      
         

        public async Task<bool> AddOrUpdate(PointTaskTag taskTag)
        {
            var action = taskTag.Id == 0 ? "create" : "modify";
            if (taskTag.Id == 0)
                taskTag.Id=(int)DateTime.Now.ToUnixTime();
            //else if (!this.Database.HashExists(KEY, taskTag.Id))
            //    throw new InvalidOperationException("不存在的taskTag");
            var result = await this.Database.HashSetAsync(KEY, taskTag.Id, JsonConvert.SerializeObject(taskTag));
            $"{action} taskTag : {Newtonsoft.Json.JsonConvert.SerializeObject(taskTag)} , the result is {result}".Log();
            return result;

        }

        

        public async Task<IEnumerable<PointTaskTag>> LoadAll()
        {
            var hes = await this.Database.HashGetAllAsync(KEY);
            if (hes == null || hes.Count() == 0)
                return new PointTaskTag[] { };
            return hes.Select(x => JsonConvert.DeserializeObject<PointTaskTag>(x.Value));
        }
    }
}
