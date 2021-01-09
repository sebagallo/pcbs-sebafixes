using HarmonyLib;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(ComponentDust), "ReducePartInstanceDust")]
    public class ComponentDustReducePartInstanceDustPatch
    {
        [HarmonyPrefix]
        private static void Prefix(ref float partReduceAmount)
        {
            if (partReduceAmount < 0.20f)
            {
                partReduceAmount = 0.20f;
            }
        }
        
    }
}