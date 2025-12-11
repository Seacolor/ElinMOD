using HarmonyLib;
using System.Collections.Generic;

namespace CatWithMillionLives
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
                    Thing thing = ThingGen.Create("altar");
                    (thing.trait as TraitAltar)?.SetDeity("cwl_cat_ching_of_traps");
                    if (points[0].Installed == null)
                    {
                        EClass._zone.AddCard(thing, points[0]).Install();
                    }
                    foreach (Point item in points)
                    {
                        if (item.x % 3 == 0 && item.z % 2 == 0 && item != points[0] && !item.Equals(points[0].Front) && item.Installed == null)
                        {
                            thing = ThingGen.Create("rock");
                            EClass._zone.AddCard(thing, item).Install();
                        }
                        item.SetObj();
                        item.SetFloor(3, 6);
                    }
                    return true;
                });
            }
        }
    }
}
