using HarmonyLib;

namespace CatWithMillionLives
{
    [HarmonyPatch(typeof(Chara), nameof(Chara.Die))]
    internal class CharaDiePatch
    {
        internal static void Postfix(Chara __instance, Element e = null, Card origin = null, AttackSource attackSource = AttackSource.None)
        {
            if (__instance.IsPC)
            {
                if (EClass.pc.idFaith == "cwl_cat_with_millionlives" && EClass.pc.things.Find("cats_bell") != null)
                {
                    Flag.ensurePreventDeathPanalty = true;
                }
            }
        }
    }
}