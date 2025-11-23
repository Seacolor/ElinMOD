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
                    (thing.trait as TraitAltar)?.SetDeity("cwl_cat_with_millionlives");
                    Chara t = CharaGen.Create("cat_wild");
                    EClass._zone.AddCard(t, points.RandomItem());
                    for (int i = 0; i < 2 + EClass.rnd(2); i++)
                    {
                        Chara t2 = CharaGen.Create("cat_wild");
                        EClass._zone.AddCard(t2, points.RandomItem());
                    }
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
