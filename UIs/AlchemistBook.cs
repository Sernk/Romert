using Romert.Common.Players;
using Romert.Core;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Romert.UIs;

public class AlchemistBook : UIState {
    float alpha = 0f;
    float alpha2 = 0f;
    string searchText = "";
    bool activeSearch;

    bool activeInfoForReagent = false;
    int activeInfoForReagentNum = 0;

    readonly Dictionary<string, bool> buttonHoverPrev = [];
    readonly Dictionary<string, bool> buttonHoverNow = [];
    readonly Dictionary<int, bool> activeElement = [];

    List<string> DrawElementData = [];

    public override void OnInitialize() {

    }
    public override void Update(GameTime gameTime) {
        Player player = Main.LocalPlayer;
        bool hasBook = false;
        player.Get<AlchemistBookPlayer>().ActiveUI = true;

        if (activeSearch) {
            PlayerInput.WritingText = true;
            Main.instance.HandleIME();
            string newText = Main.GetInputText(searchText);
            if (newText != searchText) { searchText = newText; }
        }
        for (int i = 0; i < 58; i++) {
            if (player.inventory[i].type == ItemType<Content.Items.Other.AlchemistBook>() && player.inventory[i].stack > 0) {
                hasBook = true;
                break;
            }
        }
        if (!hasBook) {
            player.Get<AlchemistBookPlayer>().ActiveUI = false;
            activeInfoForReagent = false;
        }
        if (alpha <= 0.01 && !player.Get<AlchemistBookPlayer>().ActiveUI) { GetInstance<Romert>().AlchemistBookUI.SetState(null); }
    }

