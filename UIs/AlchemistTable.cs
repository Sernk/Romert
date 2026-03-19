using Romert.Common.GlobalItems;
using Romert.Common.Players;
using Romert.Content.Items.Weapons.Alchemical;
using Romert.Content.Reagents.Flask;
using Romert.Core;
using Romert.Dataset;
using Romert.Resources;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Romert.UIs;

public class AlchemistTable : UIState {
    VanillaItemSlotWrapper flask;

    VanillaItemSlotWrapper leftTopSlot;
    VanillaItemSlotWrapper leftCenterSlot;
    VanillaItemSlotWrapper leftBotSlot;

    VanillaItemSlotWrapper rightTopSlot;
    VanillaItemSlotWrapper rightCenterSlot;
    VanillaItemSlotWrapper rightBotSlot;

    AnimationDate button;

    public VanillaItemSlotWrapper[] ReagentSlot { get; private set; } = new VanillaItemSlotWrapper[6];

    bool isFist = false;

    float glowAlpha = 0f;

    void RegisterSlot(ref VanillaItemSlotWrapper slot) {
        slot = new() {
            ValidItemFunc = item => item.IsAir || !item.IsAir && item.Get<AlchemicalItems>().isReagent,
            IsVisible = item => flask.Item.type != ItemID.None
        };
        Append(slot);
    }
    public override void OnInitialize() {
        flask = new() { ValidItemFunc = item => item.IsAir || !item.IsAir && item.Get<AlchemicalItems>().isFlask };
        Append(flask);

        RegisterSlot(ref leftTopSlot);
        RegisterSlot(ref leftCenterSlot);
        RegisterSlot(ref leftBotSlot);

        RegisterSlot(ref rightTopSlot);
        RegisterSlot(ref rightCenterSlot);
        RegisterSlot(ref rightBotSlot);

        ReagentSlot = [leftTopSlot, leftCenterSlot, leftBotSlot, rightTopSlot, rightCenterSlot, rightBotSlot];
    }
    public override void Update(GameTime gameTime) {
        Player player = Main.LocalPlayer;
        if (!player.Get<AlchemistTilePlayer>().ActiveAlchemistUI) { OnDeactivate(); }
        if (glowAlpha <= 0.01 && !player.Get<AlchemistTilePlayer>().ActiveAlchemistUI) {
            GetInstance<Romert>().AlchemistTableUI.SetState(null);
        }
    }
    public override void OnDeactivate() {
        Player player = Main.LocalPlayer;
        if (!isFist) { SoundEngine.PlaySound(SoundID.MenuClose, player.Center); isFist = true; }
        if (flask.Item.type != ItemID.None) { player.QuickSpawnItem(player.GetSource_Misc(Romert.ModName + "DeactivateAlchemistTableUI"), flask.Item.type, flask.Item.stack); flask.Item.type = ItemID.None; }
        for (int i = 0; i < ReagentSlot.Length; i++) {
            if (ReagentSlot[i].Item.type != ItemID.None) { player.QuickSpawnItem(player.GetSource_Misc(Romert.ModName + "DeactivateAlchemistTableUI"), ReagentSlot[i].Item.type, ReagentSlot[i].Item.stack); ReagentSlot[i].Item.type = ItemID.None; }
        }
    }
    void SettingVanillaItemSlot(float alfa, Vector2 pos) {
        BaseSetting(flask, alfa, pos);

        Vector2 leftPos = new(pos.X - pos.X / 2 + 220, pos.Y - pos.Y / 2 + 60);

        BaseSetting(leftTopSlot, alfa, leftPos);
        BaseSetting(leftCenterSlot, alfa, new(leftPos.X, pos.Y));
        BaseSetting(leftBotSlot, alfa, new(leftPos.X, leftPos.Y + 375));

        Vector2 rightPos = new(pos.X + pos.X / 2 - 220, pos.Y - pos.Y / 2 + 60);

        BaseSetting(rightTopSlot, alfa, rightPos);
        BaseSetting(rightCenterSlot, alfa, new(rightPos.X, pos.Y));
        BaseSetting(rightBotSlot, alfa, new(rightPos.X, rightPos.Y + 375));
    }
    static void BaseSetting(VanillaItemSlotWrapper slot, float alfa, Vector2 pos, Texture2D textureItem = null, string textureName = null) {
        slot.ItemTypeTexture = textureItem is null ? TextureAssets.Item[ItemType<BaseFlask>()].Value : textureItem;
        slot.Alpha = alfa;
        string patch = textureName is null ? "TestAlchemicalTableSlotUI" : textureName;
        slot.SlotTexture = GetUI(ShortCat[0] + patch).GetAsset().Value;
        slot.Position = pos;
    }
    void Draw(SpriteBatch spriteBatch, string name, Vector2 pos) => spriteBatch.Draw(GetUI(ShortCat[0] + name).GetAsset().Value, pos, null, Color.White * glowAlpha, 0f, GetUI(ShortCat[0] + name).GetAsset().Value.Size() / 2, 1, SpriteEffects.None, 1f);
    void Draw(SpriteBatch spriteBatch, string name, Vector2 pos, FrameData frame) => spriteBatch.Draw(GetUI(ShortCat[0] + name).GetAsset().Value, pos, GetUI(ShortCat[0] + name).GetAsset().Value.Frame(frame.Horizontal, frame.Vertical, frame.X, frame.Y, frame.SizeOffsetX, frame.SizeOffsetY), Color.White * glowAlpha, 0f, GetUI(ShortCat[0] + name).GetAsset().Value.Size() / 2, 1, SpriteEffects.None, 1f);
    static Vector2 DrawText(Vector2 pos, string locKey, Color? color = null) {
        string name = Loc(LocCategory[0] + ".Table", locKey);
        Vector2 stringSize = FontAssets.MouseText.Value.MeasureString(name);
        return ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, name, pos, color ?? new(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, 255), 0f, stringSize / 2f, Vector2.One); 
    }
    static bool Hover(string name, Vector2 pos, float drawScale = 1f) {
        Texture2D texture = GetUI(ShortCat[0] + name).GetAsset().Value;
        Vector2 size = texture.Size() * drawScale;
        Rectangle rect = new((int)(pos.X - size.X / 2f), (int)(pos.Y - size.Y / 2f), texture.Width, texture.Height);
        return rect.Contains(Main.mouseX, Main.mouseY);
    }
    static bool Hover(string name, Vector2 pos2, byte frameX = 1, byte frameY = 1, float drawScale = 1f) {
        Texture2D texture = GetUI(ShortCat[0] + name).GetAsset().Value;
        Vector2 size = texture.Size() * drawScale;
        Rectangle rect = new((int)(pos2.X - size.X / 2f), (int)(pos2.Y - size.Y / 2f), texture.Width / frameX, texture.Height / frameY);
        return rect.Contains(Main.mouseX, Main.mouseY);
    }
    protected override void DrawSelf(SpriteBatch spriteBatch) {
        base.DrawSelf(spriteBatch);

        Player player = Main.LocalPlayer;
        bool active = player.Get<AlchemistTilePlayer>().ActiveAlchemistUI;
        float target = active ? 1f : 0f;
        Vector2 basePos = new(Main.screenWidth / 2, Main.screenHeight / 2);
        glowAlpha = MathHelper.Lerp(glowAlpha, target, 0.1f);
        SettingVanillaItemSlot(glowAlpha, basePos);

        Vector2 bassTextPos = new(basePos.X - 140, basePos.Y - 320);
        Vector2 textPos = bassTextPos + GetUI(ShortCat[0] + "TestAlchemicalTableTextPanel").GetAsset().Value.Size() / 2f;
        Vector2 slotPos = new(basePos.X - 68, basePos.Y + 300);
        Vector2 buttonPos = new(basePos.X, basePos.Y - basePos.Y / 2 + 490);

        Draw(spriteBatch, "TestAlchemicalTableUI", basePos);
        Draw(spriteBatch, "TestAlchemicalTableTextPanel", new(basePos.X, basePos.Y - 300));
        Draw(spriteBatch, "TestAlchemicalTableSlotPanel", new(basePos.X, basePos.Y + 300));

        if (flask.Item.type != ItemID.None) { 
            DrawSlot(spriteBatch, slotPos, flask.Item, out string text);
            if (text != "") { DrawText(textPos, text); }
            for (int i = 0; i < 6; i++) {
                if (ReagentSlot[i].Item.type != ItemID.None) {
                    foreach (AlchemyManager recipes in Alchemy.Manager) {               
                        if (CheckRecipe(recipes, ReagentSlot)) {
                            DrawButton(spriteBatch, buttonPos);
                            //flask.Item.Get<AlchemicalItems>().AddReagent(AlchemistReagent.Get<Slime>());

                        }
                    }
                }
                else {
                    if (ReagentSlot[i].IsHoverSlot) { DrawText(textPos, "Reagent"); }
                }
            }
        }

        if (!isFist && flask.IsHoverSlot && flask.Item.type == ItemID.None) { DrawText(textPos, "FlaskItem"); }
    }
    public static bool CheckRecipe(AlchemyManager recipe, VanillaItemSlotWrapper[] slots) {
        List<IngredientData> ingredients = recipe.Ingredients;

        int usedSlots = 0;

        for (int i = 0; i < slots.Length; i++) {
            Item item = slots[i].Item;

            if (item.type == ItemID.None) { continue; }

            bool found = false;

            foreach (IngredientData ingredient in ingredients) {
                if (item.Get<AlchemicalItems>().Reagent == ingredient.Ingredient && item.stack >= ingredient.Stack) {
                    found = true;
                    usedSlots++;
                    break;
                }
            }

            if (!found) { return false; }
        }

        return usedSlots == ingredients.Count;
    }
    void DrawSlot(SpriteBatch sprite, Vector2 pos, Item item, out string text) {
        text = "";
        float scale = 0;
        float textScale = 5f;
        byte frame = 0;
        for (int i = 0; i < 6;) {
            if (item.Get<AlchemicalItems>().FlaskReagents[i] == AlchemistReagent.Get<Look>()) {
                frame = 2;
                if (Hover("TestAlchemicalPotionSlot", new(pos.X + scale - textScale, pos.Y), 2)) { text = "SlotInfo.Locked"; }
            }
            if (item.Get<AlchemicalItems>().FlaskReagents[i] == AlchemistReagent.Get<NoN>()) {
                frame = 0;
                if (Hover("TestAlchemicalPotionSlot", new(pos.X + scale - textScale, pos.Y), 2)) { text = "SlotInfo.Empty"; }
            }
            if (item.Get<AlchemicalItems>().FlaskReagents[i] != AlchemistReagent.Get<Look>() && item.Get<AlchemicalItems>().FlaskReagents[i] != AlchemistReagent.Get<NoN>()) {
                frame = 1;
                if (Hover("TestAlchemicalPotionSlot", new(pos.X + scale - textScale, pos.Y), 2)) { text = "SlotInfo.Сontains"; }
            }
            Draw(sprite, "TestAlchemicalPotionSlot", new(pos.X + scale, pos.Y), new(3, 1, frame));
            scale += 46;
            i++;
        }
    }
    void DrawButton(SpriteBatch sprite, Vector2 pos) {
        button.Init(1, 5);
        button.RowCount = 1;
        button.Update();
        if (Hover("TestAlchemicalPotionSlotFrame1", pos: pos)) {
            if (Main.mouseLeft && Main.mouseRightRelease) { button.StartAnimation = true; }         
        }
        Texture2D buttonTexture = GetUI(ShortCat[0] + "TestAlchemicalTableButton").GetAsset().Value;
        //sprite.Draw(buttonTexture, pos, button.GetSource(buttonTexture), Color.White, 0f, button.GetSource(buttonTexture).Size() / 2f, 1f, button.Effects, 0f);
    }
}