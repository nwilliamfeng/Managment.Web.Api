using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management
{
    public class Platform
    {
        /// <summary>
        /// 平台ID
        /// </summary>
     
        public int PlatformID { get; set; }

        /// <summary>
        /// 平台名称 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间 
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否有效标志位 
        /// </summary>
        public decimal? IsEnabled { get; set; }

        /// <summary>
        /// 逻辑删除标志位 
        /// </summary>
        public decimal? IsDel { get; set; }
    }
}
