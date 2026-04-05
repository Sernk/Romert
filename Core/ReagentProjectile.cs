namespace Romert.Core;

public class ReagentProjectile : CleaningType {
    public string Name => GetType().Name;

    public virtual void SetDefaults(Projectile projectile) { }
    public virtual void AI(Projectile projectile) { }
    public virtual void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone) { }
    public virtual void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info) { }
    public virtual bool? CanCutTiles(Projectile projectile) => null;
    public virtual void CutTiles(Projectile projectile) { }
    public virtual void OnKill(Projectile projectile, int timeLeft) { }

    public override string ToString() => Name;
    public override void Load(Mod mod) {
        ReagentProjectileManager.Projectiles.Add(this);
        ReagentProjectileManager.ProjectileData.Add(Name, this);
    }
}