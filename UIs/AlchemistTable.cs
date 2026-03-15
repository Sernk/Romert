using Romert.Common.GlobalItems;
using Romert.Common.Players;
using Romert.Content.Items.Weapons.Alchemical;
using Romert.Helpers;
using Romert.Resources;
using System;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.UI;

namespace Romert.UIs;

public class AlchemistTable : UIState {
    VanillaItemSlotWrapper flask;

    bool isFist = false;

    float glowAlpha = 0f;

    public override void OnInitialize() {
        Left.Set(-57.5f, 0.5f);
        Top.Set(420f, 0f);
        flask = new VanillaItemSlotWrapper() {
            Left = { Pixels = 27.5f }, // 35
            Top = { Pixels = 40.0f }, // 40
            ValidItemFunc = item => item.IsAir || !item.IsAir && item.Get<AlchemicalItems>().isFlask,
        };
        Append(flask);
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
        Main.playerInventory = false;
        if (!isFist) { SoundEngine.PlaySound(SoundID.MenuClose, player.Center); isFist = true; }
        if (flask.Item.type != ItemID.None) { player.QuickSpawnItem(player.GetSource_Misc(Romert.ModName + "DeactivateAlchemistTableUI"), flask.Item.type, flask.Item.stack); flask.Item.type = 0; }
    }
    void LoadTexture(float alfa) {
        flask.ItemTypeTexture = TextureAssets.Item[ItemType<BaseFlask>()].Value;
        flask.Alpha = alfa;
        flask.SlotTexture = GetUI(ShortCat[0] + "TestAlchemicalTableSlotUI").GetAsset().Value;
    }
    void Draw(SpriteBatch spriteBatch, string name, Vector2 pos) => spriteBatch.Draw(GetUI(ShortCat[0] + name).GetAsset().Value, pos, null, Color.White * glowAlpha, 0f, GetUI(ShortCat[0] + name).GetAsset().Value.Size() / 2, 1, SpriteEffects.None, 1f);
    protected override void DrawSelf(SpriteBatch spriteBatch) {
        base.DrawSelf(spriteBatch);
        Player player = Main.LocalPlayer;
        bool active = player.Get<AlchemistTilePlayer>().ActiveAlchemistUI;
        float target = active ? 1f : 0f;
        Vector2 basePos = new(Main.screenWidth / 2, Main.screenHeight / 2);
        glowAlpha = MathHelper.Lerp(glowAlpha, target, 0.1f);
        LoadTexture(glowAlpha);
        Draw(spriteBatch, "TestAlchemicalTableUI", basePos);
    }
}