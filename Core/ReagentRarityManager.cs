using System.Collections.Generic;

namespace Romert.Core;

public class ReagentRarityManager : CleaningType {
    public static Dictionary<string, ReagentRarity> Rarities { get; private set; } = [];
    public static List<ReagentRarity> RaritiesData { get; private set; } = [];
    public static bool PostLoad { get; private set; } = false;

    public override void Load(Mod mod) {
        if (AlchemistReagentManager.PostLoad) {
            foreach (ReagentRarity rarity in RaritiesData) {
                rarity.Register();
            }
        }
        PostLoad = AlchemistReagentManager.PostLoad;
        if (PostLoad) {
            foreach (ReagentRarity rarity in RaritiesData) {
                rarity.AddID();
            }
        }
    }
}
