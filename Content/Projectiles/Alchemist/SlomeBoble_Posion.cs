namespace Romert.Content.Projectiles.Alchemist;

public class SlomeBoble_Poison : ModProjectile {
    public override void SetDefaults() {
        Projectile.hostile = false;
        Projectile.friendly = false;
        Projectile.tileCollide = false;
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.timeLeft = 180;
    }
    public override void AI() {
        if (Projectile.ai[2] != 0f) {
            if (Projectile.ai[2] > 0) { 
                NPC npc = Main.npc[(int)Projectile.ai[2] - 1];
                if (npc.active) {
                    Projectile.position += (npc.position - npc.oldPosition) / Projectile.MaxUpdates;
                    Projectile.ai[0]++;
                    if (Projectile.ai[0] % 25 == 0) {
                        npc.AddBuff(BuffID.Poisoned, 45); Projectile.ai[0] = 0;
                    }
                    //run whatever you want here
                    //npc.AddBuff(debuffIdGoesHere, debuffTimeGoesHere); since you said it should inflict a debuff
                }
                else {
                    Projectile.timeLeft = 0;
                    //up to you to decide what happens when the entity it is attached to dies or despawns
                    //Projectile.ai[2] = 0f; will reset the projectile, allowing it to stick again
                }
            }
            else if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height)) {
                //up to you to decide what happens when the block it is attached to gets broken
                //Projectile.ai[2] = 0f; will reset the projectile, allowing it to stick again
            }
            return;
        }
        if (Main.myPlayer == Projectile.owner) {
            foreach (NPC npc in Main.ActiveNPCs) if (npc.CanBeChasedBy(Projectile, false) && Projectile.Hitbox.Intersects(npc.Hitbox)) {
                Projectile.ai[2] = npc.whoAmI + 1;
                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, Projectile.whoAmI);
            }
            if (Projectile.ai[2] == 0f && Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height)) {
                Projectile.ai[2] = -1f;
                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, Projectile.whoAmI);
            }
        }
        //normal projectile behavior goes here
    }
    public override bool ShouldUpdatePosition() => Projectile.ai[2] == 0f;
    public override bool PreDraw(ref Color lightColor) {
       
        return true;
    }
    public override void OnKill(int timeLeft) {
        Vector2 usePos = Projectile.position;
        Vector2 rotVector = Utils.ToRotationVector2(Projectile.rotation - MathHelper.ToRadians(90f));
        usePos += rotVector * 16f;

        for (int i = 0; i < 5; i++)
            Dust.NewDust(usePos, Projectile.width, Projectile.height, DustID.Poisoned, 0f, 0f, 0, default, 1.2f);
    }
}