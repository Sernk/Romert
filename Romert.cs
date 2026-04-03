using Romert.Core;
using Terraria.UI;

namespace Romert;

public class Romert : Mod {
    public static Mod Instruction { get; private set; }
    Romert() { Instruction = this; }

    public const string ModName = "Romert";

    public UserInterface AlchemistTableUI { get; private set; }
    public UserInterface AlchemistBookUI { get; private set; }
    public UserInterface ReagentTooltipsUI { get; private set; }

    public override void Load() {
        AlchemistTableUI = new();
        AlchemistBookUI = new();
        ReagentTooltipsUI = new();    
    }
    public override void PostSetupContent() {
        foreach (CleaningType type in GetContent<CleaningType>()) {
            if (type is IPostSetup setup) { setup.PostSetup(this); }
        }
    }
}