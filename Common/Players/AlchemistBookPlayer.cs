using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace Romert.Common.Players;

public class AlchemistBookPlayer : ModPlayer {
    public bool ActiveUI;
    public List<string> SaveType = [];

    public override void LoadData(TagCompound tag) {
        SaveType = tag.Get<List<string>>($"{Romert.ModName}_Open_Reagents");
    }
    public override void SaveData(TagCompound tag) {
        tag[$"{Romert.ModName}_Open_Reagents"] = SaveType;
    }
    public override void Initialize() {
        ActiveUI = false;
        SaveType = [];
    }
    public override void PostUpdate() {

    }
}