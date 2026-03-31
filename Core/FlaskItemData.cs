namespace Romert.Core;

public class FlaskItemData(string name) {
    public string Name { get; private set; } = name;

    public void SetItem(Item item) => Item = item;
    public Item Item { get; private set; }

    public void BaseSetting(int price = 0, int rare = 0, int damage = 0) {
        Price = price;
        Rare = rare;
        Damage = damage;
    }

    public int Price { get; private set; }
    public int Rare { get; private set; }
    public int Damage { get; private set; }

    // If reagent add new Projectile || edit
    public void ProjSetting(float projSpeed = 0, int projType = 0, int projDmg = 0, float projknockback = 0) {
        ProjectileSpeed = projSpeed;
        ProjectileType = projType;
        ProjectileDamage = projDmg;
        ProjectileKnockback = projknockback;
    }

    public int ProjectileType { get; private set; }
    public int ProjectileDamage { get; private set; }
    public float ProjectileSpeed { get; private set; }
    public float ProjectileKnockback { get; private set; }
    public override string ToString() => Name;
}