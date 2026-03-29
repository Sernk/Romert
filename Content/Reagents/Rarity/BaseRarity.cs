using Romert.Core;

namespace Romert.Content.Reagents.Rarity;

public class BaseRarity : ReagentRarity {
    public override void SettingRarity() {
        Color = new(189, 215, 205);
        Tooltips = "[Common]\nThe most common reagents, found everywhere.";
    }
}
