using BepInEx;

using HarmonyLib;

namespace DanalinOfWater
{
    [BepInPlugin("com.seacolorswind.danalin_of_water", "Danalin of Water", "1.0.1")]
    public class ModDanalinOfWater : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony("com.seacolorswind.danalin_of_water").PatchAll();
        }
    }
}
