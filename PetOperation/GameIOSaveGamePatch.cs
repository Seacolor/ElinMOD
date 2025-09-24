using HarmonyLib;

using Newtonsoft.Json;

using System.IO;

namespace PetOperation
{
    [HarmonyPatch(typeof(GameIO), nameof(GameIO.SaveGame))]
    public class GameIOSaveGamePatch
    {
        static void Prefix() {
            string text = JsonConvert.SerializeObject(OperationManager.globalOperations, GameIO.formatting, GameIO.jsWriteGame);
            string path = GameIO.pathCurrentSave + "operation.txt";
            if (GameIO.compressSave) {
                IO.Compress(path, text);
            } else {
                File.WriteAllText(path, text);
            }
        }
    }
}
