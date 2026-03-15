using System;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

namespace Romert.UIs;

public class VanillaItemSlotWrapper : UIElement {
    public Texture2D ItemTypeTexture;
    public Texture2D SlotTexture;
    public Item Item;
    private readonly int _context;
    private readonly float _scale;
    public Func<Item, bool> ValidItemFunc;
    public Predicate<Item> IsVisible;
    public float Alpha = 1f;

    public VanillaItemSlotWrapper(int context = ItemSlot.Context.BankItem, float scale = 1f) {
        _context = context;
        _scale = scale;
        Item = new Item();
        Item.SetDefaults(ItemID.None);

        Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
        Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
    }
    protected override void DrawSelf(SpriteBatch spriteBatch) {
        if (Item != null) { if (IsVisible != null && !IsVisible(Item)) { return; } }
        float oldScale = Main.inventoryScale;
        Color color = Color.White * Alpha;
        float drawScale = _scale * 1.25f;
        Main.inventoryScale = _scale;
        Rectangle rectangle = GetDimensions().ToRectangle();
        Vector2 slotCenter = rectangle.TopLeft() + new Vector2(TextureAssets.InventoryBack9.Value.Width * _scale / 2f, TextureAssets.InventoryBack9.Value.Height * _scale / 2f);

        if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface) {
            Main.LocalPlayer.mouseInterface = true;
            if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem)) { ItemSlot.Handle(ref Item, _context); }
        }

        spriteBatch.Draw(SlotTexture, slotCenter, null, color, 0f, SlotTexture.Size() / 2f, drawScale, SpriteEffects.None, 0);

        if (Item.type != ItemID.None) { spriteBatch.Draw(TextureAssets.Item[Item.type].Value, slotCenter, null, color, 0f, TextureAssets.Item[Item.type].Value.Size() / 2f, drawScale, SpriteEffects.None, 0f); }
        if (Item.IsAir && ItemTypeTexture != null) { spriteBatch.Draw(ItemTypeTexture, slotCenter, null, color * 0.4f, 0f, ItemTypeTexture.Size() / 2f, drawScale, SpriteEffects.None, 0f); }

        Main.inventoryScale = oldScale;
    }
}