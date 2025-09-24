using HarmonyLib;

using System.Collections.Generic;

namespace PetOperation
{
    [HarmonyPatch(typeof(ActPlan), nameof(ActPlan.Update))]
    public class ActPlanPatch
    {
        static void Postfix(ActPlan __instance) {
            if (ActPlan.warning)
            {
                return;
            }
            if (!__instance.pos.IsValid || EClass.pc.isDead)
            {
                return;
            }
            Point _pos = new Point(__instance.pos);
            List<Card> items = _pos.ListCards(false);
            bool isKey = __instance.input == ActInput.Key;
            if (EClass.pc.isBlind && !_pos.Equals(EClass.pc.pos) && !isKey && __instance.input != ActInput.LeftMouse)
            {
                return;
            }
            if (!isKey && __instance.input != ActInput.LeftMouse && __instance.input != ActInput.AllAction)
            {
                return;
            }
            if (EClass.ui.IsDragging)
            {
                return;
            }
            if (_pos.cell.outOfBounds)
            {
                return;
            }
            items.ForeachReverse(delegate (Card _c) {
                Chara chara = _c.Chara;
                if (chara == null || chara.IsPC || !EClass.pc.CanSee(chara) || !chara.IsPCParty)
                {
                    return;
                }
                int num = chara.Dist(EClass.pc);
                if (num > 1 && EClass.pc.isBlind)
                {
                    return;
                }
                if (!EClass.pc.isBlind && !chara.IsHostile() && __instance.input == ActInput.AllAction && chara.isSynced && num <= 2)
                {
                    bool flag3 = !chara.HasCondition<ConSuspend>() && (!chara.isRestrained || !chara.IsPCFaction);
                    if (EClass._zone.instance is ZoneInstanceMusic && !chara.IsPCFactionOrMinion)
                    {
                        flag3 = false;
                    }
                    if (flag3 || __instance.altAction)
                    {
                        Operation o = OperationManager.globalOperations.Find(chara.uid);
                        if (o == null || !o.isVanguard)
                        {
                            __instance.TrySetAct(OACT.OrderVanguard, chara);
                        }
                        else
                        {
                            __instance.TrySetAct(OACT.OrderRearGuard, chara);
                        }
                    }
                }
            });
        }
    }
}
