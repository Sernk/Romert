using Romert.Content.Projectiles.Alchemist;
using Romert.Core;

namespace Romert.Content.Reagents.Proj;

public class Lava : ReagentProjectile {
    public override void OnKill(Projectile projectile, int timeLeft) {
        Projectile.NewProjectile(projectile.GetSource_FromThis(), new(projectile.position.X, projectile.position.Y + 20), Vector2.Zero, ProjectileType<SlomeBoble_Poison>(), 1, 0);
    }
    //public override void AI(Projectile projectile) {
    //    Projectile.NewProjectile(projectile.GetSource_FromThis(), new(projectile.position.X, projectile.position.Y + 26), projectile.velocity, ProjectileID.RocketFireworkRed, 1, 0);
    //}


}