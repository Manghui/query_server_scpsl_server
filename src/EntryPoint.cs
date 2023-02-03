using Mirror;
using Newtonsoft.Json;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static RoundSummary;

namespace ServerQueryer
{

    public class EntryPoint
    {
        public static EntryPoint Singleton { get; private set; }

        private static bool shouldUpdateRightNow;
        private static long roundStartTime;

        [PluginPriority(LoadPriority.Highest)]
        [PluginEntryPoint("ServerQueryer", "1.0.3", "A Scp:SL Plugin for querying server status.", "Manghui")]
        void LoadPlugin()
        {
            Singleton = this;
            if (!PluginConfig.is_enable)
                return;

            EventManager.RegisterAllEvents(this);
            Task.Run(() => { Thread.Sleep(10 * 1000); UpdateToServer(); });
            Log.Info($"Plugin Loaded.");
        }
        #region 此处是强制更新数据请求处

        // 判断回合状态并非只有一种方法! 有最优解均可等效替代
        [PluginEvent(ServerEventType.WaitingForPlayers)]
        public void OnWaitingForPlayers() { shouldUpdateRightNow = true; roundStartTime = -10010; }

        // 判断回合状态并非只有一种方法! 有最优解均可等效替代
        [PluginEvent(ServerEventType.RoundRestart)]
        public void OnRoundRestart() { shouldUpdateRightNow = true; roundStartTime = -10086; }

        // 判断回合状态并非只有一种方法! 有最优解均可等效替代
        [PluginEvent(ServerEventType.RoundEnd)]
        public void OnRoundEnd(LeadingTeam lt) { shouldUpdateRightNow = true; roundStartTime = -10000; }

        // 判断回合状态并非只有一种方法! 有最优解均可等效替代
        [PluginEvent(ServerEventType.RoundStart)]
        public void OnRoundStart() => roundStartTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();

        [PluginEvent(ServerEventType.PlayerLeft)]
        public void OnPlayerLeft(Player p) => shouldUpdateRightNow = true;

        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerVerfied(Player p) => shouldUpdateRightNow = true;
        #endregion

        private void UpdateToServer()
        {
            int counter = 0;
            while (NetworkServer.active)
            {
                if (counter >= 10 || shouldUpdateRightNow)
                {
                    counter = 0; shouldUpdateRightNow = false;
                    var info = new UpdateInfo();
                    info.serverStatus.roundStartTime = roundStartTime;
                    foreach (var cat in Player.GetPlayers().Where(x =>
                            !x.ReferenceHub.isLocalPlayer &&
                            x.ReferenceHub.Mode == ClientInstanceMode.ReadyClient &&
                            !string.IsNullOrEmpty(x.ReferenceHub.characterClassManager.UserId)))
                    {
                        try
                        {
                            info.playerStatus.Add(new PlayerStatus()
                            {
                                displyName = cat.DisplayNickname,
                                nickName = cat.Nickname,
                                badge = new Badge()
                                {
                                    color = cat.ReferenceHub.serverRoles.Network_myColor,
                                    name = cat.ReferenceHub.serverRoles.Network_myText
                                },
                                remoteAdminAccess = cat.RemoteAdminAccess,
                                role = cat.Role.ToString(),
                                userId = cat.UserId
                            });
                        }
                        catch (Exception e) { if (PluginConfig.debug_mode) Log.Error("updateToServers(获取单玩家)#01: \r\n" + e.ToString()); }
                    }
                    try
                    {
                        //提交数据 并非只有这一种方法！
                        Utils.PostData(PluginConfig.server_api, PluginConfig.server_key, JsonConvert.SerializeObject(info));
                    }
                    catch (Exception e) { if (PluginConfig.debug_mode) Log.Error("updateToServers(提交数据)#02: \r\n" + e.ToString()); }
                }
                counter++;
                Thread.Sleep(1000);
            }
        }

        [PluginConfig] public Config PluginConfig;

    }
}