using Romert.Common.Players;
using Romert.Helpers;
using Romert.UIs;
using Terraria.Audio;
using Terraria.ObjectData;
using Terraria.UI;

namespace Romert.Content.Tiles;

public class AlchemicalShelf : ModTile {
    bool inZone;

    UIState ui;

    public override void SetStaticDefaults() {
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CoordinateHeights = [16, 16];
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(97, 67, 47), CreateMapEntryName());
        TileObjectData.newTile.DrawYOffset = 2;
    }
    public override bool RightClick(int i, int j) {
        Player player = Main.LocalPlayer;
        player.Get<AlchemistTilePlayer>().ActiveAlchemistUI = inZone;
        if (player.Get<AlchemistTilePlayer>().ActiveAlchemistUI) {
            if (ui == null) {
                ui = new AlchemistTable();
                GetInstance<Romert>().AlchemistTableUI.SetState(ui);
                SoundEngine.PlaySound(SoundID.MenuOpen, player.Center);
                Main.playerInventory = true;
            }
        }
        return true;
    }
    public static bool CheckBiomeTile(int x, int y, int centerX, int centerY, int size = 10) {
        int half = size / 2;

        int left = centerX - half;
        int right = centerX + half;
        int top = centerY - half;
        int bottom = centerY + half;

        return x >= left && x < right && y >= top && y < bottom;
    }
    public override void NearbyEffects(int i, int j, bool closer) {
        Player player = Main.LocalPlayer;
        if (CheckBiomeTile((int)(player.position.X / 16), (int)(player.position.Y / 16), i, j, 14)) { inZone = true; }
        else { player.Get<AlchemistTilePlayer>().ActiveAlchemistUI = false; ui = null; }
    }
    public override void MouseOver(int i, int j) {

    }
    public override void MouseOverFar(int i, int j) {
    }
}