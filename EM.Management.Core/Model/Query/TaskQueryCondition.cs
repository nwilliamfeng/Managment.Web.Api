

namespace EM.Management.Model
{
    
    public class TaskQueryCondition :QueryConditionWithTimeRange
    {
 
        public string PlatformId { get; set; }

   
        public int TaskTagId { get; set; }



        public string TaskName { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TaskQueryCondition))
                return false;

            return this..Equals(obj);
        }
    }
}
