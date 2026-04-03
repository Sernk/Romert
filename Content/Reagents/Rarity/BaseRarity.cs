using Romert.Core;

namespace Romert.Content.Reagents.Rarity;

public class BaseRarity : ReagentRarity {
    public override void SettingRarity() {
        IsAnimated = true;
        Power = 0;
        Tooltips = "The most common reagents, found everywhere.";
    }
    public override string TexturePatch => VanillaPatch + "HoverReagent_Start_Cristal_Blue";
    public override Color AnimatedColor() => Animated([Color.Red, Color.Green, Color.Blue], 45);
}
