using HarmonyLib;

namespace CatChingOfTraps.Patches
{
    [HarmonyPatch(typeof(Card))]
    [HarmonyPatch("isHidden", MethodType.Getter)]
    internal class CardIsHiddenPatch
    {
        [HarmonyPrefix]
        static bool Prefix(Card __instance, ref bool __result)
        {
            if (__instance is Thing && __instance.trait is TraitTrap && (EClass.pc.HasElement(280345) || EClass.pc.HasElement(793475)))
            {
                __result = false;
                return false;
            }

            return true;
        }
    }
}
