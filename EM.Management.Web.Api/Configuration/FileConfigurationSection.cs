using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace EM.Management.Web.Config
{
    public class FileConfigurationSection:ConfigurationSection
    {
        public static FileConfigurationSection Instance
        {
            get { return ConfigurationManager.GetSection("fileConfig") as FileConfigurationSection; }
        }

        [ConfigurationProperty("FileServerAddress", IsRequired = true)]
        public string FileServerAddress
        {
            get { return (string)base["FileServerAddress"]; }
        }

 
        
    }
}