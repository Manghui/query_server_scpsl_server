using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace ServerQueryer
{
    internal class EventHandlers
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerVerfied(Player ply)
        {

        }
    }
}
