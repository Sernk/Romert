using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;

namespace Romert
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class Romert : Mod
	{
        Hook roa;

        delegate bool orig_IsBiomeActive(object type, Player player);
        delegate bool orig_GetBiomeActive(orig_IsBiomeActive orig_GetBiomeActive, object type, Player player);

        public override void Load() {
			if (ModLoader.TryGetMod("RoA", out Mod RoAMod)) {
                //RoA.Content.Biomes.Backwoods
                Type BackwoodsBiomeClass = RoAMod.GetType().Assembly.GetType("RoA.Content.Biomes.Backwoods.BackwoodsBiome");
                MethodInfo targetMethod = BackwoodsBiomeClass.GetMethod("IsBiomeActive", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                roa = new Hook(targetMethod, (orig_GetBiomeActive)RoAIsBiomeActive);
            }
        }
        bool RoAIsBiomeActive(orig_IsBiomeActive orig, object type, Player player) {
            return false;
        }
    }
}