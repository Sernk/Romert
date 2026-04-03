using Romert.Core;

namespace Romert.Content.Reagents.Rarity;

public class Test : ReagentRarity {
    public override void SettingRarity() {
        IsAnimated = true;
        Power = 1;
        Tooltips = "Test.";
    }
    public override string TexturePatch => VanillaPatch + "HoverReagent_Start_Cristal_Purple";
    public override Color AnimatedColor() => Animated([Color.AliceBlue, Color.AntiqueWhite, Color.Plum], 25);
}