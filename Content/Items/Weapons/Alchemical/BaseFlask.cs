using Romert.Helpers;

namespace Romert.Content.Items.Weapons.Alchemical;

// TODO: Projectile and rare;
public class BaseFlask : AbstractAlchemicalItem {
    public override bool IsFlackItem => true;
    public override void PostSetStaticDefaults() {
        Item.damage = 9;
        Item.width = 26;
        Item.noUseGraphic = true;
        Item.height = 30;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.shoot = ProjectileID.Shuriken;
        Item.shootSpeed = 8f;
        Item.knockBack = 1;
        Item.UseSound = SoundID.Item106;
        Item.value = 30;
        Item.rare = ItemRarityID.Blue;
        Item.autoReuse = false;
        Item.IsAlchemistPoisoning();
    }
}