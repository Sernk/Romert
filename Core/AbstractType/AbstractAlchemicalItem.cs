namespace Romert.Core.AbstractType;

public abstract class AbstractAlchemicalItem : ModItem {
    public virtual bool IsFlackItem => false;
    public sealed override void SetDefaults() {
        Item.DamageType = IsFlackItem ? GetInstance<AlchemistsFlask>() : GetInstance<AlchemistsFlask>();
        Item.maxStack = IsFlackItem ? 9999 : 0;
        Item.consumable = IsFlackItem;
        if (IsFlackItem) {
            Item.useStyle = ItemUseStyleID.Swing;
            //Item.ammo = ItemType<BoomFlask>(); 
        }
        PostSetStaticDefaults();
    }
    public virtual void PostSetStaticDefaults() { }
}