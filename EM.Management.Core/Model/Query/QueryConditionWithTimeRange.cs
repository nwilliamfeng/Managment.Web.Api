using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Model
{
    public class QueryConditionWithTimeRange:QueryCondition
    {
        public DateTime StartTime { get; set; }  
        public DateTime EndTime { get; set; }  
    }
}
