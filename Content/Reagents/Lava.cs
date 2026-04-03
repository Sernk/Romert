using Romert.Content.Reagents.Rarity;
using Romert.Core;

namespace Romert.Content.Reagents;

public class Lava : AlchemistReagent {
    public override void Register(RegisterReagent register) {
        register.Register(this, (ItemID.HealingPotion, 10));
    }
    public override void SetRarity(ReagentRarity rarity) {
        Rarity = GetReagentRarity<Test>();
    }
}