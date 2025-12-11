using HarmonyLib;

namespace CatChingOfTraps.Patches
{
    [HarmonyPatch(typeof(TraitFloorSwitch), nameof(TraitFloorSwitch.OnStepped))]
    internal class TraitFloorSwitchOnSteppedPatch
    {
        [HarmonyPrefix]
        static bool Prefix(TraitFloorSwitch __instance, Chara c)
        {
            if (__instance is TraitTrap && c.HasElement(793475))
            {
                return false;
            }

            return true;
        }
    }
}
