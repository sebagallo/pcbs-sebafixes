using HarmonyLib;
using SebaFixes.config;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(Shop), "FreeDeliveryCheatEnabled")]
    public class Shop_FreeDeliveryCheatEnabled_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (ConfigHandler.Instance.FreeDeliveryBool.Value)
            {
                return false;   
            }

            return true;
        }

        [HarmonyPostfix]
        public static bool Postfix(bool __result)
        {
            if (ConfigHandler.Instance.FreeDeliveryBool.Value)
            {
                return true;   
            }

            return __result;
        }
        
    }
}