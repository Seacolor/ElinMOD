using System;
using HarmonyLib;

namespace DanalinOfWater
{
    [HarmonyPatch]
    internal static class StatsPatches
    {
        [HarmonyPatch(typeof(Stats), nameof(Stats.Set), new Type[] { typeof(int) })]
        [HarmonyPostfix]
        static void Post_Set(Stats __instance, int a)
        {
            DoPostProcess(__instance);
        }

        [HarmonyPatch(typeof(Stats), nameof(Stats.Mod))]
        [HarmonyPostfix]
        static void Post_Mod(Stats __instance, int a)
        {
            DoPostProcess(__instance);
        }

        static void DoPostProcess(Stats stat)
        {
            if (stat?.id != Stats.SAN.id) return;

            if (!(stat.Owner is Chara chara)) return;

            stat.Owner.RefreshFaithElement();
        }
    }
}