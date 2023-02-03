using Newtonsoft.Json.Linq;
using NorthwoodLib.Pools;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Utf8Json.Internal.DoubleConversion;

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
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(Content + EntryPoint.Singleton.PluginConfig.salt)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }

        //此处大佬们可以自己改为自己的同步方法，只要达成目的均可
        public static bool PostData(string api, string key, string data)
        {
            System.Text.StringBuilder sb = StringBuilderPool.Shared.Rent();

            sb.Append(api);
            sb.Append("?");
            sb.Append("id=" + EntryPoint.Singleton.PluginConfig.server_id);
            sb.Append("&");
            sb.Append("key=" + Md5String(key));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(StringBuilderPool.Shared.ToStringReturn(sb));
            byte[] bytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);

            //此处是接口请求时间，毫秒为单位 不建议过长时间
            request.Timeout = 2500;

            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = bytes.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

            var result = JObject.Parse(reader.ReadToEnd());

            reader.Close();
            response.Close();

            if ((bool)result["success"])
                return true;
            else
                throw new Exception((string)result["reason"]);
        }
    }
}