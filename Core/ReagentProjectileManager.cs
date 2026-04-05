using Romert.Content.Reagents.Proj;
using System.Collections.Generic;

namespace Romert.Core;

public class ReagentProjectileManager : CleaningType {
    public static List<ReagentProjectile> Projectiles { get; private set; } = [];
    public static Dictionary<string, ReagentProjectile> ProjectileData { get; private set; } = [];
    public override void Load(Mod mod) {
        if (AlchemistReagentManager.PostLoad) {

        }
    }
}