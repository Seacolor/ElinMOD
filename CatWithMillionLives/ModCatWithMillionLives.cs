using BepInEx;
using HarmonyLib;

namespace CatWithMillionLives
{
    [BepInPlugin("com.seacolorswind.cat_with_million_lives", "Cat with Million lives", "1.1.0")]
    public class ModCatWithMillionLives : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony("com.seacolorswind.cat_with_million_lives").PatchAll();
        }
    }
}
