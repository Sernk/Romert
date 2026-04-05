using Romert.Content.Projectiles.Alchemist;
using Romert.Core;

namespace Romert.Content.Reagents.Proj;
public class Slime : ReagentProjectile {
    public override void OnKill(Projectile projectile, int timeLeft) {
        Projectile.NewProjectile(projectile.GetSource_FromThis(), new(projectile.position.X, projectile.position.Y + 20), Vector2.Zero, ProjectileType<SlimeBubble>(), 1, 0);
    }


}