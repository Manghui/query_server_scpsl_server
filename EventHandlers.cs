using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace ServerQueryer
{
    internal class EventHandlers
    {
        [PluginEvent(ServerEventType.RoundStart)]
        public void RoundStarting()
        {

        }
        [PluginEvent(ServerEventType.WaitingForPlayers)]
        public void OnWaitingforPlayers()
        {

        }
    }
}
