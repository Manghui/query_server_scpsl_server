using System.Collections.Generic;
using System.ComponentModel;

namespace ServerQueryer
{
    public class Config
    {
        [Description("是否开启")]
        public bool is_enable { get; private set; } = true;
        [Description("调试模式")]
        public bool debug_mode { get; private set; } = false;
        [Description("接口地址")]
        public string server_api { get; private set; } = "https://yourdomain.com/api/updatetoserver.php";
        [Description("服务器标识(同一服务端请勿重复)")]
        public string server_id { get; private set; } = "manghui_first";
        [Description("服务器密匙")]
        public string server_key { get; private set; } = "Test123$$$";
    }
}