using Romert.Common.Players;
using Romert.Core;

namespace Romert.Common.GlobalItems;

public class AlchemicalItems : GlobalItem {
    public bool IsAlchemistPoisoning = false;

    public override bool InstancePerEntity => true;
    public override bool CanUseItem(Item item, Player player) {
        bool orgin = base.CanUseItem(item, player);
        AlchemistPlayer alchemist = player.GetModPlayer<AlchemistPlayer>();
        if (IsAlchemistPoisoning) { BaseLogic(alchemist, 0, orgin); }
        return orgin;
    }
    public static bool BaseLogic(AlchemistPlayer player, int id, bool flag) {
        if (player.AlchemistDictionary == null) { return flag; }
        player.AlchemistDictionary.TryGetValue(AlchemistDataID.GetByID(id), out AlchemistData alchemistData);
        if (alchemistData.CurrentProgress != alchemistData.PointsToDebuffTotal + player.BonusMaxPointsToDebuff) { alchemistData.AddPoints(player); }
        Main.NewText(player.CurrentAlchemist.CurrentProgress);
        return flag;
    }
}