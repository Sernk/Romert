using Romert.Common.Players;
using Romert.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.UI;

namespace Romert.Content.Items.Other;

public class AlchemistBook : ModItem {
    UIState ui;

    public override void SetDefaults() {
        Item.width = 22;
        Item.height = 22;
        Item.value = Item.buyPrice(silver: 1);
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips) {
        if (Main.LocalPlayer.Get<RomertPlayer>().Class == 3) {
            tooltips.RemoveAt(0);
            tooltips.Insert(0, new(Mod, "Name", Loc(LocCategory[0] + ".Book", "Name")));
            tooltips.Insert(1, new(Mod, "Info", Loc(LocCategory[0] + ".Book", "Info")));
        }
    }
    public override bool CanRightClick() => true;
    public override void RightClick(Player player) {
        if (!player.Get<AlchemistBookPlayer>().ActiveUI) {
            if (ui is not null) { ui = null; }
            if (ui == null) {
                ui = new UIs.AlchemistBook();
                GetInstance<Romert>().AlchemistBookUI.SetState(ui);
                SoundEngine.PlaySound(SoundID.MenuOpen, player.Center);
                Main.playerInventory = true;
            }
        }
        Item.stack++;
    }
    public override bool PreDrawTooltip(ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y) {
        if (Main.LocalPlayer.Get<RomertPlayer>().Class == 3) {
            //SpriteBatch sb = Main.spriteBatch;
            //Vector2 pos = new(x, y);
            //Draw(sb, "NameFlower", pos, Loc(LocCategory[0] + ".Book", "Name"));
            //Draw(sb, "InfoFlask", new(pos.X - 4, pos.Y + 20), Loc(LocCategory[0] + ".Book", "Info"));
        }
        return true;
    }
    static void Draw(SpriteBatch sb, string name, Vector2 pos, string text) {
        Texture2D texture = GetTexture(name + "_Left").GetAsset().Value;
        sb.Draw(texture, new Vector2(pos.X - 18, pos.Y), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
        texture = GetTexture(name + "_Right").GetAsset().Value;
        Vector2 stringSize = FontAssets.MouseText.Value.MeasureString(text);
        sb.Draw(texture, new Vector2(pos.X + stringSize.X, pos.Y), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
    }
}