using Romert.Common.GlobalItems;
using Romert.Common.RomertRarity;
using Terraria.DataStructures;

namespace Romert.Content.Items.Other {
	// Vanilla Tremor Item
	public class AdventurerSpark : ModItem {
		public override void SetStaticDefaults() {
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			ItemID.Sets.ItemIconPulse[Item.type] = true;
		}
		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 22;
            Item.rare = RarityType<Soul>();
			Item.value = Item.buyPrice(silver: 1);
			Item.GetGlobalItem<RomertItems>().oldItem = true;

        }
        //     public override bool PreDrawTooltip(ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y) {
        //         SpriteBatch sprite = Main.spriteBatch;
        //		   Texture2D icon = Textures.GetTextureName("OriginalClassToolTips").GetAsset().Value;
        //         int frame = (int)(Main.GlobalTimeWrappedHourly * 2f) % 4;

        //         sprite.Draw(icon, new Vector2(x + 10, y+10), icon.Frame(4, 1, frame), Color.White, 0f, icon.Size() / 2, 1, SpriteEffects.None, 1);
        //         return true;
        //     }
    }
}