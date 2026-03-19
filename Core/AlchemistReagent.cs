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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reagent">Reagent for this Item</param>
    /// <returns></returns>
    public virtual bool CanBySynergia(AlchemistReagent reagent) {
        return false;
    }
    /// <summary>
    /// if(reagent == AlchemistReagent.Get(Name) {
    ///  code
    /// }
    /// </summary>
    /// <param name="player"></param>
    /// <param name="item"></param>
    /// <param name="reagent">reagents for this item</param>
    public virtual void Synergia(Player player, Item item, AlchemistReagent reagent) {

    }
    public virtual void Recipe(Alchemy alchemy) {

    }
    public override string ToString() => Name;

    public static T Get<T>() where T : AlchemistReagent => (T)(AlchemistReagentManager.ReagentID.TryGetValue(typeof(T).Name, out var value) ? value : null);
    public static AlchemistReagent Get(string name) => AlchemistReagentManager.ReagentID.TryGetValue(name, out var value) ? value : null;
}