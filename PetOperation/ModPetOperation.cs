using System.IO;
using BepInEx;

using HarmonyLib;

namespace PetOperation
{
    [BepInPlugin("com.seacolorswind.pet_operation", "Pet Operation", "1.1.0")]
    public class ModPetOperation : BaseUnityPlugin
    {
        public void OnStartCore()
        {
            string dir = Path.GetDirectoryName(Info.Location);
            string excelGeneral = null;
            if (Lang.langCode == "CN")
            {
                excelGeneral = dir + "/Lang/CN/Game/General.xlsx";
            }
            else
            {
                excelGeneral = dir + "/General.xlsx";
            }
            ModUtil.ImportExcel(excelGeneral, "General", Core.Instance.sources.langGeneral);
        }
        private void Start() {
            new Harmony("PetOperation").PatchAll();
        }
    }
}
