using Romert.Core;

namespace Romert.Content.Reagents;

public class Poison : AlchemistReagent {
    public override void Recipe(Alchemy alchemy) {
        alchemy.Create(this);
        alchemy.AddIngredient(Get<Slime>(), 10);
        alchemy.Register();
    }
}