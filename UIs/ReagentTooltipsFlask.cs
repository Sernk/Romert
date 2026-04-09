using Romert.Content.Reagents.Flask;
using Romert.Core;
using System.Collections.Generic;
using Terraria.GameContent;

namespace Romert.UIs;

public class ReagentTooltipsFlask {
    public static void Draw(SpriteBatch sb, Vector2 pos, Vector2 centerPos, AlchemistReagent[] reagents) {
        List<string> names = [];
        string text;
        for (int i = 0; i < reagents.Length; i++) { if (reagents[i].Name != GetReagent<NoN>().Name && reagents[i].Name != GetReagent<Look>().Name) { names.Add(reagents[i].LocalizationName); } }
        text = Loc("Alchemist", "Tooltips.ActiveReagent") + " " + string.Join(", ", names);
        Vector2 heling = FontAssets.MouseText.Value.MeasureString(text); heling.X += 30;
        float totalWidth = heling.X + 20f + 20f;
        int posY = (int)heling.Y + 25;
        AlchemistReagent.Draw(sb, pos, heling, posY, centerPos);
        pos = new Vector2(centerPos.X - totalWidth / 2f, pos.Y);
        Vector2 posText = new(pos.X + 30f, pos.Y + 20);
        if (names.Count != 0) {
            Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Loc("Alchemist", "Tooltips.ActiveReagent"), posText.X, posText.Y, Color.White, Color.Black, Vector2.Zero, 1f);
            float scale = 0;
            for (int i = 0; i < names.Count; i++) {
                Color color;
                if (reagents[i].Rarity.IsAnimated) { color = reagents[i].Rarity.AnimatedColor(); }
                else { color = reagents[i].Rarity.Color; }
                Vector2 fistTextSize = FontAssets.MouseText.Value.MeasureString(Loc("Alchemist", "Tooltips.ActiveReagent"));
                Vector2 nameSize = FontAssets.MouseText.Value.MeasureString(names[i]);
                string subString = i < names.Count - 1 ? ", " : ".";
                Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, $" {names[i]}", posText.X + fistTextSize.X + scale, posText.Y, color, reagents[i].Rarity.BorderColor, Vector2.Zero, 1f);
                Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, $" {subString}", posText.X + nameSize.X + fistTextSize.X + scale, posText.Y, Color.White, Color.Black, Vector2.Zero, 1f);
                scale += nameSize.X + 10;
            }
        }
    }
    public static void DrawInUI(SpriteBatch sb, Vector2 pos, Vector2 centerPos, AlchemistReagent reagent) {
        Vector2 heling = FontAssets.MouseText.Value.MeasureString(Loc("Alchemist", "Tooltips.Add") + " " + reagent.LocalizationName); heling.X += 30;
        float totalWidth = heling.X + 20f + 20f;
        int posY = (int)heling.Y + 25;
        AlchemistReagent.Draw(sb, pos, heling, posY, centerPos);
        Color color;
        if (reagent.Rarity.IsAnimated) { color = reagent.Rarity.AnimatedColor(); }
        else { color = reagent.Rarity.Color; }
        pos = new Vector2(centerPos.X - totalWidth / 2f, pos.Y);
        Vector2 posText = new(pos.X + 30f, pos.Y + 20);
        Vector2 fistTextSize = FontAssets.MouseText.Value.MeasureString(Loc("Alchemist", "Tooltips.Add"));
        Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Loc("Alchemist", "Tooltips.Add"), posText.X, posText.Y, Color.White, Color.Black, Vector2.Zero, 1f);
        Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, $" {reagent.LocalizationName}", posText.X + fistTextSize.X, posText.Y, color, reagent.Rarity.BorderColor, Vector2.Zero, 1f);
    }
}