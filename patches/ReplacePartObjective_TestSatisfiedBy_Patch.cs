using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(ReplacePartObjective), "TestSatisifiedBy")]
    public class ReplacePartObjectiveTestSatisfiedByPatch
    {

        [HarmonyPrefix]
        private static bool Prefix()
        {
            return false;
        }

        [HarmonyPostfix]
        private static bool Postfix(bool __result, ReplacePartObjective __instance, Job job, ComputerSave save, string ___m_refPart, PartDesc.Type ___m_partType, int ___m_refGB, int ___m_minRamRating, int ___m_totalPartsOfType)
        {
            Debug.Log("[SF] Test PART");
            if (___m_refPart == null)
                return true;
            if (___m_partType == PartDesc.Type.RAM)
                return save.GetRAM() >= ___m_refGB && save.GetMinRAMRating() >= ___m_minRamRating;
            if (((IEnumerable<PartDesc.Type>) PartDesc.s_storageTypes).Contains<PartDesc.Type>(___m_partType))
                return save.GetStorage() >= ___m_refGB;
            if (___m_partType == PartDesc.Type.MOTHERBOARD)
                return true;
            if (___m_partType == PartDesc.Type.AIR_COOLER)
                return true;
            PartDesc refPart = PartsDatabase.GetDesc(___m_refPart, (GetString) null);
            return save.GetAllParts().Count<PartInstance>((Func<PartInstance, bool>) (x => x != null && x.IsActive() && x.GetPart().MatchForUpgrade(refPart.m_type) && (double) x.GetPart().GetQuality() >= (double) refPart.GetQuality())) == ___m_totalPartsOfType;
        }
        
    }
}