using HarmonyLib;
using SebaFixes.config;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(ComponentDust), "ReducePartInstanceDust")]
    public class ComponentDustReducePartInstanceDustPatch
    {
        [HarmonyPrefix]
        private static void Prefix(ref float partReduceAmount)
        {
            if (ConfigHandler.Instance.FasterVacuumBool.Value)
            {
                var vacuumSpeed = ConfigHandler.Instance.FasterVacuumValue.Value;
                if (partReduceAmount < vacuumSpeed)
                {
                    partReduceAmount = vacuumSpeed;
                }   
            }
        }
        
    }
}