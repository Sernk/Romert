namespace Romert.Core;

public abstract class AlchemistReagent : CleaningType {
    public string Name => GetType().Name;
    public sealed override void Load(Mod mod) {
        AlchemistReagentManager.ReagentsData.Add(this);
        AlchemistReagentManager.ReagentID.Add(Name, this);
    }
    public void SetDefaults() {
        SetStaticDefaults();
    }
    public virtual void SetStaticDefaults() {

    }
    public virtual bool CanBySynergia(AlchemistReagent reagent) {
        return false;
    }
    public virtual void Synergia(Player player, Item item, AlchemistReagent reagent) {

    }
    public virtual void Recipe(Alchemy alchemy) {

    }
    public override string ToString() => Name;

    public static T Get<T>() where T : AlchemistReagent => (T)(AlchemistReagentManager.ReagentID.TryGetValue(typeof(T).Name, out var value) ? value : null);
    public static AlchemistReagent Get(string name) => AlchemistReagentManager.ReagentID.TryGetValue(name, out var value) ? value : null;
}