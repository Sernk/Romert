using Romert.Common.Players;
using Romert.Core;
using System.Collections.Generic;

namespace Romert.Common.GlobalItems;

public class AlchemicalItems : GlobalItem {
    public HashSet<int> IsAlchemistPoisoningItems = [];

    public override bool InstancePerEntity => true;
    public override void HoldItem(Item item, Player player) {
        AlchemistPlayer alchemist = AlchemistPlayer.GetPlayer(player);
        if (IsAlchemistPoisoningItems.Contains(item.type)) { alchemist.AlchemistDatas[0].IsActive = true; }
    }
    public override bool CanUseItem(Item item, Player player) {
        bool orgin = base.CanUseItem(item, player);
        AlchemistPlayer alchemist = player.GetModPlayer<AlchemistPlayer>();
        if (IsAlchemistPoisoningItems.Contains(item.type)) { alchemist.AddPoints(0, orgin); }
        return orgin;
    }
    public override void UpdateAccessory(Item item, Player player, bool hideVisual) {
        //if(item.type == 5000) {
        //    //AlchemistPlayer player1 = player.GetModPlayer<AlchemistPlayer>();
        //    //player1.CurrentAlchemist.ModifyPointsEarned += 9;
        //    //player1.BonusPointsEarned += 20;
        //    ////player1.CurrentAlchemist.ResetPoints();
        //    //Main.NewText(player1.CurrentAlchemist.CurrentProgress);
        //}
    }
}