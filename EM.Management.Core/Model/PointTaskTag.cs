using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management
{
    public class PointTaskTag
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 支持平台（基金、证券或全平台）
        /// </summary>
        public int PlatformID { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 标签顺序
        /// </summary>
        public int TagSequence { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 添加人员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 逻辑删除标志位
        /// </summary>
        public bool IsDel { get; set; }
    }
}
