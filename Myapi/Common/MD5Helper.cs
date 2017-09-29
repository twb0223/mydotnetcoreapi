using System.Security.Cryptography;
using System.Text;

namespace Myapi.Common
{
    public class MD5Helper
    {
        public static string GetEncryptResult(string data, string key)
        {
            HMACMD5 source = new HMACMD5(Encoding.UTF8.GetBytes(key));
            byte[] buff = source.ComputeHash(Encoding.UTF8.GetBytes(data));
            string result = string.Empty;
            for (int i = 0; i < buff.Length; i++)
            {
                result += buff[i].ToString("X2");
            }
            return result;
        }
    }
}