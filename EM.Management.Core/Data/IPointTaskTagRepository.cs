using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;

namespace EM.Management.Data
{
    public interface IPointTaskTagRepository:IRepository
    {
        Task<IEnumerable<PointTaskTag>> LoadAll();

        Task<bool> AddOrUpdate(PointTaskTag taskTag);

       

    }
}
