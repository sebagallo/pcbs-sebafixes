using HarmonyLib;
using SebaFixes.utils;
using UnityEngine;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(ReplacePartObjective), "TestSatisifiedBy")]
    public class ReplacePartObjectiveTestSatisfiedByPatch
    {

        [HarmonyPostfix]
        private static bool Postfix(bool __result, ComputerSave save, PartDesc.Type ___m_partType, string ___m_refPart, int ___m_totalPartsOfType)
        {
            if (___m_partType == PartDesc.Type.MOTHERBOARD ||
                ___m_partType == PartDesc.Type.AIR_COOLER ||
                ___m_partType == PartDesc.Type.LIQUID_COOLER)
            {
                var successfulPartChanges = 0;
                foreach (var partInstance in save.GetAllParts())
                {
                    if (partInstance!=null && partInstance.IsActive())
                    {
                        PartDesc refPart = PartsDatabase.GetDesc(___m_refPart);
                        var canUpgrade = partInstance.GetPart().MatchForUpgrade(refPart.m_type);
                        SFLog.log($"{partInstance.GetPart().m_partName} | canUpgrade: {canUpgrade.ToString()}");
                        if (canUpgrade)
                        {
                            successfulPartChanges++;
                        }
                    }
                }
                var cond = ___m_totalPartsOfType == successfulPartChanges;
                SFLog.log($"totalPartsOfType: {___m_totalPartsOfType.ToString()}, successfulPartChanges: {successfulPartChanges.ToString()}, satisfied: {cond.ToString()}");
                return cond;
            }
            return __result;
        }
        
    }
}