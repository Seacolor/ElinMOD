using HarmonyLib;

namespace PetOperation
{
    [HarmonyPatch(typeof(Chara), nameof(Chara.CanReplace))]
    public class CharaCanReplacePatch
    {
        static void Postfix(Chara __instance, Chara c, ref bool __result) {
            if (__instance.IsPC || c.IsPC || !__instance.IsPCParty || !c.IsPCParty) {
                return;
            }
            Operation o1 = OperationManager.globalOperations.Find(__instance.uid);
            if (o1 == null || !o1.isVanguard) {
                return;
            }
            Operation o2 = OperationManager.globalOperations.Find(c.uid);
            if (o2 != null && o2.isVanguard) {
                return;
            }
            __result = true;
        }
    }
}
