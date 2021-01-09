using HarmonyLib;
using UnityEngine;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(ReplacePartObjective), "TestSatisifiedBy")]
    public class ReplacePartObjectiveTestSatisfiedByPatch
    {

        [HarmonyPostfix]
        private static bool Postfix(bool __result, ComputerSave save, PartDesc.Type ___m_partType, string ___m_refPart)
        {
            // Debug.Log("[SF] TestSatisifiedBy");
            if (___m_partType == PartDesc.Type.MOTHERBOARD ||
                ___m_partType == PartDesc.Type.AIR_COOLER ||
                ___m_partType == PartDesc.Type.LIQUID_COOLER)
            {
                foreach (var partInstance in save.GetAllParts())
                {
                    if (partInstance!=null && partInstance.IsActive())
                    {
                        PartDesc refPart = PartsDatabase.GetDesc(___m_refPart);
                        return partInstance.GetPart().MatchForUpgrade(refPart.m_type);
                    }
                }
            }
            return __result;
        }
        
    }
}