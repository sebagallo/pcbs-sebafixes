using System.Collections;
using HarmonyLib;
using SebaFixes.utils;
using I2.Loc;
using System.Collections.Generic;
using SebaFixes.config;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(AddProgramApp), "Uninstall")]
    public class AddProgramApp_Uninstall_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (ConfigHandler.Instance.NoRebootUnInstallBool.Value)
            {
                return false;   
            }
            return true;
        }

        [HarmonyPostfix]
        public static IEnumerator Postfix(IEnumerator __result, AddProgramApp __instance, OSProgramDesc desc, bool ___m_dayEnded)
        {
            if (!ConfigHandler.Instance.NoRebootUnInstallBool.Value)
            {
                yield return __result;
            }
            ___m_dayEnded = false;
            SFReflect.Run("ShowProgressDialog", __instance, new[] {(object)ScriptLocalization.AddPrograms.REMOVING, desc});
            yield return SFReflect.Run<IEnumerator>("ShowProgress", __instance, new[] {(object)0f, (object)0.9f, (object)(float)((double) desc.m_removeTime * 0.2f), (object)true});
            yield return SFReflect.Run<IEnumerator>("ShowProgress", __instance, new[] {(object)0.9f, (object)0.95f, (object)(float)((double) desc.m_removeTime * 0.8f), (object)true});
            yield return SFReflect.Run<IEnumerator>("ShowProgress", __instance, new[] {(object)0.95f, (object)1f, (object)1f, (object)false});
            ComputerSoftware software = __instance.GetComponentInParent<VirtualComputer>().GetComputer().m_software;
            List<string> list = new List<string>(software.m_programs);
            list.Remove(desc.m_id);
            software.m_programs = list.ToArray();
            __instance.m_installPopup.SetActive(false);
            var os = __instance.GetComponentInParent<OS>();
            SFLog.log($"Uninstalling {desc.m_uiName} without rebooting...");
            var m_icons = SFReflect.Get<List<ProgramIcon>>("m_icons", os);
            foreach (var icon in m_icons)
            {
                if (icon.m_text.text == desc.m_uiName)
                {
                    m_icons.Remove(icon);
                    UnityEngine.Object.Destroy((UnityEngine.Object) icon.gameObject);
                    break;
                }
            }
            var m_userDescs = SFReflect.Get<List<OSProgramDesc>>("m_userDescs", os);
            foreach (var userDesc in m_userDescs)
            {
                if (userDesc.m_id == desc.m_id)
                {
                    m_userDescs.Remove(userDesc);
                    UnityEngine.Object.Destroy(userDesc);
                    break;
                }
            }
            SFReflect.Run("ShowPrograms", __instance);
            yield break;
        }
        
    }
}