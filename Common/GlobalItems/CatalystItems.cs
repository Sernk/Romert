using System.Collections.Generic;

namespace Romert.Common.GlobalItems;

public class CatalystItems : GlobalItem {
    public int power = 0;

    public override bool InstancePerEntity => true;
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
        if (power > 0) { tooltips.Add(new(Mod, "CatalystPower", "this item have power: " + power)); }
    }
}