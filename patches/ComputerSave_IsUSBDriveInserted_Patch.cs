using HarmonyLib;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(ComputerSave), "IsUSBDriveInserted")]
    public class ComputerSave_IsUSBDriveInserted_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            return false;
        }

        [HarmonyPostfix]
        public static bool Postfix(bool __result)
        {
            return true;
        }
    }
}