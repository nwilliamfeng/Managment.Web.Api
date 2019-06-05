

namespace EM.Management.Model
{
    
    public class TaskQueryCondition :QueryConditionWithTimeRange
    {
 
        public string PlatformId { get; set; }

   
        public int TaskTagId { get; set; }



        public string TaskName { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode()*13+this.PlatformId.GetHashCode()+this.TaskTagId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TaskQueryCondition))
                return false;
            var other = obj as TaskQueryCondition;
           
            if (this.PlatformId != other.PlatformId)
                return false;
            if (this.TaskName != other.TaskName)
                return false;
            if (this.TaskTagId != other.TaskTagId)
                return false;
         
            return base.Equals(obj);
        }
    }
}
