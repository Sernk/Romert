using Romert.Common.GlobalItems;
using Romert.Content.Reagents.Flask;
using Romert.Core;
using System;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Romert.Content.Projectiles;

public class BaseFlaskProj : ModProjectile {
    Item current = null;

    public override void SetStaticDefaults() {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }
    public override void SetDefaults() {
        Projectile.width = 18;
        Projectile.height = 28;
        Projectile.friendly = true;
        Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
        Projectile.penetrate = 1; 
        Projectile.timeLeft = 1200;
        Projectile.scale = 1f;
    }
    private void ForEachReagent(Action<AlchemistReagent> action) {
        current = Main.player[Projectile.owner].HeldItem;
        if (string.IsNullOrEmpty(current.Name)) { return; }
        if (!current.Get<AlchemicalItems>().isFlask) { return; }
        foreach (AlchemistReagent reagent in current.Get<AlchemicalItems>().FlaskReagents) {
            if (reagent == GetReagent<NoN>() || reagent == GetReagent<Look>()) { continue; }
            action(reagent);
        }
    }
    public override void OnSpawn(IEntitySource source) {
        current = Main.LocalPlayer.HeldItem;
        ForEachReagent(reagent => reagent.AlchemistProjectile.SetDefaults(Projectile));
        base.OnSpawn(source);
    }
    public override void AI() {
        ForEachReagent(reagent => reagent.AlchemistProjectile.AI(Projectile));
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
        ForEachReagent(reagent => reagent.AlchemistProjectile.OnHitNPC(Projectile, target, hit, damageDone));
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info) {
        ForEachReagent(reagent => reagent.AlchemistProjectile.OnHitPlayer(Projectile, target, info));
    }
    public override bool? CanCutTiles() {
        ForEachReagent(reagent => reagent.AlchemistProjectile.CanCutTiles(Projectile));
        return base.CanCutTiles();
    }
    public override void CutTiles() {
        ForEachReagent(reagent => reagent.AlchemistProjectile.CutTiles(Projectile));
    }
    public override void OnKill(int timeLeft) {
        ForEachReagent(reagent => reagent.AlchemistProjectile.OnKill(Projectile, timeLeft));

        SoundEngine.PlaySound(SoundID.Item107, Projectile.position);

        IEntitySource source = Projectile.GetSource_FromThis();
        for (int i = 0; i < 1; i++) { Gore.NewGore(source, new(Projectile.position.X, Projectile.position.Y + 20), -Projectile.oldVelocity * 0.2f, 704, 1f); }
        for (int i = 0; i < 1; i++) { Gore.NewGore(source, new(Projectile.position.X, Projectile.position.Y + 20), -Projectile.oldVelocity * 0.2f, 705, 1f); }
    }
}