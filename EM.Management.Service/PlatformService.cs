using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Data;
 
namespace EM.Management.Service
{
    public class PlatformService : IPlatformService
    {
        private readonly IEnumerable<IPlatformRepository> _platformRepositories;

        public PlatformService( IEnumerable<IPlatformRepository> repositories)
        {
            if (repositories == null || repositories.Count() == 0)
                throw new ArgumentNullException("未找到配置的数据仓库。");
            this._platformRepositories = repositories;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Platform>> GetPlatforms()
        {
            
            IEnumerable<Platform> result = new List<Platform>();
            if (this.Cache != null)
                result =await this.Cache.GetPlatforms();
            if (result.Count() > 0)
                result.Log();
            if ( result.Count() > 0)
                return result;
            var db = this.Db;
            if (db == null)
                return result;
            result = await db.GetPlatforms();
            if (result.Count() > 0)
                this.AppendToRedisAsync(result);
          
            return result;
        }

        private IPlatformRepository Cache => this._platformRepositories.FirstOrDefault(x => x.IsCache);

        private IPlatformRepository Db => this._platformRepositories.FirstOrDefault(x => !x.IsCache);


        private  void AppendToRedisAsync(IEnumerable<Platform> platforms)
        {
            this.Cache?.AddPlatforms(platforms.ToArray());
        }
        
    }
}
