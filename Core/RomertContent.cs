using Newtonsoft.Json.Linq;

namespace Romert.Core;

public class RomertContent {
    public static T GetReagent<T>() where T : AlchemistReagent => (T)(AlchemistReagentManager.ReagentID.TryGetValue(typeof(T).Name, out var value) ? value : null);
    public static AlchemistReagent GetReagent(string name) => AlchemistReagentManager.ReagentID.TryGetValue(name, out var value) ? value : null;
    public static T GetReagentRarity<T>() where T : ReagentRarity => (T)(ReagentRarityManager.Rarities.TryGetValue(typeof(T).Name, out ReagentRarity value) ? value : null);
    public static ReagentRarity GetReagentRarity(string name) => ReagentRarityManager.Rarities.TryGetValue(name, out ReagentRarity value) ? value : null;
    public static T GetReagentProjectile<T>() where T : ReagentProjectile => (T)(ReagentProjectileManager.ProjectileData.TryGetValue(typeof(T).Name, out ReagentProjectile value) ? value : null);
    public static ReagentProjectile GetReagentProjectile(string name) => ReagentProjectileManager.ProjectileData.TryGetValue(name, out ReagentProjectile value) ? value : null;
}