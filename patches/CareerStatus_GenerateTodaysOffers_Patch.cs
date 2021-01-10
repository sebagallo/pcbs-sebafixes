using HarmonyLib;
using SebaFixes.utils;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(CareerStatus), "GenerateTodaysOffers")]
    public class CareerStatusGenerateTodaysOffersPatch
    {
        [HarmonyPrefix]
        public static void Prefix()
        {
            if (CareerConstants.s_numberOffers < 7)
            {
                CareerConstants.s_numberOffers = 7;
            }
        }
        
    }
}