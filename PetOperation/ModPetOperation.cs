using BepInEx;

using HarmonyLib;

namespace PetOperation
{
    [BepInPlugin("com.seacolorswind.pet_operation", "Pet Operation", "1.0.2")]
    public class ModPetOperation : BaseUnityPlugin
    {
        private void Start() {
            new Harmony("PetOperation").PatchAll();
        }
    }
}
