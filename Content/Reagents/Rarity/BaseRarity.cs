using Romert.Core;

namespace Romert.Content.Reagents.Rarity;

public class BaseRarity : ReagentRarity {
    public override void SettingRarity() {
        Color = new(189, 215, 205);
        IsAnimated = true;
        Colors = [new(189, 215, 205), new(169, 195, 185), new(149, 175, 165)];
        Tooltips = "The most common reagents, found everywhere.";
    }
}
