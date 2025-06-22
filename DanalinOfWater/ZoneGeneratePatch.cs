using System.Collections.Generic;
using HarmonyLib;

namespace DanalinOfWater
{
    [HarmonyPatch(typeof(Zone), nameof(Zone.Generate))]
    internal class ZoneGeneratePatch
    {
        static void Postfix(Zone __instance)
        {
            if (EClass.rnd(20) == 0)
            {
                EClass.core.refs.crawlers.start.CrawlUntil(__instance.map, () => __instance.map.poiMap.GetCenterCell().GetCenter(), 1, delegate (Crawler.Result r)
                {
                    if (r.points.Count <= 4)
                    {
                        return false;
                    }
                    __instance.map.poiMap.OccyupyPOI(r.points[0]);
                    List<Point> points = r.points;
                    Thing thing = ThingGen.Create("altar_danalinofwater");
                    if (points[0].Installed == null)
                    {
                        EClass._zone.AddCard(thing, points[0]).Install();
                    }
                    return true;
                });
            }
        }
    }
}
