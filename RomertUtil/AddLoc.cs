using Romert.Common.PlayerLayers;
using Terraria.Localization;

namespace Romert.RomertUtil;

public class AddLoc {
    public const string LocPatch = "Mods.Romert.";
    public static string[] LocCategory { get; private set; } = ["Alchemist", "Tooltips"];

    public static string Loc(string category, string key) {
        if (category == "") { return Language.GetTextValue($"{LocPatch}{key}"); }
        else { return Language.GetTextValue($"{LocPatch}{category}.{key}"); }
    }
}