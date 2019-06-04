using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management
{
    public class PointTask
    {
       
        public string TaskStrategyID { get; set; }
        
        public EnumTaskType TaskType { get; set; }
        
        public EnumTaskSubType TaskSubType { get; set; }
        
        public int FlowType { get; set; }
        
        public int TaskMinPoint { get; set; }
        
        public int TaskMaxPoint { get; set; }
        
        public int TaskState { get; set; }
        
        public int TaskRewardType { get; set; }
        
        public string TaskUrl { get; set; }
        
        public string TaskButtonText { get; set; }
        
        public string AllowUserGroup { get; set; }
        
        public string GroupName { get; set; }
        
        public int TaskSort { get; set; }
        
        public bool IsNeedGet { get; set; }
        
        public string TaskIconUrl { get; set; }
        
        public int TaskExpireDays { get; set; }
        
        public bool IsDel { get; set; }
        
        public DateTime UpdateTime { get; set; }
        
        public string TaskID { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int PlatformID { get; set; }
        
        public int Point { get; set; }
        
        public int DayTimes { get; set; }
        
        public bool IsEnabled { get; set; }
        
        public int TotalTimes { get; set; }
        
        public int TotalLimit { get; set; }
        
        public DateTime BeginTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public string Operator { get; set; }
        
        public int Crc32 { get; set; }
        
        public DateTime CreateTime { get; set; }
        
        public int DayLimit { get; set; }
        
        public int TagId { get; set; }

        public bool IsNew => string.IsNullOrEmpty(this.TaskID);

        public override bool Equals(object obj)
        {
            if (!(obj is PointTask))
                return false;
            return this.TaskID == (obj as PointTask).TaskID;
        }

        public override int GetHashCode()
        {
            return this.TaskID==null?base.GetHashCode():this.TaskID.GetHashCode();
        }
    }
}
