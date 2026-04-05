using Romert.Core;
using Terraria.DataStructures;

namespace Romert.Content.Reagents;

public class Poison : AlchemistReagent {
    public override void Recipe(Alchemy alchemy) {
        alchemy.Create(this);
        alchemy.AddIngredient(this, 10);
        alchemy.Register();
    }
    public override void Register(RegisterReagent register) {
        register.Register(this, (ItemID.Stinger, 10));
    }
    public override void Buff(Player player, AlchemistData buff) {
     
    }
    public override FlaskItemData EditItem() {
        FlaskItemData data = new(LocalizationName);
        data.BaseSetting(10000, damage: 10);
        return data;
    }
    public override bool CanNewShot(bool synergy) => true;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
      //  Projectile.NewProjectile(source, position, velocity, ProjectileID.RubyBolt, damage, knockback, player.whoAmI);
        return true;
    }
}