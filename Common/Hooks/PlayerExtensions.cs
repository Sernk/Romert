using Romert.Common.Hooks.ILs;

namespace Romert.Common.Hooks; 
public class PlayerExtensions : ILoadable {
    public void Load(Mod mod) {
        IL_Player.ApplyTouchDamage += TouchBlock.ILHook;
    }
    public void Unload() {
        IL_Player.ApplyTouchDamage -= TouchBlock.ILHook;
    }
}