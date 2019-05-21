using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management
{
    public class DayReport
    {
       
        public string ID
        {
            get; set;
        }
       
        public string ReportDate
        {
            get; set;
        }
      
        public string CompanyId
        {
            get; set;
        }
       
        public int ReportType
        {
            get; set;
        }
      
        public decimal Value
        {
            get; set;
        }
       
        public decimal Total
        {
            get; set;
        }
      
        public string Extra
        {
            get; set;
        }
       
        public string CreateTime
        {
            get; set;
        }
     
        public string UpdateTime
        {
            get; set;
        }  
        
        public int PlatformId
        {
            get; set;
        }

    }
}
