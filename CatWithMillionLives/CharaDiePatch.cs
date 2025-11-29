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
                Thing c = EClass.pc.things.Find("cats_bell");
                if (c != null && !c.c_idDeity.IsEmpty() && c.c_idDeity == EClass.pc.idFaith)
                {
                    Flag.ensurePreventDeathPanalty = true;
                }
            }
        }
    }
}