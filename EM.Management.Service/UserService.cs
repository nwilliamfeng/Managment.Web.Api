using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;
using EM.Management.Data;

namespace EM.Management.Service
{
    public class UserService : IUserService
    {
        private IEnumerable<IUserRepository> _userRepositories;

        public UserService(IEnumerable<IUserRepository> userRepositories)
        {
            if (userRepositories == null || userRepositories.Count() < 2)
                throw new ArgumentException("请配置正确的UserRepository。");
            this._userRepositories = userRepositories;
        }

        private IUserRepository Cache => this._userRepositories.FirstOrDefault(x => x.IsCache);

        private IUserRepository Db => this._userRepositories.FirstOrDefault(x => !x.IsCache);


        public async Task<JsonResultData<User>> AddOrUpdate(User user)
        {
            var result = await this.Cache.AddOrUpdate(user);
            result = await this.Db.AddOrUpdate(result);
            return result.ToJsonResultData();
        }

       

        public async Task<JsonResultData<User>> Load(string nickname,string password)
        {
            var result =await this.Cache.Load(nickname,password);
            if (result == null)
            {
                result = await Db.Load(nickname,password);
                if (result == null)
                    return new JsonResultData<User> {  StatusCode=1};
               await this.Cache.AddOrUpdate(result);
            }
            return result.ToJsonResultData();
        }
    }
}
