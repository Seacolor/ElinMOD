using HarmonyLib;

using System;

namespace PetOperation
{
    [HarmonyPatch(typeof(Game), nameof(Game.Load), new Type[] { typeof(string), typeof(bool) })]
    internal class GameLoadPatch
    {
        static void Postfix(string id, bool cloud) {
            foreach (Chara chara in EClass.game.cards.globalCharas.Values)
            {
                if (chara.idFaith == "cwl_danalinofwater")
                {
                    chara.RefreshFaithElement();
                }
            }
        }
    }
}
