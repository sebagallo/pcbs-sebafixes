﻿using HarmonyLib;
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
            if (__instance.m_computerItem != null)
            {
                var bonusPercentage = 50;
                var value = __result * (100 + bonusPercentage) / 100;
                SFLog.log($"Auction for: {__instance.m_computerItem.GetUIName()} Resale: {___m_value.ToString()} | Original Bid: {__result.ToString()} | Modified Bid: {value.ToString()}");
                return value;
            }
            return __result;

        }
    }
}