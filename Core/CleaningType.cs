namespace Romert.Core;

public class CleaningType : ILoadable, IPostSetup {
    public virtual void Load(Mod mod) { }
    public virtual void PostSetup(Mod mod) { }
    public virtual void Unload() { }
}