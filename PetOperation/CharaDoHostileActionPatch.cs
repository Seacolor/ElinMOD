using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PetOperation
{
    [HarmonyPatch(typeof(Chara), nameof(Chara.DoHostileAction))]
    public class CharaDoHostileActionPatch
    {
        static void Postfix(Chara __instance, Card _tg, bool immediate = false)
        {
            if (_tg == null || !_tg.isChara)
            {
                return;
            }
            Chara chara = _tg.Chara;
            if ((__instance.IsPCFaction || __instance.IsPCFactionMinion) && (chara.IsPCFaction || chara.IsPCFactionMinion))
            {
                return;
            }
            if (!__instance.IsPC || chara.hostility > Hostility.Enemy)
            {
                return;
            }
            foreach (Chara member in EClass.pc.party.members)
            {
                if (member == EClass.pc)
                {
                    continue;
                }
                Operation operation = OperationManager.globalOperations.Find(member.uid);
                if (operation == null || operation.targetEnemy != 2)
                {
                    continue;
                }
                if (member.enemy != chara)
                {
                    member.SetEnemy(chara);
                }
            }
        }
    }
}
