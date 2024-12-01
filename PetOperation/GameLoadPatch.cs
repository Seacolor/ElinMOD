using HarmonyLib;

using System;

namespace PetOperation
{
    [HarmonyPatch(typeof(Game), nameof(Game.Load), new Type[] { typeof(string), typeof(string) })]
    public class GameLoadPatch
    {
        static void Postfix(string id, string root) {
            OperationManager.globalOperations = OperationIO.LoadOperation(root);
        }
    }
}
