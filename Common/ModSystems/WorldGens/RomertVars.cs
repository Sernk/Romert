using System.IO;
using Terraria.ModLoader.IO;

namespace Romert.Common.ModSystems.WorldGens;

public class RomertVars : ModSystem {
    public static int JungleLeftX { get; internal set; } = 0;
    public static int JungleRightX { get; internal set; } = 0;
    public static int JungleCenterX { get; internal set; } = 0;

    void Clear() {
        JungleLeftX = 0;
        JungleRightX = 0;
        JungleCenterX = 0;
    }
    public override void SaveWorldData(TagCompound tag) {
        tag[$"{Romert.ModName}:{JungleLeftX}"] = JungleLeftX;
        tag[$"{Romert.ModName}:{JungleRightX}"] = JungleRightX;
        tag[$"{Romert.ModName}:{JungleCenterX}"] = JungleCenterX;
    }
    public override void LoadWorldData(TagCompound tag) {
        JungleLeftX = tag.GetInt($"{Romert.ModName}:{JungleLeftX}");
        JungleRightX = tag.GetInt($"{Romert.ModName}:{JungleRightX}");
        JungleCenterX = tag.GetInt($"{Romert.ModName}:{JungleCenterX}");
    }
    public override void NetSend(BinaryWriter writer) {
        writer.Write(JungleLeftX);
        writer.Write(JungleRightX);
        writer.Write(JungleCenterX);
    }
    public override void NetReceive(BinaryReader reader) {
        JungleLeftX = reader.ReadInt16();
        JungleRightX = reader.ReadInt16();
        JungleCenterX = reader.ReadInt16();
    }
    public override void OnWorldLoad() => Clear();
    public override void ClearWorld() => Clear();
}
