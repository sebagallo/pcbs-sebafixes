using HarmonyLib;
using SebaFixes.config;
using SebaFixes.utils;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(Auction), "OnDayEnd")]
    public class Auction_OnDayEnd_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(Auction __instance)
        {
            if (ConfigHandler.Instance.FastAuctionsBool.Value && __instance.m_daysToGo > 1)
            {
                SFLog.log($"Reducing daysToGo from {__instance.m_daysToGo.ToString()} to 1");
                __instance.m_daysToGo = 1;
            }

            return true;
        }
    }
}