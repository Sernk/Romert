using System.Collections.Generic;

namespace Romert.Core;

public class Touch {
    public static Dictionary<int, int> Type { get; private set; } = [];

    public static void Register(int blockType, int buffType) {
        TileID.Sets.TouchDamageHot[blockType] = true;
        Type.TryAdd(blockType, buffType);
    }
}