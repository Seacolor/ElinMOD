using System;
using System.IO;
using BepInEx;
using Steamworks;

using HarmonyLib;

namespace PetOperation
{
    [BepInPlugin("com.seacolorswind.pet_operation", "Pet Operation", "1.1.2")]
    public class ModPetOperation : BaseUnityPlugin
    {
        public void OnStartCore()
        {
            string dir = Path.GetDirectoryName(Info.Location);
            string excelGeneral = null;
            string langCode = "JP";
            if (Core.Instance.config != null)
            {
                langCode = Core.Instance.config.lang;
            }
            else
            {
                string currentGameLanguage = SteamApps.GetCurrentGameLanguage();
                if (currentGameLanguage == "chinese")
                {
                    langCode = "CN";
                }
            }
            if (langCode == "CN")
            {
                excelGeneral = $"{dir}{Path.DirectorySeparatorChar}CN{Path.DirectorySeparatorChar}Game{Path.DirectorySeparatorChar}General.xlsx";
            }
            else
            {
                excelGeneral = $"{dir}{Path.DirectorySeparatorChar}General.xlsx";
            }
            ModUtil.ImportExcel(excelGeneral, "General", Core.Instance.sources.langGeneral);
        }
        private void Start() {
            new Harmony("PetOperation").PatchAll();
        }
    }
}
