using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;

namespace EM.Management.Data
{
    public interface IUserRepository:IRepository
    {
        Task<User> Load(string nickName,string password);

        Task<User> AddOrUpdate(User user);

       
    }
}
