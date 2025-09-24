using HarmonyLib;

using System;

namespace PetOperation
{
    [HarmonyPatch(typeof(Game), nameof(Game.Load), new Type[] { typeof(string), typeof(bool) })]
    public class GameLoadPatch
    {
        static void Postfix(string id, bool cloud) {
            string root = (cloud ? CorePath.RootSaveCloud : CorePath.RootSave) + id;
            OperationManager.globalOperations = OperationIO.LoadOperation(root);
        }
    }
}
