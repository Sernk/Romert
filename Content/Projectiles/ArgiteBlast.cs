namespace Romert.Content.Projectiles;

public class ArgiteBlast : ModProjectile {
	public override void SetStaticDefaults() => Main.projFrames[Projectile.type] = 5;

    public override void SetDefaults() {
		Projectile.timeLeft = 200;
		Projectile.width = 52;	
		Projectile.height = 52;
        Projectile.hostile = true;   
        Projectile.friendly = true; 
        Projectile.penetrate = -1;
	}
	public override Color? GetAlpha(Color lightColor) => Color.White;
    public override void AI() {
		Projectile.frameCounter++;
		if (Projectile.frameCounter > 2) {
			Projectile.frame++;
			Projectile.frameCounter = 0;
		}
		if (Projectile.frame >= 5) { Projectile.Kill(); }
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) { if (Main.rand.NextBool(2)) { target.AddBuff(BuffID.Poisoned, 300, false); } }
    public override void OnHitPlayer(Player target, Player.HurtInfo info) { if (Main.rand.NextBool(2)) { target.AddBuff(BuffID.Poisoned, 300, false); } }
}