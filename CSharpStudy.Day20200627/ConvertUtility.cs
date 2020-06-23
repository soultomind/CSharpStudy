using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200627
{
    public class ConvertUtility
    {
        /// <summary>
        /// 해당 문자열을 Base64 형태로 인코딩하여 반환
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encodingName"></param>
        /// <returns></returns>
        public static string Base64Encode(string text, string encodingName = "UTF-8")
        {
            byte[] bytes = GetBytes(text, encodingName);
            return ToBase64String(bytes);
        }

        /// <summary>
        /// 해당 Base64 문자열을 디코딩하여 반환
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Base64Decode(string text)
        {
            byte[] bytes = FromBase64String(text);
            return GetString(bytes);
        }

        public static string ToBase64StringFromFile(string path)
        {
            return ToBase64String(System.IO.File.ReadAllBytes(path));
        }

        private static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        private static byte[] FromBase64String(string text)
        {
            return Convert.FromBase64String(text);
        }

        private static byte[] GetBytes(string text, string encodingName = "UTF-8")
        {
            Encoding encoding = Encoding.GetEncoding(encodingName);
            if (encoding != null)
            {
                return encoding.GetBytes(text);
            }
            return null;
        }

        private static string GetString(byte[] bytes, string encodingName = "UTF-8")
        {
            Encoding encoding = Encoding.GetEncoding(encodingName);
            if (encoding != null)
            {
                return encoding.GetString(bytes);
            }
            return null;
        }
    }
}
