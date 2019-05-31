using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EM.Management.Model
{
    [Serializable]
    public class TaskQueryCondition :QueryConditionWithTimeRange
    {
        public string PlatformId { get; set; }

        public int TaskTagId { get; set; }

        public string TaskName { get; set; }
    }
}
