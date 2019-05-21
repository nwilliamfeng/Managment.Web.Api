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

        public PlatformService(IEnumerable<IPlatformRepository> repositories)
        {
            if (repositories == null || repositories.Count() == 0)
                throw new ArgumentNullException("未找到配置的数据仓库。");
            this._platformRepositories = repositories;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PlatformModel>> GetPlatforms()
        {
            var cache = this._platformRepositories.FirstOrDefault(x => x.IsCache);
            var data = this._platformRepositories.FirstOrDefault(x => !x.IsCache);
            IEnumerable<PlatformModel> result = new List<PlatformModel>();
            if (cache != null)
                result =await cache.GetPlatforms();
            if ( result.Count() > 0)
                return result;
            return data != null ? await data.GetPlatforms():result;
        }

        
    }
}
