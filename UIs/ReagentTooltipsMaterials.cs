using Romert.Common.Players;
using Romert.Core;
using Terraria.GameContent;

namespace Romert.UIs;

public class ReagentTooltipsMaterials {
    public static void Draw(SpriteBatch sb, Vector2 pos, Vector2 centerPos, AlchemistReagent reagent) {
        if (!Main.LocalPlayer.Get<AlchemistBookPlayer>().OpenType.Contains(reagent.Name)) { return; }

        Vector2 tooltips = FontAssets.MouseText.Value.MeasureString(Loc("Alchemist", "Tooltips.Has") + " " + reagent.LocalizationName);
        Vector2 heling = tooltips; heling.X += 30;
        float totalWidth = heling.X + 20f + 20f;

        int posY = (int)heling.Y + 25;

        AlchemistReagent.Draw(sb, pos, heling, posY, centerPos);

        Color color;

        if (reagent.Rarity.IsAnimated) { color = reagent.Rarity.AnimatedColor(); }
        else { color = reagent.Rarity.Color; }

        pos = new Vector2(centerPos.X - totalWidth / 2f, pos.Y);
        Vector2 posText = new(pos.X + 30f, pos.Y + 20);

        Vector2 fistTextSize = FontAssets.MouseText.Value.MeasureString(Loc("Alchemist", "Tooltips.Has"));
        Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Loc("Alchemist", "Tooltips.Has"), posText.X, posText.Y, Color.White, Color.Black, Vector2.Zero, 1f);
        Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, $" {reagent.LocalizationName}", posText.X + fistTextSize.X, posText.Y, color, reagent.Rarity.BorderColor, Vector2.Zero, 1f);
    }
}