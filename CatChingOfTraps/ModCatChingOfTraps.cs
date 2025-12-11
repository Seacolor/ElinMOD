using BepInEx;

using HarmonyLib;

namespace CatChingOfTraps
{
    public static class ModInfo
    {
        public const string Guid = "com.seacolorswind.cat_ching_of_traps";
        public const string Name = "Cat-Ching of Traps";
        public const string Version = "1.0.0";
    }

    [BepInPlugin(ModInfo.Guid, ModInfo.Name, ModInfo.Version)]
    internal class ModCatChingOfTraps : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony(ModInfo.Guid).PatchAll();
        }
    }
}
