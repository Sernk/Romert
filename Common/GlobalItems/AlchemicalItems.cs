using Romert.Common.Players;
using System.Collections.Generic;

namespace Romert.Common.GlobalItems;

public class AlchemicalItems : GlobalItem {
    public HashSet<int> IsAlchemistPoisoningItems = [];

    public override bool InstancePerEntity => true;
    public override void HoldItem(Item item, Player player) {
        AlchemistPlayer alchemist = AlchemistPlayer.GetPlayer(player);
        if (IsAlchemistPoisoningItems.Contains(item.type)) { alchemist.ActiveCurrentAlchemist = true; alchemist.AlchemistDatas[0].IsActive = true; }
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