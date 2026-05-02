using Mono.Cecil.Cil;
using MonoMod.Cil;
using Romert.Core;
using System;

namespace Romert.Common.Hooks.ILs;

public class TouchBlock {
    public static void ILHook(ILContext il) {
        ILCursor c = new(il);
        c.GotoNext(MoveType.After, i => i.MatchLdsfld(typeof(TileID.Sets).GetField("TouchDamageHot")), i => i.MatchLdarg(1), i => i.MatchLdelemU1(), i => i.MatchBrfalse(out _));
        c.RemoveRange(6);
        c.Emit(OpCodes.Ldarg_0);
        c.Emit(OpCodes.Ldarg_1);
        c.EmitDelegate<Action<Player, int>>((player, tileId) => {
            if (Touch.Type.TryGetValue(tileId, out int value)) { player.AddBuff(value, 20); }
            else { player.AddBuff(BuffID.Burning, 20); }
        });
    }
}