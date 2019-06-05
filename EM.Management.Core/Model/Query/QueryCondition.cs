using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Model
{
    public class QueryCondition
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public override int GetHashCode()
        {
            return this.PageIndex.GetHashCode() * 37 + this.PageSize.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is QueryCondition))
                return false;
            var other = obj as QueryCondition;
            if (this.PageSize != other.PageSize)
                return false;
            return this.PageIndex == other.PageIndex;
        }
    }
}
