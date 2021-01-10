using HarmonyLib;
using SebaFixes.config;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(ComputerSave), "IsUSBDriveInserted")]
    public class ComputerSave_IsUSBDriveInserted_Patch
    {
        [HarmonyPostfix]
        public static bool Postfix(bool __result)
        {
            if (ConfigHandler.Instance.USBInsertedBool.Value)
            {
                return true;
            }
            return __result;
        }
    }
}