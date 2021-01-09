using HarmonyLib;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(ReplacePartObjective), "TestSatisifiedBy")]
    public class ReplacePartObjectiveTestSatisfiedByPatch
    {

        [HarmonyPostfix]
        private static bool Postfix(bool __result, PartDesc.Type ___m_partType)
        {
            if (___m_partType == PartDesc.Type.MOTHERBOARD)
                return true;
            if (___m_partType == PartDesc.Type.AIR_COOLER)
                return true;
            return __result;
        }
        
    }
}