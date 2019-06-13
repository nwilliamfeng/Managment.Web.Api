using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Data;
using EM.Management.Model;

namespace EM.Management.Data.MySQL
{
    public class UserRepository :IUserRepository
    {
        private List<User> _cache = new List<User>();

        public bool IsCache => false;

        public UserRepository()
        {
             
            _cache.Add(new User {  Id="1001", CreateTime=DateTime.Now, Name="FengWei", Password="1234",Nickname="fw" });
            _cache.Add(new User { Id = "1002", CreateTime = DateTime.Now, Name = "Customer1", Password = "1111", Nickname = "user" });


        }

        public Task<User> AddOrUpdate(User user)
        {
            return Task.Run(() =>
            {
                if (_cache.Contains(user))
                {
                    _cache[_cache.IndexOf(user)] = user;
                    user.UpdateTime = DateTime.Now;
                }
                else
                {
                    if (user.Id == null)
                        user.Id = $"100{_cache.Count}";
                    _cache.Add(user);
                }
                return user;
            });
        }


       

        public Task<User> Load(string nickname, string password)
        {
            return Task.Run(() => this._cache.FirstOrDefault(x => x.Nickname == nickname && x.Password==password));
        }
    }
}
