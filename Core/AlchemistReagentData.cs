using System.Collections.Generic;

namespace Romert.Core;

public class AlchemistReagentData {
    public List<Item> ItemID { get; set; } = [];
    public AlchemistReagent Reagent { get; set; }
}