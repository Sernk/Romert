using ReLogic.Content;
using Romert.Core;
using Terraria.Audio;

namespace Romert.Content.Tiles;

public class ArgiteOreTile : ModTile {
    private Texture2D glowTexture;

    public override void SetStaticDefaults() {
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileLighted[Type] = true;
        Touch.Register(Type, BuffID.Poisoned);
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
    public override bool KillSound(int i, int j, bool fail) {
        return true;
    }
    public override void PostDraw(int i, int j, SpriteBatch spriteBatch) {

    }
    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) {

        return true;
    }
    public override void NearbyEffects(int i, int j, bool closer) {
        base.NearbyEffects(i, j, closer);
    }
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem) {
        if (!fail) { SoundEngine.PlaySound(new SoundStyle("Romert/Asset/Song/Ore/Argite/DestructionOre"), new(i * 16, j * 16)); }
        //SoundEngine.PlaySound(new SoundStyle("Romert/Asset/Song/Ore/Argite/DestructionOre"), new(i, j));
    }
}