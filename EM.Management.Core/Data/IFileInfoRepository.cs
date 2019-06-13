using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;
 

namespace EM.Management.Data
{
    public interface IFileInfoRepository : IRepository
    {
        Task<FileInfo> Add(FileInfo file);

      

        Task<bool> Remove(string fileName);


        Task<FileInfo> Load(string id);
        

    }
}
