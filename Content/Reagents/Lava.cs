using Romert.Content.Reagents.Rarity;
using Romert.Core;

namespace Romert.Content.Reagents;

public class Lava : AlchemistReagent {
    public override void Register(RegisterReagent register) {
        register.Register(this, (ItemID.HealingPotion, 10));
    }
    public override void Recipe(Alchemy alchemy) {
        alchemy.Create(this);
        alchemy.AddIngredient(this, 10);
        alchemy.Register();
    }
    public override void SetStaticDefaults() {
        SetRarity(GetReagentRarity<Test>());
    }
}