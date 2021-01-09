using System.Collections;
using HarmonyLib;
using SebaFixes.utils;
using I2.Loc;
using System.Collections.Generic;
using UnityEngine;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(AddProgramApp), "Install")]
    public class AddProgramApp_Install_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            return false;
        }
        
        [HarmonyPostfix]
        public static IEnumerator Postfix(IEnumerator __result, AddProgramApp __instance, OSProgramDesc desc, bool ___m_dayEnded)
        {
            ___m_dayEnded = false;
            SFReflect.Run("ShowProgressDialog", __instance, new[] {(object)ScriptLocalization.AddPrograms.INSTALLING, desc});
            yield return SFReflect.Run<IEnumerator>("ShowProgress", __instance, new[] {(object)0f, (object)0.9f, (object)(float)((double) desc.m_installTime * 0.4f), (object)true});
            yield return SFReflect.Run<IEnumerator>("ShowProgress", __instance, new[] {(object)0.9f, (object)0.95f, (object)(float)((double) desc.m_installTime * 0.6f), (object)true});
            yield return SFReflect.Run<IEnumerator>("ShowProgress", __instance, new[] {(object)0.95f, (object)1f, (object)1f, (object)false});
            ComputerSoftware software = __instance.GetComponentInParent<VirtualComputer>().GetComputer().m_software;
            software.m_programs = new List<string>(software.m_programs)
            {
                desc.m_id
            }.ToArray();
            __instance.m_installPopup.SetActive(false);
            var os = __instance.GetComponentInParent<OS>();
            SFLog.log($"Installing {desc.m_uiName} without rebooting...");
            SFReflect.Run("AddProgramIcon", os, Utils.fixArray(desc));
            var m_userDescs = SFReflect.Get<List<OSProgramDesc>>("m_userDescs", os);
            m_userDescs.Add(desc);
            SFReflect.Run("ShowPrograms", __instance);
            SFReflect.Run("BringToFront", os, Utils.fixArray(__instance.GetComponentInParent<WindowFrame>()));
            yield break;
        }
        
    }
}