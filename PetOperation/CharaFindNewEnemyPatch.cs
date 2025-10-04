using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PetOperation
{
    [HarmonyPatch(typeof(Chara), nameof(Chara.FindNewEnemy))]
    public class CharaFindNewEnemyPatch
    {
        static void Postfix(Chara __instance, ref bool __result)
        {
            if (__instance.IsPC || !__instance.IsPCParty || EClass._zone.isPeace)
            {
                return;
            }
            if (EClass.pc.isHidden && __instance.isHidden)
            {
                return;
            }
            bool flag = __instance.enemy == null;
            Operation operation = OperationManager.globalOperations.Find(__instance.uid);
            if (operation == null || operation.targetEnemy != 1)
            {
                return;
            }
            List<Chara> list = new List<Chara>();
            foreach (Chara member in EClass.pc.party.members)
            {
                if (member.enemy != null && member.enemy.IsAliveInCurrentZone)
                {
                    list.Add(member.enemy);
                }
            }
            Chara chara = null;
            int num2 = 9999;
            for (int i = 0; i < list.Count; i++)
            {
                Chara chara2 = list[i];
                if (chara2 == __instance || !__instance.IsHostile(chara2) || !__instance.CanSee(chara2))
                {
                    continue;
                }
                int num3 = __instance.Dist(chara2);
                if (__instance.CanSeeLos(chara2) && (!EClass.game.config.tactics.dontWander || EClass.pc.isBlind || EClass.pc.CanSeeLos(chara2)) && EClass.pc.ai.ShouldAllyAttack(chara2))
                {
                    if (num3 < num2)
                    {
                        num2 = num3;
                        chara = chara2;
                    }
                }
            }
            if (chara != null)
            {
                if (flag)
                {
                    __instance.DoHostileAction(chara);
                }
                __instance.enemy = chara;
                __result = true;
            }
        }
    }
}
