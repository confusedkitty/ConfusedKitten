using System;
using System.Security.Cryptography;
using System.Text;

namespace ConfusedKitten.Common
{
    public static class Md5Helper
    {
        /// <summary>
        /// MD5加密,32位小写
        /// </summary>
        /// <param name="text">加密内容</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>System.String.</returns>
        private static string EncryptL32(string text, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            MD5 md5 = new MD5CryptoServiceProvider();
            var data = encoding.GetBytes(text);
            var encs = md5.ComputeHash(data);
            return BitConverter.ToString(encs).Replace("-", "").ToLower();
        }

        /// <summary>
        /// MD5加密,32位大写
        /// </summary>
        /// <param name="text">加密内容</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>System.String.</returns>
        private static string EncryptU32(string text, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(encoding.GetBytes(text));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            foreach (byte t in s)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + t.ToString("X");
            }
            return pwd;
        }

        /// <summary>
        /// MD5 16位加密 加密后密码为大写
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string EncryptU16(string text, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(text)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// MD5 16位加密 加密后密码为小写
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string EncryptL16(string text, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(text)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }
    }
}
