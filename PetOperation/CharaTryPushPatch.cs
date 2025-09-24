using HarmonyLib;

using System.Collections.Generic;

using UnityEngine;

namespace PetOperation
{
    [HarmonyPatch(typeof(Chara), nameof(Chara.TryPush))]
    public class CharaTryPushPatch
    {
        static void Postfix(Chara __instance, Point point) {
            if (__instance.IsPCParty) {
                Operation operation = OperationManager.globalOperations.Find(__instance.uid);
                if (operation == null || !operation.isVanguard) {
                    return;
                }
            }
            point.Charas.ForeachReverse(delegate (Chara c)
            {
                if (c.IsPC || !c.trait.CanBePushed || c == __instance || c.noMove || (EClass._zone.IsRegion && !c.IsPCFactionOrMinion)) {
                    return;
                }
                if (c.IsPCParty) {
                    Operation operation = OperationManager.globalOperations.Find(c.uid);
                    if (operation != null && operation.isVanguard) {
                        return;
                    }
                }
                List<Point> list = new List<Point>();
                for (int i = point.x - 1; i <= point.x + 1; i++) {
                    for (int j = point.z - 1; j <= point.z + 1; j++) {
                        if (i != point.x || j != point.z) {
                            Point point2 = new Point(i, j);
                            if (point2.IsValid && !point2.HasChara && !point2.IsBlocked && !point2.cell.hasDoor && !point2.IsBlockByHeight(point)) {
                                list.Add(point2);
                            }
                        }
                    }
                }
                if (list.Count > 0) {
                    if (list.Count > 1) {
                        list.ForeachReverse(delegate (Point p)
                        {
                            if (p.Equals(new Point(point.x + point.x - __instance.pos.x, point.z + point.z - __instance.pos.z))) {
                                list.Remove(p);
                            }
                        });
                    }
                    Point newPoint = list.RandomItem<Point>();
                    __instance.Say("displace", __instance, c, null, null);
                    if (c.isSynced) {
                        c.PlayEffect("push", true, 0f, default(Vector3));
                    }
                    c.MoveByForce(newPoint, __instance, true);
                }
            });
        }
    }
}
