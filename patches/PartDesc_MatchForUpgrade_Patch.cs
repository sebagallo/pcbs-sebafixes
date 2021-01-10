using HarmonyLib;
using SebaFixes.config;
using SebaFixes.utils;
using UnityEngine;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(PartDesc), "MatchForUpgrade")]
    public class PartDescMatchForUpgradePatch
    {
        [HarmonyPostfix]
        private static bool Postfix(bool __result, PartDesc.Type ___m_type, PartDesc.Type rhsType)
        {
            if (ConfigHandler.Instance.UpgradeMoreComponentMatchBool.Value)
            {
                if (rhsType == PartDesc.Type.AIR_COOLER)
                {
                    var cond = ___m_type == PartDesc.Type.AIR_COOLER || ___m_type == PartDesc.Type.LIQUID_COOLER;
                    SFLog.log($"Part: {___m_type.ToString()} | Reference: {rhsType.ToString()} | match: {__result.ToString()}");
                    return cond;
                }
            }
            return __result;
        }
        
    }
}