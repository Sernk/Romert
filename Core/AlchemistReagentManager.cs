using System.Collections.Generic;

namespace Romert.Core;

public class AlchemistReagentManager : CleaningType {
    public static List<AlchemistReagent> ReagentsData { get; private set; } = [];
    public static Dictionary<string, AlchemistReagent> ReagentID { get; private set; } = [];
    public static bool PostLoad { get; private set; } = false;
   
    public override void Load(Mod mod) {
        foreach (AlchemistReagent reagent in ReagentsData) {
            reagent.SetDefaults();
        }
        PostLoad = true;
    }
}