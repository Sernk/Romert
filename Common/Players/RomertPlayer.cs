using Romert.Content.Items.Other;
using System.Collections.Generic;

namespace Romert.Common.Players {
    public class RomertPlayer : ModPlayer {
        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath) {
            itemsByMod["Terraria"].RemoveAll(item => item.type == ItemID.CopperShortsword);
            itemsByMod["Terraria"].Insert(0, new Item(ItemType<AdventurerSpark>()));
        }
    }
}