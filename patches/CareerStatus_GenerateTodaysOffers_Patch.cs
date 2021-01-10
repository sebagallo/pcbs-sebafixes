using HarmonyLib;
using SebaFixes.config;
using SebaFixes.utils;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(CareerStatus), "GenerateTodaysOffers")]
    public class CareerStatusGenerateTodaysOffersPatch
    {
        [HarmonyPrefix]
        public static void Prefix()
        {
            if (ConfigHandler.Instance.MoreOffersBool.Value)
            {
                var offerNumber = ConfigHandler.Instance.MoreOffersValue.Value;
                if (CareerConstants.s_numberOffers < offerNumber)
                {
                    CareerConstants.s_numberOffers = offerNumber;
                }
            }
            else
            {
                CareerConstants.s_numberOffers = 4;
            }
        }
        
    }
}