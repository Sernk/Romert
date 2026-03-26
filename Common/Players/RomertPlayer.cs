using Romert.Content.Items.Other;
using System.Collections.Generic;

namespace Romert.Common.Players;

public class RomertPlayer : ModPlayer {
    /// <summary> -1 empty, 0 - mele, 1 - range, 2 - throwing, 3 - achemist, 4 - mag, 5 - summon, 6 ... other mod class   </summary>
    public int Class = 3; 

    public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath) {
        itemsByMod["Terraria"].RemoveAll(item => item.type == ItemID.CopperShortsword);
        itemsByMod["Terraria"].Insert(0, new Item(ItemType<AdventurerSpark>()));
    }

}