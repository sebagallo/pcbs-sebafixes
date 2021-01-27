using System;
using System.Reflection;
using BepInEx.Configuration;
using UnityEngine;

namespace SebaFixes.config
{
    public sealed class ConfigHandler
    {
        private ConfigFile _configFile;
        private bool ShowDebugUI = false;
        private static readonly ConfigHandler instance = new ConfigHandler();
        
        static ConfigHandler()
        {
        }

        private ConfigHandler()
        {
        }

        public static ConfigHandler Instance
        {
            get
            {
                return instance;
            }
        }

        public ConfigFile ConfigFile
        {
            get
            {
                return _configFile;
            }
            set
            {
                _configFile = value;
                Init();
            }
        }
        
        public ConfigEntry<bool> BetterBidsBool { get; set; }
        public ConfigEntry<int> BetterBidsValue { get; set; }
        public ConfigEntry<bool> MoreOffersBool { get; set; }
        public ConfigEntry<int> MoreOffersValue { get; set; }
        public ConfigEntry<bool> FasterVacuumBool { get; set; }
        public ConfigEntry<float> FasterVacuumValue { get; set; }
        public ConfigEntry<bool> USBInsertedBool { get; set; }
        public ConfigEntry<bool> UpgradePriceCheckRemoveBool { get; set; }
        public ConfigEntry<bool> UpgradeMoreComponentMatchBool { get; set; }
        public ConfigEntry<bool> NoRebootInstallBool { get; set; }
        public ConfigEntry<bool> NoRebootUnInstallBool { get; set; }
        public ConfigEntry<bool> FastAuctionsBool { get; set; }
        public ConfigEntry<bool> FreeDeliveryBool { get; set; }
        public ConfigEntry<KeyboardShortcut> ShowDebugMenu { get; set; }
        private void Init()
        {
            BetterBidsBool = _configFile.Bind<bool>("PCBay", "Better Bids", true,
                new ConfigDescription("Get better bids for your PC sales"));
            BetterBidsValue = _configFile.Bind<int>("PCBay", "Better Bids Percent", 50,
                new ConfigDescription("Define the percent bonus to add to the bids", new AcceptableValueRange<int>(0, 200)));
            MoreOffersBool = _configFile.Bind<bool>("PCBay", "More Daily Offers", true,
                new ConfigDescription("Get more daily offers"));
            MoreOffersValue = _configFile.Bind<int>("PCBay", "More Daily Offers Number", 7,
                new ConfigDescription("Define the quantity of the total offers to receive (vanilla: 4)", new AcceptableValueRange<int>(1, 20)));
            FastAuctionsBool = _configFile.Bind<bool>("PCBay", "Fast Auctions", true,
                new ConfigDescription("Auctions end in 1 day"));
            FasterVacuumBool = _configFile.Bind<bool>("Misc", "Faster Cleaning", true,
                new ConfigDescription("Clean the PC components faster"));
            FasterVacuumValue = _configFile.Bind<float>("Misc", "Faster Cleaning Value", 0.20f,
                new ConfigDescription("Speed at which the components get cleanded (vanilla: 0.05)",
                    new AcceptableValueRange<float>(0f, 1f)));
            USBInsertedBool = _configFile.Bind<bool>("Misc", "USB Not Required", true,
                new ConfigDescription("Do not require the USB Drive to be connected to install OS and software"));
            FreeDeliveryBool = _configFile.Bind<bool>("Misc", "Free Delivery", true,
                new ConfigDescription("Deliveries from the shop are free"));
            UpgradePriceCheckRemoveBool = _configFile.Bind<bool>("PC Upgrade", "Remove Price Check", true,
                new ConfigDescription("Remove price check during upgrades for MBs and Air/Liquid Coolers"));
            UpgradeMoreComponentMatchBool = _configFile.Bind<bool>("PC Upgrade", "More Component Matches", true,
                new ConfigDescription("Make it possible to upgrade air coolers to liquid coolers"));
            NoRebootInstallBool = _configFile.Bind<bool>("Software", "Install No Reboot", true,
                new ConfigDescription("Do not require the PC to be reboot after install"));
            NoRebootUnInstallBool = _configFile.Bind<bool>("Software", "UnInstall No Reboot", true,
                new ConfigDescription("Do not require the PC to be reboot after uninstall"));
            ShowDebugMenu =
                _configFile.Bind<KeyboardShortcut>("Debug", "Show Debug Menu", new KeyboardShortcut(KeyCode.F2));

        }

        public void Update()
        {
            if (ShowDebugMenu.Value.IsDown())
            {
                ShowDebugUI = !ShowDebugUI;
            }

            if (ShowDebugUI)
            {
                ShowMouse(ShowDebugUI);
            }
        }

        public void OnGUI()
        {
            if (ShowDebugUI)
            {
                config.DebugMenu.Instance.Show();
            }
        }

        private void ShowMouse(bool condition)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = condition;
        }

    }
}