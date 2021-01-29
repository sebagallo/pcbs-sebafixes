using HarmonyLib;
using SebaFixes.config;
using SebaFixes.utils;
using UnityEngine;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(Auction), "GetBaseBid")]
    public class Auction_GetBaseBid_Patch
    {
        [HarmonyPostfix]
        public static int Postfix(int __result, int ___m_value, Auction __instance, int day)
        {
            if (!ConfigHandler.Instance.BetterBidsBool.Value)
            {
                return __result;
            }
            if (__instance.m_computerItem != null)
            {
                var modifier = ConfigHandler.Instance.BetterBidsValue.Value;
                var value = __result + (int)(__result * modifier);
                SFLog.log($"Auction for: {__instance.m_computerItem.GetUIName()} Resale: {___m_value.ToString()} | Original Bid: {__result.ToString()} | Modified Bid: {value.ToString()}");
                return value;
            }
            return __result;

        }
    }
}