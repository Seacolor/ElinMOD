using HarmonyLib;
using UnityEngine;

namespace CatChingOfTraps.Patches
{
    [HarmonyPatch(typeof(MapGenDungen), "OnGenerateTerrain")]
    internal class MapGenDungenOnGenerateTerrainPatch
    {
        [HarmonyPostfix]
        static void Postfix(MapGenDungen __instance, bool __result)
        {
            if (!__result || !EClass.pc.HasElement(280345))
            {
                return;
            }

            int num5 = EClass.rnd(__instance.Size * __instance.Size / 50 + EClass.rnd(20)) + 5;
            num5 = num5 * Mathf.Min(20 + __instance.zone.DangerLv * 5, 100) / 100;
            if (__instance.zone is Zone_RandomDungeonNature)
            {
                num5 /= 5;
            }
            Point point = null;
            for (int num6 = 0; num6 < num5; num6++)
            {
                point = EClass._map.GetRandomPoint();
                if (!point.cell.isModified && !point.HasThing && !point.HasBlock && !point.HasObj)
                {
                    Thing t2 = ThingGen.CreateFromCategory("trap", __instance.zone.DangerLv);
                    EClass._zone.AddCard(t2, point).Install();
                }
            }
        }
    }
}