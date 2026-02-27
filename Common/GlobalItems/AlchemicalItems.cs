using Romert.Common.Players;
using Romert.Core;

namespace Romert.Common.GlobalItems;

// TODO: Normal name for vareable. Method for active debuff
public class AlchemicalItems : GlobalItem {
    public bool IsTestDebuffItem = false;

    public override bool InstancePerEntity => true;
    public override bool CanUseItem(Item item, Player player) {
        if (IsTestDebuffItem) {
            player.GetModPlayer<AlchemistPlayer>().AlchemistDictionary.TryGetValue(AlchemistDataID.GetByID(0), out AlchemistData alchemistData);
            if (alchemistData.ProgressToDebuff != 100) { alchemistData.ProgressToDebuff++; }
        }
        return base.CanUseItem(item, player);
    }
}
