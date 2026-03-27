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
    void DrawText(SpriteBatch spriteBatch, string text, Vector2 pos) => Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, pos.X, pos.Y, Color.White * alpha, Color.Black * alpha, Vector2.Zero, 1f);
    static Vector2 DrawText(Vector2 pos, string name, Color? color = null) {
        Vector2 stringSize = FontAssets.MouseText.Value.MeasureString(name);
        return ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, name, pos, color ?? new(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, 255), 0f, stringSize / 2f, Vector2.One);
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
    static string Loc(string locKey) => RomertUtil.AddLoc.Loc(LocCategory[0] + ".Book", locKey);

    protected override void DrawSelf(SpriteBatch spriteBatch) {
        base.DrawSelf(spriteBatch);

        buttonHoverNow.Clear();

        Player player = Main.LocalPlayer;
        bool active = player.Get<AlchemistBookPlayer>().ActiveUI;
        float target = active ? 1f : 0f;

        alpha = MathHelper.Lerp(alpha, target, 0.1f);

        Vector2 basePos = new(Main.screenWidth / 2, Main.screenHeight / 2);
        Vector2 searchPos = new(basePos.X - 235, basePos.Y - 260);
        Vector2 elementPos = new(searchPos.X, searchPos.Y);

        Draw(spriteBatch, "AdventurersBookPageEmpty", basePos);
        DrawSearch(spriteBatch, searchPos);
        DrawElement(spriteBatch, new(elementPos.X - 188, elementPos.Y + 70));

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
        if (player.ActiveUI) {
            Vector2 textPos = new Vector2(pos.X + 180, pos.Y - 388) + GetUI(ShortCat[0] + "Book/" + "AdventurersBookPageEmpty").GetAsset().Value.Size() / 2f;
            DrawText(textPos, Loc("Info1"));
        }
        if (player.SaveType.Count != 0) {
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
        for (int k = 0; k < player.SaveType.Count; k++) {
            if (AlchemistReagentManager.ReagentsData[i].Name == player.SaveType[k]) {
                Draw(sb, "NameSlot", new(pos.X + 210, pos.Y + scale));
                Draw(sb, "IconSlot", new(pos.X  + 7, pos.Y + scale));
                buttonHoverNow[AlchemistReagentManager.ReagentsData[i].Name] = Hover("FullSlot", new(pos.X + 188, pos.Y + scale));
                bool wasHover = buttonHoverPrev.TryGetValue(AlchemistReagentManager.ReagentsData[i].Name, out var v) && v;
                Vector2 textPos = new Vector2(pos.X + 10, pos.Y - 18 + scale) + GetUI(ShortCat[0] + "Book/" + "NameSlot").GetAsset().Value.Size() / 2f;

                if (Hover("FullSlot", new(pos.X + 188, pos.Y + scale)) && !wasHover) {
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Player.Center);
                }
                if (Hover("FullSlot", new(pos.X + 188, pos.Y + scale))) {
                    Main.instance.MouseText(Loc("HoverSlot"));
                    Draw(sb, "FullSlot_Glow", new(pos.X + 188, pos.Y + scale), color: Color.Gold);
                    if (Main.mouseLeft && Main.mouseLeftRelease) {
                        if (activeInfoForReagentNum == i) {
                            if (activeInfoForReagent) {
                                SoundEngine.PlaySound(SoundID.MenuClose, player.Player.Center);
                                activeInfoForReagent = false;
                            }
                            else {
                                SoundEngine.PlaySound(SoundID.MenuOpen, player.Player.Center);
                                activeInfoForReagent = true;
                            }
                        }
                        else {
                            SoundEngine.PlaySound(SoundID.MenuOpen, player.Player.Center);
                            activeInfoForReagent = true;
                        }
                        activeInfoForReagentNum = i;
                    }
                }
                if (alpha2 > 0.01f) {
                    Page2(sb, new(pos.X + 654, pos.Y + 60), AlchemistReagentManager.ReagentsData[activeInfoForReagentNum], alpha2);
                }
                if (player.ActiveUI) { DrawText(textPos, AlchemistReagentManager.ReagentsData[i].LocalizationName); }

                Draw(sb, texture, new(pos.X + 7, pos.Y + scale));
                scale += 60;
            }
        }
    }
    void Page2(SpriteBatch sb, Vector2 pos, AlchemistReagent reagent, float alpha) {
        Draw(sb, "RecipeBg", pos, alpha: alpha);

        Draw(sb, "IconSlot", pos, alpha: alpha);
        Draw(sb, texture: reagent.TexturePatch.GetAsset().Value, pos, alpha: alpha);
        if (Hover(texture: reagent.TexturePatch.GetAsset().Value, pos)) {
            Main.instance.MouseText(reagent.LocalizationName);
        }

        Vector2[] points = [new(pos.X - 88, pos.Y - 55), new(pos.X + 88, pos.Y - 55), new(pos.X - 88, pos.Y), new(pos.X + 88, pos.Y), new(pos.X - 88, pos.Y + 55), new(pos.X + 88, pos.Y + 55)];
        int frame = (int)(Main.GlobalTimeWrappedHourly * 0.50f) % 6;

        for (int i = 0; i < RegisterReagent.AlchemistReagents.Count; i++) {
            for (int j = 0; j < RegisterReagent.AlchemistReagents[i].ItemID.Count; j++) {
                if (reagent.Name == RegisterReagent.AlchemistReagents[i].Reagent.Name) {
                    for (int k = 0; k < points.Length; k++) {
                        Draw(sb, "IconSlot", points[k], alpha: alpha);
                        Draw(sb, texture: TextureAssets.Item[RegisterReagent.AlchemistReagents[i].ItemID[j].type].Value, points[frame], alpha: alpha);
                        if (Hover(texture: TextureAssets.Item[RegisterReagent.AlchemistReagents[i].ItemID[j].type].Value, points[frame])) {
                            Main.instance.MouseText($"Item name: {RegisterReagent.AlchemistReagents[i].ItemID[j].Name}\nneed ingredient {RegisterReagent.AlchemistReagents[i].ItemID[j].stack}");
                        }
                    }
                    DrawText(sb, reagent.Descriptions, new(pos.X - 125, pos.Y + 125));
                }
            }
        }
    }
}