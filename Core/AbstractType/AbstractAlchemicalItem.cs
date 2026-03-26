namespace Romert.Core.AbstractType;

public abstract class AbstractAlchemicalItem : ModItem {
    public virtual bool IsFlackItem => false;
    public sealed override void SetDefaults() {
        Item.DamageType = IsFlackItem ? GetInstance<AlchemistsFlask>() : GetInstance<AlchemistsFlask>();
        Item.maxStack = IsFlackItem ? 9999 : 1;
        Item.consumable = IsFlackItem;
        Item.useStyle = ItemUseStyleID.Swing;
        if (IsFlackItem) {
            Item.useStyle = ItemUseStyleID.Swing;
        }
        PostSetDefaults();
    }
    public virtual void PostSetDefaults() { }
}