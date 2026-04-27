using ReLogic.Content;
using System;

namespace Romert.Content.Tiles {
	public class ArgiteOreTile: ModTile
    {
        private Texture2D glowTexture;

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.JungleSpore;
            HitSound = SoundID.Tink;
            MineResist = 15f;
            MinPick = 65; 
            AddMapEntry(new Color(95, 201, 64), CreateMapEntryName());
        }
        public override void Load() {
            if (!Main.dedServ) {
                glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow", AssetRequestMode.ImmediateLoad).Value;
            }
        }

        public override void Unload() {
            glowTexture = null;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch) {
            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
            Vector2 drawPos = zero + new Vector2(i * 16, j * 16) - Main.screenPosition;
            float pulse = (float)Math.Sin(Main.GameUpdateCount * 0.02f) * 0.3f + 0.7f;
            Color glowColor = new Color(95, 201, 64) * pulse;
            Tile tile = Main.tile[i, j];
            Rectangle tileFrame = new(0, 0, 16, 16);
            //spriteBatch.Draw(glowTexture, drawPos, tileFrame, glowColor);
        }
    }
}