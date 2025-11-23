using HarmonyLib;

namespace CatWithMillionLives
{
    [HarmonyPatch(typeof(DNA), nameof(DNA.CanRemove))]
    internal class DNACanRemovePatch
    {
        internal static void Postfix(DNA __instance, ref bool __result)
        {
            for (int i = 0; i < __instance.vals.Count; i += 2)
            {
                int num = __instance.vals[i];
                if (num == 689054)
                {
                    __result = false;
                }
            }
        }
    }
}
