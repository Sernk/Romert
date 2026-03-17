namespace Romert.Core;

public abstract class AlchemistReagent : CleaningType {
    public Mod Mod { get; private set; }

    public int Type;

    public string Name => GetType().Name;

    public sealed override void Load(Mod mod) {
        AlchemistReagentManager.ReagentsData.Add(this);
        AlchemistReagentManager.ReagentID.Add(GetType().Name, this);
        Mod = mod;
    }
    public void SetDefaults() {
        Type = AlchemistReagentManager.ReagentsData.IndexOf(this);
        SetStaticDefaults();
    }
    public virtual void SetStaticDefaults() {

    }
    public virtual void Recipe(Alchemy alchemy) {

    }
    public static T Get<T>() where T : AlchemistReagent => (T)(AlchemistReagentManager.ReagentID.TryGetValue(typeof(T).Name, out var value) ? value : null);
}