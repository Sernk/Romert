using System.Collections.Generic;

namespace Romert.Core;

public class RegisterReagent : CleaningType {
    public static List<AlchemistReagentData> AlchemistReagents { get; private set; }
    AlchemistReagentData currentReagents = new();

    public sealed override void Load(Mod mod) {
        AlchemistReagents = [];
        if (AlchemistReagentManager.PostLoad) {
            foreach (AlchemistReagent reagent in AlchemistReagentManager.ReagentsData) { reagent.Register(this); }
        }
    }
    public void Register(AlchemistReagent reagent, params(int type, int count)[] item) {
        currentReagents = new() { Reagent = reagent };
        foreach (var (items, stack) in item) { currentReagents.ItemID.Add(new Item(items, stack)); }
        reagent.CurrentType = currentReagents;
        AlchemistReagents.Add(currentReagents);
    }
}