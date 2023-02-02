using System.Collections.Generic;

namespace ServerQueryer
{
    //大家都可以在任何地方补充
    public class UpdateInfo
    {
        public string formatVersion = "1.0.1";
        public List<PlayerStatus> playerStatus { get; set; } = new List<PlayerStatus>();
        public ServerStatus serverStatus { get; set; } = new ServerStatus();
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
