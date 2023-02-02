
using GameCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerQueryer
{
    public class Utils
    {
        //获取时间戳
        public static long getTimeStamp()
        {
            return new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
        }

        //一个独特的获取Md5方法
        public static string Md5String(string Content)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Content)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }

        //此处大佬们可以自己改为自己的同步方法，只要达成目的均可
        public static string PostData(string api, string key, string data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api);
            byte[] bytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);

            //此处是接口请求时间，毫秒为单位 不建议过长时间
            request.Timeout = 2500;

            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = bytes.Length;

            //设置Key
            request.Headers.Add("Manghui-Key", Md5String(key));

            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return result;
        }
    }
}