using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Model
{
    public class LiquidationQueryCondition:QueryCondition
    {
       public DateTime ClearDate { get; set; }

       public int Platformid { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 13 + this.Platformid.GetHashCode()  ;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LiquidationQueryCondition))
                return false;
            var other = obj as LiquidationQueryCondition;

            if (this.ClearDate != other.ClearDate)
                return false;
            if (this.Platformid != other.Platformid)
                return false;
      
            return base.Equals(obj);
        }
    }
}
