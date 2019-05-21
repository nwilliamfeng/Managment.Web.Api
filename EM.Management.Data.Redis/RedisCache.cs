using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EM.Management.Data.Redis
{
    public abstract class RedisCache:IRepository
    {
        private ConnectionMultiplexer connection;

        public static string ConfigName { get; set; }
       
        protected RedisCache()
        {
            connection = ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings[ConfigName]);

        }

        protected IDatabase Database
        {
            get
            {
                return connection.GetDatabase(0);
            }
        }

        public bool IsCache => true;
    }
}
