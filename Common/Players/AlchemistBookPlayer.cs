using Romert.Core;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace Romert.Common.Players;

public class AlchemistBookPlayer : ModPlayer {
    public bool HasBook;
    public bool ActiveUI;
    public bool ActiveRecipeUI;
    public bool VisibleBookInfo;

    public AlchemistReagent PreviewReagent;

    public List<string> OpenType { get; private set; } = [];
    public List<string> Current  { get; private set; } = [];
    public List<string> Locked   { get; private set; } = [];
    public List<Item> LockedType { get; private set; } = [];


    public override void LoadData(TagCompound tag) {
        VisibleBookInfo = tag.GetBool($"{Romert.ModName}:Visible");
        
        OpenType = tag.Get<List<string>>($"{Romert.ModName}:Open_Reagents");
        Current =  tag.Get<List<string>>($"{Romert.ModName}:Current_Reagents");
        Locked = tag.Get<List<string>>($"{Romert.ModName}:Locked_Reagents");
        if (tag.TryGet($"{Romert.ModName}:LockedType_Reagents", out List<TagCompound> list)) {
            foreach (TagCompound t in list) { LockedType.Add(ItemIO.Load(t)); }
        }
    }
    public override void SaveData(TagCompound tag) {
        tag[$"{Romert.ModName}:Visible"] = VisibleBookInfo;

        tag[$"{Romert.ModName}:Open_Reagents"] = OpenType;
        tag[$"{Romert.ModName}:Current_Reagents"] = Current;
        tag[$"{Romert.ModName}:Locked_Reagents"] = Locked;
        List<TagCompound> locked = [];
        foreach (Item item in LockedType) { locked.Add(ItemIO.Save(item)); }
        tag[$"{Romert.ModName}:LockedType_Reagents"] = locked;
    }
    public override void Initialize() {
        HasBook = false;
        ActiveUI = false;
        ActiveRecipeUI = false;
        VisibleBookInfo = true;
        PreviewReagent = null;
        OpenType = [];
        Current  = [];
        LockedType = [];
    }
    public override void ResetEffects() {
        HasBook = false;
        ActiveRecipeUI = false;
        PreviewReagent = null;
    }
    public override void PostUpdate() {
        for (int i = 0; i < 58; i++) {
            if (Player.inventory[i].type == ItemType<Content.Items.Other.AlchemistBook>() && Player.inventory[i].stack > 0) {
                HasBook = true;
                break;
            }
        }
        //Main.NewText(ReagentRarityManager.RaritiesData[0].Tooltips);
    }
}