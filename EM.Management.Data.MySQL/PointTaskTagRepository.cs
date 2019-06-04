using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Data.MySQL
{
    public class PointTaskTagRepository : IPointTaskTagRepository
    {
        public bool IsCache => false;
        private List<PointTaskTag> lst = new List<PointTaskTag>();


        public PointTaskTagRepository()
        {
            
            lst.Add(new PointTaskTag { PlatformID = 0, TagName = "推荐任务", Id = 1 });
            lst.Add(new PointTaskTag { PlatformID = 1, TagName = "新手任务", Id = 2 });
            lst.Add(new PointTaskTag { PlatformID = 0, TagName = "日常任务", Id = 3 });
            lst.Add(new PointTaskTag { PlatformID = 0, TagName = "进阶任务", Id = 4 });
           
        }

        public  Task<bool> AddOrUpdate(PointTaskTag taskTag)
        {
            return Task.Run(()=>true);
        }

        public Task<PointTaskTag> Load(int tagId)
        {
            return Task.Run(() => lst.FirstOrDefault(x => x.Id == tagId));
        }

        public Task<IEnumerable<PointTaskTag>> LoadAll()
        {
            return Task.Run<IEnumerable<PointTaskTag>>(() => lst);
        }
    }
}
