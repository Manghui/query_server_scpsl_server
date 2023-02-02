using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace ServerQueryer
{

    public class EntryPoint
    {
        public static EntryPoint Singleton { get; private set; }

        [PluginPriority(LoadPriority.Highest)]
        [PluginEntryPoint("ServerQueryer", "1.0.0", "插件介绍", "Manghui")]
        void LoadPlugin()
        {
            Singleton = this;
            Log.Info("Loaded plugin, register events...");
            EventManager.RegisterEvents<EventHandlers>(this);
            Log.Info($"Plugin Loaded.");
        }

        [PluginConfig] public Config PluginConfig;

    }
}