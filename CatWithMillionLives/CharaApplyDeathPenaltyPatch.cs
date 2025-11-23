using HarmonyLib;

namespace CatWithMillionLives
{
    [HarmonyPatch(typeof(Chara), nameof(Chara.ApplyDeathPenalty))]
    internal class CharaApplyDeathPenaltyPatch
    {
        internal static bool Prefix(Chara __instance)
        {
            if (Flag.ensurePreventDeathPanalty)
            {
                Msg.Say("noDeathPenaltyCatsBell", __instance);
                Flag.ensurePreventDeathPanalty = false;
                return false;
            }
            return true;
        }
    }
}
