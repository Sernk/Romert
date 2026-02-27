using Romert.Common.RomertRarity;
using System.Collections.ObjectModel;
using static Romert.Resources.Textures;

namespace Romert.Common.GlobalItems {
    public class RomertItems : GlobalItem {
        public bool oldItem;

        public override bool InstancePerEntity => true;
        public override bool PreDrawTooltip(Item item, ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y) {
            if (oldItem) {
                SpriteBatch sprite = Main.spriteBatch;
                sprite.Draw(GetTextureName("OriginalItemToolTips").GetAsset().Value, new Vector2(x, y - 20), Color.White);
            }
            return true;
        }
        public override void PostDrawTooltip(Item item, ReadOnlyCollection<DrawableTooltipLine> lines) {
            if (item.rare == RarityType<Soul>()) {
                Soul.DrawCustomTooltipLine(lines[0]);
            }
        }
    }
}