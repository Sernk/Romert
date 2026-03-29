using Romert.Core;

namespace Romert.Content.Reagents;

public class Lava : AlchemistReagent {
    public override void Register(RegisterReagent register) {
        register.Register(this, (ItemID.HealingPotion, 10));
    }
}