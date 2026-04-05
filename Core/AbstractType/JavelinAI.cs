using System;
using System.Collections.Generic;
using Terraria.Audio;

namespace Romert.Core.AbstractType {
    public abstract class JavelinAI : ModProjectile {
        public bool destroyOutside = true;

        public bool stickDealDamage;

        public int StickTime = 900;

        public Point[] stickingJavelins = new Point[1];

        public int GravityDelay = 45;

        public float velXmult = 0.98f;

        public float velYadd = 0.35f;

        public bool IsStickingToTarget {
            get { return Projectile.ai[0] == 1f; }
            set { Projectile.ai[0] = value ? 1f : 0f; }
        }

        public int TargetWhoAmI {
            get { return (int)Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }

        public int GravityDelayTimer {
            get { return (int)Projectile.ai[2]; }
            set { Projectile.ai[2] = value; }
        }

        public float StickTimer {
            get { return Projectile.localAI[0]; }
            set { Projectile.localAI[0] = value; }
        }

        public override void SetDefaults() {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.alpha = 255;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.hide = true;
        }
        public override void AI() {
            AI2();
            CustomAI();
        }
        public void AI2() {
            UpdateAlpha();
            if (IsStickingToTarget) { StickyAI(); }
            else { NormalAI(); }
        }
        void NormalAI() {
            GravityDelayTimer++;
            if (GravityDelayTimer >= GravityDelay) {
                GravityDelayTimer = GravityDelay;
                Projectile.velocity.X *= velXmult;
                Projectile.velocity.Y += velYadd;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }
        void StickyAI() {
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            StickTimer += 1f;
            bool flag = StickTimer % 30f == 0f;
            int targetWhoAmI = TargetWhoAmI;
            if (StickTimer >= (float)StickTime || targetWhoAmI < 0 || targetWhoAmI >= 200) { Projectile.Kill(); }
            else if (Main.npc[targetWhoAmI].active && !Main.npc[targetWhoAmI].dontTakeDamage) {
                Projectile.Center = Main.npc[targetWhoAmI].Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = Main.npc[targetWhoAmI].gfxOffY;
                if (flag) { Main.npc[targetWhoAmI].HitEffect(0, 1.0); }
            }
            else { Projectile.Kill(); }
        }
        public override void OnKill(int timeLeft) {
            SoundEngine.PlaySound(in SoundID.Dig, Projectile.position);
            Vector2 position = Projectile.position;
            Vector2 vector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
            _ = position + vector * 16f;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            int num = 0;
            for (int i = 0; i < 1000; i++) {
                if (i != target.whoAmI && Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == base.Type && Main.projectile[i].ai[0] == 1f && Main.projectile[i].ai[1] == (float)target.whoAmI) {
                    stickingJavelins[num++] = new Point(i, Main.projectile[i].timeLeft);
                    if (num >= stickingJavelins.Length) {
                        break;
                    }
                }
            }

            modifiers.FinalDamage *= (float)Math.Pow(1.25, num);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            IsStickingToTarget = true;
            TargetWhoAmI = target.whoAmI;
            Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
            Projectile.netUpdate = true;
            Projectile.damage = 0;
            //target.AddBuff(ModContent.BuffType<JavelinDebuff>(), 900);
            Projectile.KillOldestJavelin(Projectile.whoAmI, base.Type, target.whoAmI, stickingJavelins);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac) {
            width = (height = 10);
            return true;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8) {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }

            return projHitbox.Intersects(targetHitbox);
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI) {
            if (IsStickingToTarget) {
                int targetWhoAmI = TargetWhoAmI;
                if (targetWhoAmI >= 0 && targetWhoAmI < 200 && Main.npc[targetWhoAmI].active) {
                    if (Main.npc[targetWhoAmI].behindTiles) { behindNPCsAndTiles.Add(index); }
                    else { behindNPCsAndTiles.Add(index); }
                    return;
                }
            }
            behindNPCsAndTiles.Add(index);
        }
        private void UpdateAlpha() {
            if (Projectile.alpha > 0) { Projectile.alpha -= 25; }
            if (Projectile.alpha < 0) { Projectile.alpha = 0; }
        }
        public virtual void CustomAI() { }
    }
}
