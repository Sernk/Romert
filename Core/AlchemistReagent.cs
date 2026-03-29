using Romert.Content.Reagents.Rarity;
using Romert.Core.Exceptions;
using Terraria.DataStructures;

namespace Romert.Core;

public abstract class AlchemistReagent : ModType, ILocalizedModType {
    public string LocalizationCategory => "Reagents";
    public string SearchName { get; internal set; }
    public bool Synergy { get; internal set; }
    public AlchemistReagentData CurrentType { get; internal set; }
    public ReagentRarity Rarity { get; internal set; }


    public sealed override void Load() {
        AlchemistReagentManager.ReagentsData.Add(this);
        AlchemistReagentManager.ReagentID.Add(Name, this);
        _ = this.GetLocalization("Name").Value;
        _ = this.GetLocalization("Descriptions").Value;
        if (HasTexture) {
            if (!HasAsset(TexturePatch)) {
                throw new NoTexture(Name);
            }
        }
    }
    protected override void Register() {
        ModTypeLookup<AlchemistReagent>.Register(this);
    }
    // В каком предмете он находится и сколько нужно для открытия в книге!
    public virtual void Register(RegisterReagent register) { }
    public void SetDefaults() {
        SetStaticDefaults();
        SearchName = LocalizationName;
    }

    public virtual bool HasTexture => true;

    public virtual string LocalizationName => this.GetLocalization("Name").Value;
    public virtual string Descriptions => this.GetLocalization("Descriptions").Value;
    public virtual string TexturePatch => (GetType().Namespace + "." + Name).Replace('.', '/');

    // use only add item debuff and add player buff
    public virtual void Buff(Player player, AlchemistData buff) { }
    public virtual FlaskItemData EditItem() => new("");
    public virtual bool CanNewShot(bool synergy) => false;
    public virtual bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) => true;
    public virtual bool CanBySynergia(AlchemistReagent reagent) => false;
    public virtual void Synergia(Player player, Item item, AlchemistReagent reagent) { }
    public virtual void Recipe(Alchemy alchemy) { }
    public virtual void SetRarity(ReagentRarity rarity) {
        Rarity = GetReagentRarity<BaseRarity>();
    }
    public override string ToString() => Name;
}