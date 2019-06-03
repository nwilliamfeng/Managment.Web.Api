using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CommonUtils
{
    /// <summary>
    /// 加解密工具类
    /// </summary>
    public class EncryptUtils
    {
        #region 生成key
        /// <summary>
        /// 随机生成8位长度KEY
        /// </summary>
        /// <returns></returns>
        public static string GenerateKey(int _len)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            byte[] keybyte = new byte[_len];

            int i = 0;
            while (i < _len)
            {
                byte bRand = (byte)random.Next(48, 122);

                if ((bRand >= 48 && bRand <= 57) || (bRand >= 65 && bRand <= 90) || (bRand >= 97 && bRand <= 122))
                {
                    keybyte[i] = bRand;
                    i++;
                }
            }
            return ASCIIEncoding.ASCII.GetString(keybyte);
        }
        #endregion

        #region md5
        /// <summary>
        /// md5 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Encrypt(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                //LogTool.WriteSystemLog("Md5Encrypt", "空");
                return "";
            }
            MD5 md5 = MD5.Create();
            byte[] source = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sBuilder.Append(source[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// md5 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Encrypt(byte[] input)
        {
            MD5 md5 = MD5.Create();
            byte[] source = md5.ComputeHash(input);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sBuilder.Append(source[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// md5 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Encrypt(Stream input)
        {
            MD5 md5 = MD5.Create();
            byte[] source = md5.ComputeHash(input);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sBuilder.Append(source[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 生成16位 的md5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Encrypt16(string input)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(input)));
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 生成16位 的md5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Encrypt16(byte[] input)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(input));
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 生成16位 的md5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Encrypt16(Stream input)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(input));
            t2 = t2.Replace("-", "");
            return t2;
        }

        #endregion

        #region  DES

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DESEnCode(string pToEncrypt, string sKey)
        {
            pToEncrypt = HttpUtility.UrlEncode(pToEncrypt);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);


            des.Key = Encoding.GetEncoding("UTF-8").GetBytes(sKey);
            des.IV = Encoding.GetEncoding("UTF-8").GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DESDeCode(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = Encoding.GetEncoding("UTF-8").GetBytes(sKey);
            des.IV = Encoding.GetEncoding("UTF-8").GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            return HttpUtility.UrlDecode(System.Text.Encoding.GetEncoding("UTF-8").GetString(ms.ToArray()));
        }

        private static DESCryptoServiceProvider getDesProvider(string k)
        {
            var des = new DESCryptoServiceProvider();

            if (!string.IsNullOrEmpty(k))
            {
                des.IV = des.Key = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(k, "md5").Substring(0, 8));
            }

            return des;
        }

        #endregion

        #region 音视频加密串

        public static byte[] Encrypt(byte[] data, byte[] key)
        {

            byte[] dataBytes;
            if (data.Length % 2 == 0)
            {
                dataBytes = data;
            }
            else
            {
                dataBytes = new byte[data.Length + 1];
                Array.Copy(data, 0, dataBytes, 0, data.Length);
                dataBytes[data.Length] = 0x0;
            }
            byte[] result = new byte[dataBytes.Length * 4];
            uint[] formattedKey = FormatKey(key);
            uint[] tempData = new uint[2];
            for (int i = 0; i < dataBytes.Length; i += 2)
            {
                tempData[0] = dataBytes[i];
                tempData[1] = dataBytes[i + 1];
                code(tempData, formattedKey);
                Array.Copy(ConvertUIntToByteArray(tempData[0]), 0, result, i * 4, 4);
                Array.Copy(ConvertUIntToByteArray(tempData[1]), 0, result, i * 4 + 4, 4);
            }
            return result;
        }

        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            uint[] formattedKey = FormatKey(key);
            int x = 0;
            uint[] tempData = new uint[2];
            byte[] dataBytes = new byte[data.Length / 8 * 2];
            for (int i = 0; i < data.Length; i += 8)
            {
                tempData[0] = ConvertByteArrayToUInt(data, i);
                tempData[1] = ConvertByteArrayToUInt(data, i + 4);
                decode(tempData, formattedKey);
                dataBytes[x++] = (byte)tempData[0];
                dataBytes[x++] = (byte)tempData[1];
            }
            //修剪添加的空字符
            if (dataBytes[dataBytes.Length - 1] == 0x0)
            {
                byte[] result = new byte[dataBytes.Length - 1];
                Array.Copy(dataBytes, 0, result, 0, dataBytes.Length - 1);
            }
            return dataBytes;

        }

        static uint[] FormatKey(byte[] key)
        {
            if (key.Length == 0)
                throw new ArgumentException("Key must be between 1 and 16 characters in length");
            byte[] refineKey = new byte[16];
            if (key.Length < 16)
            {
                Array.Copy(key, 0, refineKey, 0, key.Length);
                for (int k = key.Length; k < 16; k++)
                {
                    refineKey[k] = 0x20;
                }
            }
            else
            {
                Array.Copy(key, 0, refineKey, 0, 16);
            }
            uint[] formattedKey = new uint[4];
            int j = 0;
            for (int i = 0; i < refineKey.Length; i += 4)
                formattedKey[j++] = ConvertByteArrayToUInt(refineKey, i);
            return formattedKey;
        }

        #region Tea Algorithm

        static void code(uint[] v, uint[] k)
        {
            uint y = v[0];
            uint z = v[1];
            uint sum = 0;
            uint delta = 0x9e3779b9;
            uint n = 16;
            while (n-- > 0)
            {
                sum += delta;
                y += (z << 4) + k[0] ^ z + sum ^ (z >> 5) + k[1];
                z += (y << 4) + k[2] ^ y + sum ^ (y >> 5) + k[3];
            }
            v[0] = y;
            v[1] = z;
        }

        static void decode(uint[] v, uint[] k)
        {
            uint n = 16;
            uint sum;
            uint y = v[0];
            uint z = v[1];
            uint delta = 0x9e3779b9;

            sum = delta << 4;
            while (n-- > 0)
            {
                z -= (y << 4) + k[2] ^ y + sum ^ (y >> 5) + k[3];
                y -= (z << 4) + k[0] ^ z + sum ^ (z >> 5) + k[1];
                sum -= delta;
            }
            v[0] = y;
            v[1] = z;
        }

        #endregion

        private static byte[] ConvertUIntToByteArray(uint v)
        {
            byte[] result = new byte[4];
            result[0] = (byte)(v & 0xFF);
            result[1] = (byte)((v >> 8) & 0xFF);
            result[2] = (byte)((v >> 16) & 0xFF);
            result[3] = (byte)((v >> 24) & 0xFF);
            return result;
        }

        private static uint ConvertByteArrayToUInt(byte[] v, int offset)
        {
            if (offset + 4 > v.Length) return 0;
            uint output;
            output = (uint)v[offset];
            output |= (uint)(v[offset + 1] << 8);
            output |= (uint)(v[offset + 2] << 16);
            output |= (uint)(v[offset + 3] << 24);
            return output;
        }
        #endregion

        #region aes 加密解密
        private static string aeskey = "a72569c1ca9da77a77ec57a1b08f5912";
        /// <summary>
        /// AES解密
        /// 跨平台 
        /// java ：https://www.cnblogs.com/xbzhu/p/7064642.html
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptAes(string source, string key = "")
        {
            if (string.IsNullOrWhiteSpace(key)) key = aeskey;
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                byte[] content = Convert.FromBase64String(source);
                aesProvider.Key = Convert.FromBase64String(key);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = content;
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Encoding.UTF8.GetString(results);
                }
            }
        }

        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptAes(string source, string key = "")
        {
            if (string.IsNullOrWhiteSpace(key)) key = aeskey;
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = Convert.FromBase64String(key);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] inputBuffers = Encoding.UTF8.GetBytes(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    aesProvider.Dispose();
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        #endregion

        #region SHA256


        public static string Encode(string input)
        {
            SHA256 sha256 = new SHA256Managed();
            byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            sha256.Clear();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }


            return sb.ToString();
        }
        #endregion

        #region gzip
        /// <summary>
        /// Gzip压缩
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static byte[] GzipCompress(byte[] rawData)
        {
            MemoryStream ms = new MemoryStream();
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
            compressedzipStream.Write(rawData, 0, rawData.Length);
            compressedzipStream.Close();
            return ms.ToArray();
        }
        /// <summary>
        /// Gzip解压缩
        /// </summary>
        /// <param name="zippedData"></param>
        /// <returns></returns>
        public static byte[] GzipDecompress(byte[] zippedData)
        {
            MemoryStream ms = new MemoryStream(zippedData);
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }

        #endregion
    }
}
