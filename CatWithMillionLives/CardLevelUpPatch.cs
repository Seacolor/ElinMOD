using HarmonyLib;

namespace CatWithMillionLives
{
    [HarmonyPatch(typeof(Card), nameof(Card.LevelUp))]
    internal class CardLevelUpPatch
    {
        internal static void Postfix(Card __instance)
        {
            if (__instance.HasElement(689054) && __instance.Evalue(689054) < 7 && __instance.LV >= __instance.Evalue(689054) * 5 + 10)
            {
                __instance.Chara.SetFeat(689054, __instance.Evalue(689054) + 1, msg: true);
            }
            if (__instance.IsPC)
            {
                return;
            }
            __instance.Chara.TryUpgrade();
        }
    }
}
