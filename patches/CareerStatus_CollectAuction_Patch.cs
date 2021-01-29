using System;
using HarmonyLib;
using SebaFixes.config;
using SebaFixes.utils;

namespace SebaFixes.patches
{
    [HarmonyPatch(typeof(CareerStatus), "CollectAuction")]
    public class CareerStatus_CollectAuction_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(CareerStatus __instance, Auction auction)
        {
            if (ConfigHandler.Instance.AuctionGiveKudosBool.Value)
            {
                if (auction.m_item.GetPart().m_type == PartDesc.Type.CASE)
                {
                    SoundPlayer.Get().Play2DSound(SoundPlayer.Get().m_collectSound);
                    var kudos = (int)(auction.m_bid * ConfigHandler.Instance.AuctionGiveKudosValue.Value);
                    SFLog.log($"Added {kudos.ToString()} Kudos!");
                    __instance.AddKudos(kudos);
                }
            }
        }
    }
}