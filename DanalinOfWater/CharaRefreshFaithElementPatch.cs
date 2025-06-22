using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace DanalinOfWater
{
    [HarmonyPatch(typeof(Chara), nameof(Chara.RefreshFaithElement))]
    internal class CharaRefreshFaithElementPatch
    {
        static void Postfix(Chara __instance)
        {
            if (!EClass.core.IsGameStarted)
            {
                return;
            }
            if (__instance.idFaith != "cwl_danalinofwater")
            {
                return;
            }
            if (__instance.HasCondition<ConExcommunication>())
            {
                return;
            }
            // Bonus for Insanity
            int bonusSAN = 30 * (__instance.GetPietyValue() * (120 + __instance.Evalue(1407) * 15) / 100) / 50;
            if (bonusSAN >= 50)
            {
                bonusSAN = 50;
            }
            if (bonusSAN > __instance.SAN.GetValue())
            {
                __instance.SAN.Set(bonusSAN);
            }
            int SAN = __instance.SAN.GetValue();
            // Bonuses for random based on insanity
            Rand.SetSeed(EClass.world.date.GetRawDay());
            IList<SourceElement.Row> elementList = EClass.sources.elements.rows.Where((SourceElement.Row e) => e.type == "AttbMain" || (e.group == "SKILL" && e.category == "skill" && e.id != 306)).ToList().Shuffle();
            int[] array = new int[10];
            for (int i = 0; i < 5; i++)
            {
                SourceElement.Row row = elementList.ElementAt(i);
                int skill = row.id;
                int bonus = 1 + Rand.rnd(20);
                array[i * 2] = skill;
                array[i * 2 + 1] = bonus;
            }
            Rand.SetSeed();
            int num = SAN * 120 / 100;
            for (int i = 0; i < array.Length; i += 2)
            {
                int num2 = array[i + 1] * num / 50;
                if (array[i] == 79)
                {
                    num2 = EClass.curve(num2, array[i + 1] * 2, 10, 50);
                }
                if (num2 >= 20 && array[i] >= 950 && array[i] < 970)
                {
                    num2 = 20;
                }
                __instance.faithElements.SetBase(array[i], Mathf.Max(num2, 1));
            }
            if (SAN >= 10)
            {
                __instance.faithElements.SetBase(ENC.revealFaith, 1);
            }
            if (SAN >= 30)
            {
                __instance.faithElements.SetBase(ENC.seeInvisible, 1);
            }
            __instance.faithElements.SetParent(__instance);
        }
    }
}
