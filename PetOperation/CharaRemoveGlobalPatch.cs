using HarmonyLib;

namespace PetOperation
{
    [HarmonyPatch(typeof(Chara), nameof(Chara.RemoveGlobal))]
    public class CharaRemoveGlobalPatch
    {
        static void Prefix(Chara __instance)
        {
            if (!__instance.IsGlobal || __instance.trait is TraitUniqueChara || __instance.IsUnique || EClass.game.cards.listAdv.Contains(__instance))
            {
                return;
            }
            OperationManager.globalOperations.Remove(__instance.Chara);
        }
    }
}
