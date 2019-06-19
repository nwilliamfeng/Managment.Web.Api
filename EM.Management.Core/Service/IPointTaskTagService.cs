using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;
using Microcomm;

namespace EM.Management.Service
{
    public interface IPointTaskTagService
    {
        Task<JsonResultData<IEnumerable<PointTaskTag>>> LoadAll();

        Task<JsonResultData<bool>> AddOrUpdate(PointTaskTag taskTag);

       

    }
}