    void Draw(SpriteBatch spriteBatch, string name, Vector2 pos, Color? color = null, float? alpha = null) {
        alpha = alpha is null ? this.alpha : alpha;
        color = color is null ? Color.White : color;
        spriteBatch.Draw(GetUI(ShortCat[0] + "Book/" + name).GetAsset().Value, pos, null, (Color)(color * alpha), 0f, GetUI(ShortCat[0] + "Book/" + name).GetAsset().Value.Size() / 2, 1, SpriteEffects.None, 1f);
    }
    void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 pos, Color? color = null, float? alpha = null) {
        alpha = alpha is null ? this.alpha : alpha;
        color = color is null ? Color.White : color;
        spriteBatch.Draw(texture, pos, null, (Color)(color * alpha), 0f, texture.Size() / 2, 1, SpriteEffects.None, 1f);
    }
    void DrawText(SpriteBatch spriteBatch, string text, Vector2 pos) {
        if (!Main.LocalPlayer.Get<AlchemistBookPlayer>().ActiveUI) { return; }
        Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, pos.X, pos.Y, Color.White * alpha, Color.Black * alpha, Vector2.Zero, 1f);
    }
    static Vector2 DrawText(Vector2 pos, string name, Color? color = null, float? alpha = null) {
        alpha = alpha is null ? 1f : alpha;
        color = color is null ? Color.White : color;
        if (!Main.LocalPlayer.Get<AlchemistBookPlayer>().ActiveUI) { return Vector2.Zero; }
        Vector2 stringSize = FontAssets.MouseText.Value.MeasureString(name);
        return ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, name, pos, (Color)(color * alpha), 0f, stringSize / 2f, Vector2.One);
    }
    static bool Hover(string name, Vector2 pos, float drawScale = 1f) {
        Texture2D texture = GetUI(ShortCat[0] + "Book/" + name).GetAsset().Value;
        Vector2 size = texture.Size() * drawScale;
        Rectangle rect = new((int)(pos.X - size.X / 2f), (int)(pos.Y - size.Y / 2f), texture.Width, texture.Height);
        return rect.Contains(Main.mouseX, Main.mouseY);
    }
    static bool Hover(Texture2D texture, Vector2 pos, float drawScale = 1f) {
        Vector2 size = texture.Size() * drawScale;
        Rectangle rect = new((int)(pos.X - size.X / 2f), (int)(pos.Y - size.Y / 2f), texture.Width, texture.Height);
        return rect.Contains(Main.mouseX, Main.mouseY);
    }
    static string Loc(string locKey, string sub = "") => RomertUtil.AddLoc.Loc(LocCategory[0] + ".Book" + sub, locKey);

    protected override void DrawSelf(SpriteBatch spriteBatch) {
        base.DrawSelf(spriteBatch);

        buttonHoverNow.Clear();

        Player player = Main.LocalPlayer;
        bool active = player.Get<AlchemistBookPlayer>().ActiveUI;
        float target = active ? 1f : 0f;

        alpha = MathHelper.Lerp(alpha, target, 0.1f);

        Vector2 basePos = new(Main.screenWidth / 2, Main.screenHeight / 2);
        Vector2 paragraphPos = new Vector2(basePos.X - 1175, basePos.Y - 900) + GetUI(ShortCat[0] + "Book/" + "AdventurersBookPageEmpty").GetAsset().Value.Size() / 2f;
        Vector2 searchPos = new(basePos.X - 235, basePos.Y - 210);
        Vector2 elementPos = new(searchPos.X - 188, searchPos.Y + 100);

        Draw(spriteBatch, "AdventurersBookPageEmpty", basePos);
        DrawParagraph(spriteBatch, paragraphPos, Loc("Search", ".Paragraph"), alpha: alpha);
        DrawParagraph(spriteBatch, new(paragraphPos.X, paragraphPos.Y + 100), Loc("Reagents", ".Paragraph"), alpha: alpha);
        DrawSearch(spriteBatch, searchPos);
        DrawElement(spriteBatch, elementPos);

        buttonHoverPrev.Clear();
        foreach (var kv in buttonHoverNow) {
            buttonHoverPrev[kv.Key] = kv.Value;
        }
    }
    void DrawSearch(SpriteBatch spriteBatch, Vector2 pos) {
        Draw(spriteBatch, "TestSearch", pos);
        Draw(spriteBatch, "TestLoup", new Vector2(pos.X - 180, pos.Y));

        bool hover = Hover("TestSearch", pos);
        buttonHoverNow["Search"] = hover;
        bool wasHover = buttonHoverPrev.TryGetValue("Search", out bool v) && v;
        Vector2 textPos = pos + new Vector2(-155, -12);
        string displayText = searchText;

        if (hover && !wasHover) { SoundEngine.PlaySound(SoundID.MenuTick); }
        if (hover) {
            if (Main.mouseLeft && Main.mouseLeftRelease) { activeSearch = true; }
            Main.instance.MouseText(Loc("Search"));
        }
        else {
            if (activeSearch && Main.mouseLeft && Main.mouseLeftRelease) {
                activeSearch = false;
                Main.CurrentInputTextTakerOverride = null;
            }
        }
        if (activeSearch) {
            displayText += "|";
            PlayerInput.WritingText = true;
        }
        if (!string.IsNullOrEmpty(displayText)) { DrawText(spriteBatch, displayText, new(textPos.X, textPos.Y)); }
        if (hover || activeSearch) { Draw(spriteBatch, "FullSlot_Glow", pos, color: Color.Gold); }
    }
    void DrawElement(SpriteBatch sb, Vector2 pos) {
        float scale = 0;
        AlchemistBookPlayer player = Main.LocalPlayer.Get<AlchemistBookPlayer>();
        Vector2 textPos = new Vector2(pos.X + 180, pos.Y - 468) + GetUI(ShortCat[0] + "Book/" + "AdventurersBookPageEmpty").GetAsset().Value.Size() / 2f;
        if (!activeInfoForReagent) {
            DrawText(textPos, Loc("Info1"));
        }
        if (player.OpenType.Count != 0 || player.LockedType.Count != 0) {
            for (int i = 0; i < AlchemistReagentManager.ReagentsData.Count; i++) {
                if (AlchemistReagentManager.ReagentsData[i].HasTexture) {
                    Texture2D texture = AlchemistReagentManager.ReagentsData[i].TexturePatch.GetAsset().Value;
                    if (!string.IsNullOrWhiteSpace(searchText)) {
                        string search = searchText.Replace(" ", "");
                        string name = AlchemistReagentManager.ReagentsData[i].LocalizationName.Replace(" ", "");
                        if (name.Contains(search, StringComparison.OrdinalIgnoreCase)) {
                            BaseLogic(sb, pos, player, texture, ref scale, i);
                        }
                    }
                    else {
                        BaseLogic(sb, pos, player, texture, ref scale, i);
                    }
                }
            }
        }
    }
    void BaseLogic(SpriteBatch sb, Vector2 pos, AlchemistBookPlayer player, Texture2D texture, ref float scale, int i) {
        float target = activeInfoForReagent ? 1f : 0f;
        alpha2 = MathHelper.Lerp(alpha2, target, 0.1f);
        for (int k = 0; k < player.Current.Count; k++) {
            if (AlchemistReagentManager.ReagentsData[i].Name == player.Current[k]) {
                if (!DrawElementData.Exists(x => x == player.Current[k])) {
                    DrawElementData.Add(player.Current[k]);
                }
            }
        }
        // Current 
        for (int k = 0; k < player.OpenType.Count; k++) {
            if (AlchemistReagentManager.ReagentsData[i].Name == player.OpenType[k]) {
                if (!DrawElementData.Exists(x => x == player.OpenType[k])) {
                    DrawElementData.Add(player.OpenType[k]);
                }
            }
        }
        for (int j = 0; j < DrawElementData.Count; j++) {
            if (AlchemistReagentManager.ReagentsData[i].Name == DrawElementData[j]) {
                Draw(sb, "NameSlot", new(pos.X + 210, pos.Y + scale));
                Draw(sb, "IconSlot", new(pos.X + 7, pos.Y + scale));

                buttonHoverNow[AlchemistReagentManager.ReagentsData[i].Name] = Hover("FullSlot", new(pos.X + 188, pos.Y + scale));
                bool wasHover = buttonHoverPrev.TryGetValue(AlchemistReagentManager.ReagentsData[i].Name, out var v) && v;

                if (Hover("FullSlot", new(pos.X + 188, pos.Y + scale)) && !wasHover) { SoundEngine.PlaySound(SoundID.MenuTick, player.Player.Center); }
                if (Hover("FullSlot", new(pos.X + 188, pos.Y + scale))) {
                    Main.instance.MouseText(Loc("HoverSlot"));
                    Draw(sb, "FullSlot_Glow", new(pos.X + 188, pos.Y + scale), color: Color.Gold);
                    if (Main.mouseLeft && Main.mouseLeftRelease) {
                        if (activeInfoForReagentNum == i) {
                            if (activeInfoForReagent) {
                                SoundEngine.PlaySound(SoundID.MenuClose, player.Player.Center);
                                activeInfoForReagent = false;
                                activeElement.Clear();
                                activeElement.Add(activeInfoForReagentNum, activeInfoForReagent);
                            }
                            else {
                                SoundEngine.PlaySound(SoundID.MenuOpen, player.Player.Center);
                                activeInfoForReagent = true;
                                activeElement.Clear();
                                activeElement.Add(activeInfoForReagentNum, activeInfoForReagent);
                            }
                        }
                        else {
                            SoundEngine.PlaySound(SoundID.MenuOpen, player.Player.Center);
                            activeInfoForReagent = true;
                            activeElement.Clear();
                            activeElement.Add(i, activeInfoForReagent);
                        }
                        activeInfoForReagentNum = i;
                    }
                }
                Vector2 textPos = new Vector2(pos.X + 10, pos.Y - 18 + scale) + GetUI(ShortCat[0] + "Book/" + "NameSlot").GetAsset().Value.Size() / 2f;
                for (int num = 0; num < player.OpenType.Count; num++) {
                    if (AlchemistReagentManager.ReagentsData[i].Name == player.OpenType[num]) {
                        activeElement.TryGetValue(i, out bool value);
                        if (value) {
                            Page2(sb, new(pos.X + 654, pos.Y + 6), AlchemistReagentManager.ReagentsData[activeInfoForReagentNum], player, alpha2);
                        }
                        DrawText(textPos, AlchemistReagentManager.ReagentsData[i].LocalizationName);
                        Draw(sb, texture, new(pos.X + 7, pos.Y + scale));
                    }
                }
                for (int num = 0; num < player.Locked.Count; num++) {
                    if (AlchemistReagentManager.ReagentsData[i].Name == player.Locked[num]) {
                        activeElement.TryGetValue(i, out bool value);
                        if (value) {
                            Page2Locked(sb, new(pos.X + 654, pos.Y + 6), AlchemistReagentManager.ReagentsData[activeInfoForReagentNum], player, alpha2);
                        }
                        DrawText(textPos, "???");
                        Draw(sb, texture, new(pos.X + 7, pos.Y + scale));
                    }
                }
                scale += 60;
            }
        }
    }
    void Page2Locked(SpriteBatch sb, Vector2 pos, AlchemistReagent reagent, AlchemistBookPlayer player, float alpha) {
        Vector2 posElement = new(pos.X, pos.Y - 38);
        DrawParagraph(sb, new(pos.X - 475, pos.Y - 475), Loc("Descriptions", ".Paragraph"), alpha: alpha);
        for (int k = 0; k < player.LockedType.Count; k++) {
            for (int l = 0; l < reagent.CurrentType.ItemID.Count; l++) {
                if (player.LockedType[k].type == reagent.CurrentType.ItemID[l].type) {
                    DrawText(sb, $"To access the {player.LockedType[k].Name} property, \nyou'll need {reagent.CurrentType.ItemID[l].stack}. You have {player.LockedType[k].stack}.", new(posElement.X - 198, posElement.Y - 100));
                }
            }
        }
    }
    void Page2(SpriteBatch sb, Vector2 pos, AlchemistReagent reagent, AlchemistBookPlayer player, float alpha) {
        Vector2 posElement = new(pos.X, pos.Y - 38);
        DrawParagraph(sb, new(pos.X - 475, pos.Y - 475), Loc("Recipe", ".Paragraph"), alpha: alpha);
        DrawParagraph(sb, new(pos.X - 475, pos.Y - 230), Loc("Descriptions", ".Paragraph"), alpha: alpha);

        Draw(sb, "RecipeBg", posElement, alpha: alpha);

        Draw(sb, "IconSlot", new(posElement.X, pos.Y - 3), alpha: alpha);
        int frame = (int)(Main.GlobalTimeWrappedHourly * 3f) % 5;
        sb.Draw(GetTexture2D("Arrow_Frame"), posElement, GetTexture2D("Arrow_Frame").Frame(1, 5, 0, frame), Color.White * alpha, 0f, GetTexture2D("Arrow").Size() / 2, 1f, SpriteEffects.None, 1f);
        if (Hover(texture: GetTexture2D("Arrow"), posElement)) {
            Main.instance.MouseText("Filling in");
        }
        Draw(sb, "IconSlot", new(posElement.X, pos.Y - 73), alpha: alpha);
        frame = (int)(Main.GlobalTimeWrappedHourly * 1.25f) % Lists.Items.FlaskItem.Count;
        Draw(sb, TextureAssets.Item[Lists.Items.FlaskItem[frame]].Value, new(posElement.X, pos.Y - 73), alpha: alpha);
        Draw(sb, texture: TextureAssets.Item[Lists.Items.FlaskItem[frame]].Value, new(posElement.X, pos.Y - 3), alpha: alpha);
        if (Hover(texture: TextureAssets.Item[Lists.Items.FlaskItem[frame]].Value, new(posElement.X, pos.Y - 73))) {
            player.ActiveRecipeUI = true;
            player.PreviewReagent = reagent;
            Main.HoverItem = new(Lists.Items.FlaskItem[frame]);
            Main.instance.MouseText(Main.hoverItemName);
        }
        if (Hover(texture: TextureAssets.Item[Lists.Items.FlaskItem[frame]].Value, new(posElement.X, pos.Y - 3))) {
            Main.HoverItem = new(Lists.Items.FlaskItem[frame]);
            Main.instance.MouseText(Main.hoverItemName);
        }

        Vector2[] points = [new(posElement.X - 88, posElement.Y - 55), new(posElement.X + 88, posElement.Y - 55), new(posElement.X - 88, posElement.Y), new(posElement.X + 88, posElement.Y), new(posElement.X - 88, posElement.Y + 55), new(posElement.X + 88, posElement.Y + 55)];
        frame = (int)(Main.GlobalTimeWrappedHourly * 0.50f) % 6;

        for (int i = 0; i < RegisterReagent.AlchemistReagents.Count; i++) {
            for (int j = 0; j < RegisterReagent.AlchemistReagents[i].ItemID.Count; j++) {
                if (reagent.Name == RegisterReagent.AlchemistReagents[i].Reagent.Name) {
                    for (int k = 0; k < points.Length; k++) {
                        Draw(sb, "IconSlot", points[k], alpha: alpha);
                        Draw(sb, texture: TextureAssets.Item[RegisterReagent.AlchemistReagents[i].ItemID[j].type].Value, points[frame], alpha: alpha);
                        if (Hover(texture: TextureAssets.Item[RegisterReagent.AlchemistReagents[i].ItemID[j].type].Value, points[frame])) {
                            //RegisterReagent.AlchemistReagents[i].ItemID[j].stack = 1;
                            Main.HoverItem = RegisterReagent.AlchemistReagents[i].ItemID[j];
                            Main.instance.MouseText(Main.hoverItemName);
                        }
                    }
                    DrawText(sb, reagent.Descriptions, new(posElement.X - 198, posElement.Y + 150));
                }
            }
        }
    }
    void DrawParagraph(SpriteBatch sb, Vector2 pos, string text, Color? color = null, float? alpha = null) {
        alpha ??= this.alpha;
        color ??= Color.White;

        Texture2D bg = GetTexture2D("AdventurersBookPageEmpty");

        Vector2 center = pos + bg.Size() / 2f;
        Vector2 textSize = FontAssets.MouseText.Value.MeasureString(text);

        float left = center.X - textSize.X / 2f;
        float right = center.X + textSize.X / 2f;

        DrawText(center, text, color, alpha);
        // need new texture
        sb.Draw(GetTexture2D("Paragraph_Left"), new Vector2(left - 5 - GetTexture2D("Paragraph_Left").Width / 2f, center.Y - 5), null, (Color)(color * alpha), 0f, GetTexture2D("Paragraph_Left").Size() / 2, 1f, SpriteEffects.None, 1f);
        //sb.Draw(GetTexture2D("Paragraph_Flower"), new Vector2(left - 107 + GetTexture2D("Paragraph_Flower").Width / 2f, center.Y - 4), null, (Color)(color * alpha), 0f, GetTexture2D("Paragraph_Flower").Size() / 2, 1f, SpriteEffects.None, 1f);
        sb.Draw(GetTexture2D("Paragraph_Right"), new Vector2(right + 82 - GetTexture2D("Paragraph_Right").Width / 2f, center.Y - 5), null, (Color)(color * alpha), 0f, GetTexture2D("Paragraph_Right").Size() / 2, 1f, SpriteEffects.None, 1f);
        //sb.Draw(GetTexture2D("Paragraph_Flower"), new Vector2(right + 108 - GetTexture2D("Paragraph_Flower").Width / 2f, center.Y - 4), null, (Color)(color * alpha), 0f, GetTexture2D("Paragraph_Flower").Size() / 2, 1f, SpriteEffects.FlipHorizontally, 1f);
    }
    Texture2D GetTexture2D(string name) => GetUI(ShortCat[0] + "Book/" + name).GetAsset().Value;
}