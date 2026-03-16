using Romert.Common.GlobalItems;
using Romert.Common.Players;
using Romert.Content.Items.Weapons.Alchemical;
using Romert.Resources;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Romert.UIs;

public class AlchemistTable : UIState {
    VanillaItemSlotWrapper flask;
    VanillaItemSlotWrapper leftTopSlot;

    bool isFist = false;

    float glowAlpha = 0f;

    public override void OnInitialize() {
        flask = new() { ValidItemFunc = item => item.IsAir || !item.IsAir && item.Get<AlchemicalItems>().isFlask };
        leftTopSlot = new() {
            ValidItemFunc = item => item.IsAir || !item.IsAir && item.Get<AlchemicalItems>().isFlask,
            IsVisible = item => flask.Item.type != ItemID.None
        };

        Append(flask);
        Append(leftTopSlot);
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
        if (flask.Item.type != ItemID.None) { player.QuickSpawnItem(player.GetSource_Misc(Romert.ModName + "DeactivateAlchemistTableUI"), flask.Item.type, flask.Item.stack); flask.Item.type = 0; }
    }
    void SettingVanillaItemSlot(float alfa, Vector2 pos) {
        BaseSetting(flask, alfa, pos);

        Vector2 leftPos = new(pos.X - pos.X / 2 + 220, pos.Y - pos.Y / 2 + 30);

        BaseSetting(leftTopSlot, alfa, leftPos);
    }
    static void BaseSetting(VanillaItemSlotWrapper slot, float alfa, Vector2 pos, Texture2D textureItem = null, string textureName = null) {
        slot.ItemTypeTexture = textureItem is null ? TextureAssets.Item[ItemType<BaseFlask>()].Value : textureItem;
        slot.Alpha = alfa;
        string patch = textureName is null ? "TestAlchemicalTableSlotUI" : textureName;
        slot.SlotTexture = GetUI(ShortCat[0] + patch).GetAsset().Value;
        slot.Position = pos;
    }
    void Draw(SpriteBatch spriteBatch, string name, Vector2 pos) => spriteBatch.Draw(GetUI(ShortCat[0] + name).GetAsset().Value, pos, null, Color.White * glowAlpha, 0f, GetUI(ShortCat[0] + name).GetAsset().Value.Size() / 2, 1, SpriteEffects.None, 1f);
    static Vector2 DrawText(Vector2 pos, string locKey, Color? color = null) {
        string name = Loc(LocCategory[0] + ".Table", locKey);
        Vector2 stringSize = FontAssets.MouseText.Value.MeasureString(name);
        return ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, name, pos, color ?? new(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, 255), 0f, stringSize / 2f, Vector2.One); 
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

        Draw(spriteBatch, "TestAlchemicalTableUI", basePos);
        Draw(spriteBatch, "TestAlchemicalTableTextPanel", new(basePos.X, basePos.Y - 300));

        if (!isFist && flask.IsHoverSlot && flask.Item.type == ItemID.None) { DrawText(textPos, "FlaskItem"); }
    }
}