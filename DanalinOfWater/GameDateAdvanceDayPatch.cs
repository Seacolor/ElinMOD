using HarmonyLib;

namespace DanalinOfWater
{
    [HarmonyPatch(typeof(GameDate), nameof(GameDate.AdvanceDay))]
    internal class GameDateAdvanceDayPatch
    {
        static void Postfix()
        {
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
