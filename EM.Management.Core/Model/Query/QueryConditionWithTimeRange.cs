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

        public override int GetHashCode()
        {
            return base.GetHashCode()+ this.StartTime.GetHashCode()*37+this.EndTime.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is QueryConditionWithTimeRange))
                return false;
            var other = obj as QueryConditionWithTimeRange;
            if (this.EndTime != other.EndTime)
                return false;
            if (this.StartTime != other.StartTime)
                return false;
            return base.Equals(obj);
        }
    }
}
