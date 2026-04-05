using Romert.Core;

namespace Romert.Content.Reagents;

public class Slime : AlchemistReagent {
    public override void Register(RegisterReagent register) {
        register.Register(this, (ItemID.Gel, 10));
    }
    public override void Recipe(Alchemy alchemy) {
        alchemy.Create(this);
        alchemy.AddIngredient(this, 10);
        alchemy.Register();
    }
    public override void SetStaticDefaults() {
        SetProjectile(GetReagentProjectile<Proj.Slime>());
    }
    public override bool CanBySynergia(AlchemistReagent reagent) {
        return reagent.Name == "Poison";
    }
    public override void Synergia(Player player, Item item, AlchemistReagent reagent) {
        //SetProjectile(GetReagentProjectile<Proj.Lava>());
        if (reagent.Name == "Poison") {
            SetProjectile(GetReagentProjectile<Proj.Lava>());
        }
        //else {
        //    SetProjectile(GetReagentProjectile<Proj.Lava>());
        //}
    }
}