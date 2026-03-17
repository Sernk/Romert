using System;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

namespace Romert.UIs;

public class VanillaItemSlotWrapper : UIElement {
    public Texture2D ItemTypeTexture;
    public Texture2D SlotTexture;
    public Item Item;
    public Func<Item, bool> ValidItemFunc;
    public Predicate<Item> IsVisible;
    public float Alpha = 1f;
    public Vector2 Position = Vector2.Zero;
    public bool IsHoverSlot { get; private set; } = false;

    readonly int _context;
    readonly float _scale;

    public VanillaItemSlotWrapper(int context = ItemSlot.Context.BankItem, float scale = 1f) {
        _context = context;
        _scale = scale;
        Item = new Item();
        Item.SetDefaults(ItemID.None);

        Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
        Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
    }
    public string Name => GetType().Name;
    protected override void DrawSelf(SpriteBatch spriteBatch) {
        if (Item != null) { if (IsVisible != null && !IsVisible(Item)) { return; } }
        float oldScale = Main.inventoryScale;
        Color color = Color.White * Alpha;
        float drawScale = _scale * 1.25f;
        Main.inventoryScale = _scale;

        SlotTexture ??= TextureAssets.InventoryBack9.Value;

        Vector2 size = SlotTexture.Size() * drawScale;
        Rectangle rect = new((int)(Position.X - size.X / 2f), (int)(Position.Y - size.Y / 2f), SlotTexture.Width + 10, SlotTexture.Height + 10);

        bool hover = rect.Contains(Main.mouseX, Main.mouseY);

        if (hover && !PlayerInput.IgnoreMouseInterface) {
            Main.LocalPlayer.mouseInterface = true;
            IsHoverSlot = true;
            if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem)) { ItemSlot.Handle(ref Item, _context); }
        }
        else { IsHoverSlot = false; }

        spriteBatch.Draw(SlotTexture, Position, null, color, 0f, SlotTexture.Size() / 2f, drawScale, SpriteEffects.None, 0);

        if (Item.type != ItemID.None) { spriteBatch.Draw(TextureAssets.Item[Item.type].Value, Position, null, color, 0f, TextureAssets.Item[Item.type].Value.Size() / 2f, drawScale, SpriteEffects.None, 0f); }
        if (Item.IsAir && ItemTypeTexture != null) { spriteBatch.Draw(ItemTypeTexture, Position, null, color * 0.4f, 0f, ItemTypeTexture.Size() / 2f, drawScale, SpriteEffects.None, 0f); }

        Main.inventoryScale = oldScale;
    }
}