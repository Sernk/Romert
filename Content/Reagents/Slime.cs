using Romert.Core;

namespace Romert.Content.Reagents;

public class Slime : AlchemistReagent {
    public override void Register(RegisterReagent register) {
        register.Register(this, (ItemID.Gel, 10));
    }
}