using CommandSystem;
using PlayerStatsSystem;
using PluginAPI.Core;
using System;

namespace ServerQueryer
{
    public class GetKeyCommand
    {
        [CommandHandler(typeof(RemoteAdminCommandHandler))]
        public class Common : ICommand
        {
            public string Command { get; } = "getquerykey";

            public string[] Aliases { get; }

            public string Description { get; } = "Get Server Queryer's Key";

            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                response = "please use the console to execute this command";
                
                if (sender.LogName == "SERVER CONSOLE")
                    response = Utils.Md5String(EntryPoint.Singleton.PluginConfig.server_key);

                return true;
            }
        }
    }
}
