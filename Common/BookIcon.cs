using Romert.Common.Players;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Romert.Common;

public class BookIcon : BuilderToggle {
    string book = "BookIcon_Open";
    string hover = "BookIcon_Open_Glow";

    public override string Texture => "Romert/Asset/Textures/UI/Alchemist/" + book;
    public override string HoverTexture => "Romert/Asset/Textures/UI/Alchemist/" + hover;
    public override string DisplayValue() {
        string value;
        if (Main.LocalPlayer.Get<AlchemistBookPlayer>().VisibleBookInfo) { value = "Enable"; }
        else { value = "Disable"; }
        return string.Format(Language.GetTextValue($"{LocPatch}{LocCategory[0] + ".Book.Icon"}." + "Info"), Language.GetTextValue($"{LocPatch}{LocCategory[0] + ".Book.Icon"}." + value));
    }
    public override bool Active() => Main.LocalPlayer.Get<AlchemistBookPlayer>().HasBook;
    public override bool OnLeftClick(ref SoundStyle? sound) {
        if (Main.LocalPlayer.Get<AlchemistBookPlayer>().VisibleBookInfo) { Main.LocalPlayer.Get<AlchemistBookPlayer>().VisibleBookInfo = false; }
        else { Main.LocalPlayer.Get<AlchemistBookPlayer>().VisibleBookInfo = true; }
        return base.OnLeftClick(ref sound);
    }
    public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams) {
        if (Main.LocalPlayer.Get<AlchemistBookPlayer>().VisibleBookInfo) { book = "BookIcon_Open"; }
        else { book = "BookIcon_Close"; }
        return base.Draw(spriteBatch, ref drawParams);
    }
    public override bool DrawHover(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams) {
        if (Main.LocalPlayer.Get<AlchemistBookPlayer>().VisibleBookInfo) { hover = "BookIcon_Open_Glow"; }
        else { hover = "BookIcon_Close_Glow"; }
        return base.DrawHover(spriteBatch, ref drawParams);
    }
}