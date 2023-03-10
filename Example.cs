using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(getData("https://yourdomain/api/query.php", "id", "key", "测试服名"));
            Console.Read();
        }
        private static string getData(string api, string id, string key, string formatted_name)
        {
            string result = "{}";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(api); sb.Append("?id="); sb.Append(id); sb.Append("&key="); sb.Append(key);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sb.ToString());
                sb.Clear();
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                var stream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();
                stream.Close();
                reader.Close();

                var ri = JsonConvert.DeserializeObject<UpdateInfo>(result, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include });
                if (!ri.success)
                    return ri.reason;
                if (ri.formatVersion == "1.0.4")
                {
                    if ((new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() - ri.updateTime) >= 40)
                        return formatted_name + " 离线";
                    int i = 0;
                    string admin_list = String.Empty;
                    foreach (var cat4 in ri.playerStatus)
                    {
                        i++;
                        string gname = cat4.displyName != null ? cat4.displyName : cat4.nickName;
                        if (cat4.remoteAdminAccess)
                            admin_list += NoHtml(gname) + "\r\n";
                    }
                    if (admin_list != String.Empty)
                        admin_list.Substring(0, admin_list.Length - 2);

                    string results = formatted_name + " 在线: " + i.ToString();
                    string roundinfo = "游戏进行：";
                    TimeSpan ts;
                    switch (ri.serverStatus.roundStartTime)
                    {
                        case -10086:
                            roundinfo = "回合重置";
                            break;
                        case -10010:
                            roundinfo = "等待玩家";
                            break;
                        case -10000:
                            roundinfo = "回合结束";
                            break;
                        default:
                            ts = new TimeSpan(0, 0, (int)(new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() - (int)(ri.serverStatus.roundStartTime / 1000)));
                            roundinfo += "[" + ts.Minutes + "M" + ts.Seconds + "S" + "]";
                            break;
                    }
                    return results + " " + roundinfo + (admin_list != String.Empty ? "\r\n在线的管理员：\r\n" + admin_list : "");
                }
                else
                    return "解析版本不匹配";

            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
        public static string NoHtml(string html)
        {
            string StrNohtml = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            StrNohtml = System.Text.RegularExpressions.Regex.Replace(StrNohtml, "&[^;]+;", "");
            return StrNohtml;
        }
        public class UpdateInfo
        {
            public string formatVersion = "1.0.4";
            public List<PlayerStatus> playerStatus { get; set; } = new List<PlayerStatus>();
            public ServerStatus serverStatus { get; set; } = new ServerStatus();
            public int updateTime = 0;
            public bool success = false;
            public string reason = "null";
        }
        public class PlayerStatus
        {
            public string userId { get; set; }
            public string role { get; set; }
            public Badge badge { get; set; }
            public string displyName { get; set; }
            public string nickName { get; set; }
            public bool remoteAdminAccess { get; set; }
        }
        public class Badge
        {
            public string name { get; set; } = "";
            public string color { get; set; } = "";
        }
        public class ServerStatus
        {
            public long roundStartTime { get; set; }
        }
    }
}
