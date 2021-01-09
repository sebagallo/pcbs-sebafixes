using HarmonyLib;
using UnityEngine;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(ReplacePartObjective), "TestSatisifiedBy")]
    public class ReplacePartObjectiveTestSatisfiedByPatch
    {

        [HarmonyPostfix]
        private static bool Postfix(bool __result, ComputerSave save, PartDesc.Type ___m_partType)
        {
            // Debug.Log("[SF] TestSatisifiedBy");
            if (___m_partType == PartDesc.Type.MOTHERBOARD || ___m_partType == PartDesc.Type.AIR_COOLER)
            {
                foreach (var partInstance in save.GetAllParts())
                {
                    return partInstance.IsActive() && partInstance.GetPart().MatchForUpgrade(___m_partType);
                }
            }
            return __result;
        }
        
    }
}