using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using SebaFixes.config;
using SebaFixes.utils;

namespace SebaFixes
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    internal class SebaFixes : BaseUnityPlugin
    {
        public const string PluginGuid = "com.sebag.pcbs.fixes";
        public const string PluginName = "Seba Fixes";
        public const string PluginVersion = "1.6.0";
        
        internal void Awake()
        {
            SFLog.log("loaded!");
            SFLog.disableLogger();
            ConfigHandler.Instance.ConfigFile = Config;
        }

        public SebaFixes()
        {
            var harmony = new Harmony(PluginGuid);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        private void Update()
        {
            ConfigHandler.Instance.Update();
        }

        protected void OnGUI()
        {
            ConfigHandler.Instance.OnGUI();
        }
    }
}