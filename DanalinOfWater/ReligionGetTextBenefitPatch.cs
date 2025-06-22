using HarmonyLib;

namespace DanalinOfWater
{
    [HarmonyPatch(typeof(Religion), nameof(Religion.GetTextBenefit))]
    internal class ReligionGetTextBenefitPatch
    {
        static void Postfix(Religion __instance, ref string __result)
        {
            if (__instance.id == "cwl_danalinofwater")
            {
                __result = __instance.source.GetText("textBenefit");
            }
        }
    }
}