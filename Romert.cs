using Terraria.UI;

namespace Romert;

public class Romert : Mod {
    public static Mod Instruction { get; private set; }
    Romert() { Instruction = this; }

    public const string ModName = "Romert";

    public UserInterface AlchemistTableUI { get; private set; }

    public override void Load() {
        AlchemistTableUI = new UserInterface();
    }
}