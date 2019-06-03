using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Guba.Utils
{
    /// <summary>
    /// 唯一id生成器
    /// </summary>
    public static class GUIDUtils
    {
        /// <summary>
        /// 32位长度 
        /// 获取新guid字符串，不含有 '-'
        /// </summary>
        public static string GuidString
        {
            get { return Guid.NewGuid().ToString("N"); }
        }

        /// <summary>
        /// 22位字符串
        /// guid 前64位 + 999 随机数
        /// </summary>
        /// <returns></returns>
        public static string UUID22()
        {
            Guid guid = Guid.NewGuid();
            // guid 前64位
            var buffer = guid.ToByteArray();
            var str = BitConverter.ToUInt64(buffer, 0).ToString();

            // guid 中间部分的32位，与pid进行XOR的值做种子进行随机数
            var pid = Process.GetCurrentProcess().Id;
            var lowGuidPart = BitConverter.ToUInt32(buffer, 8);
            var seed = (int)(pid ^ lowGuidPart);
            var rnd = new Random(seed);

            return string.Format("{0}{1:D3}", str, rnd.Next(1000));
        }
    }
}
