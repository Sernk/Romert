using Romert.Common.Players;
using Romert.Content.Reagents;
using Romert.Content.Reagents.Flask;
using Romert.Core;
using Romert.Core.Exceptions;
using Romert.Resources;
using Romert.UIs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.GameContent;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace Romert.Common.GlobalItems;

public class AlchemicalItems : GlobalItem {
    public HashSet<int> IsAlchemistPoisoningItems = [];

    public AlchemistReagent Reagent;
    public AlchemistReagent[] FlaskReagents { get; private set; } = new AlchemistReagent[6];

    public bool isFlask;
    public bool isReagent;
    public bool isAlchemistMaterials;

    float glowAlpha = 0f;

    public int[] FlaskSlot { get; private set; } = new int[6]; // -1 is look slot, 0 is empty slot, 1 is has value is slot. [-∞; -2] U [2; ∞] is Exception 

    public UIState ReagentTooltips { get; private set; } = null;

    public override bool InstancePerEntity => true;
    public sealed override void SaveData(Item item, TagCompound tag) {
        if (isFlask) {
            List<string> ReagentName = [];
            for (int i = 0; i < FlaskReagents.Length; i++) {
                if (ReagentName.Count != 6) { ReagentName.Add(FlaskReagents[i].Name); }
            }
            for (int i = FlaskReagents.Length - 1; i >= 0; i--) {
                if (FlaskReagents[i] != GetReagent<Look>() && FlaskReagents[i] != GetReagent<NoN>()) {
                    if (ReagentName[i] == "NoN") {
                        ReagentName.RemoveAt(ReagentName.IndexOf("NoN"));
                        ReagentName.Insert(i, FlaskReagents[i].Name);
                    }
                }
            }
            tag[Romert.ModName + "ActiveReagent"] = ReagentName;
        }
    }
    public sealed override void LoadData(Item item, TagCompound tag) {
        if (isFlask) {
            List<string> ReagentName = tag.Get<List<string>>(Romert.ModName + "ActiveReagent");
            if (ReagentName.Count != 0) {
                for (int i = 0; i < FlaskReagents.Length; i++) {
                    FlaskReagents[i] = GetReagent(ReagentName[i]);
                }
            }
        }
    }
    public override void SetDefaults(Item entity) {
        if (isAlchemistMaterials) { entity.material = true; }
        if (AlchemistReagentManager.PostLoad) {
            for (int i = 0; i < RegisterReagent.AlchemistReagents.Count; i++) {
                for (int j = 0; j < RegisterReagent.AlchemistReagents[i].ItemID.Count; j++) {
                    if (entity.type == RegisterReagent.AlchemistReagents[i].ItemID[j].type) { Reagent = RegisterReagent.AlchemistReagents[i].Reagent; }
                }
            }
            if (!isReagent || isFlask) { Reagent??= GetReagent<NoNInItem>(); }
            if (Reagent != GetReagent<NoNInItem>()) { isReagent = true; }
            if (isFlask) {
                if (!Lists.Items.FlaskItem.Exists(x => x == entity.type)) { Lists.Items.FlaskItem.Add(entity.type); }
                for (int i = 0; i < FlaskSlot.Length; i++) {
                    if (FlaskSlot[i] == -1) { FlaskReagents[i] = GetReagent<Look>(); }
                    if (FlaskSlot[i] == 0) { FlaskReagents[i] = GetReagent<NoN>(); }
                }
            }
            if (isReagent && !Lists.Items.ReagentItem.Exists(x => x == entity.type)) { Lists.Items.ReagentItem.Add(entity.type); }
            if (isFlask && !Lists.Projectiles.FlackProjType.Exists(x => x == entity.shoot)) { Lists.Items.ReagentItem.Add(entity.shoot); }
        }
    }
    public override bool PreDrawTooltip(Item item, ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y) {
        Vector2 centePos;
        float scale = 4;
        float max = 0;
        for (int i = 0; i < lines.Count; i++) {
            centePos = FontAssets.MouseText.Value.MeasureString(lines[i].Text);
            scale += centePos.Y;
            if (max < centePos.X) { max = centePos.X;}
        }
        if (ReagentTooltips == null && Main.LocalPlayer.Get<AlchemistBookPlayer>().HasBook && Main.LocalPlayer.Get<AlchemistBookPlayer>().VisibleBookInfo) {
            if (FlaskReagents[0] != null) { ReagentTooltips = new ReagentTooltips(new(x - 12, y + scale + 12), new(x + max / 2, y + scale - 1), item, Reagent, FlaskReagents); }
            else {
                if (Reagent != GetReagent<NoNInItem>()) { ReagentTooltips = new ReagentTooltips(new(x - 12, y + scale + 12), new(x + max / 2, y + scale - 1), item, Reagent, FlaskReagents); }
            }
            GetInstance<Romert>().ReagentTooltipsUI.SetState(ReagentTooltips);
        }
        DrawReagentTooltipHeader(Main.spriteBatch, new(x - 12, y + scale + 12), new(x + max / 2, y + scale - 1), Reagent);
        return base.PreDrawTooltip(item, lines, ref x, ref y);
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
        foreach (TooltipLine line in tooltips) {
            if (isAlchemistMaterials) {
                if (line.Name == "Material") { line.Text = Loc(LocCategory[0] + "." + LocCategory[1], "Material"); }
            }
        }
    }
    public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
        bool active = Main.LocalPlayer.Get<AlchemistTilePlayer>().ActiveAlchemistUI;
        if (isFlask) {
            float target = active ? 1f : 0f;
            glowAlpha = MathHelper.Lerp(glowAlpha, target, 0.1f);
            spriteBatch.Draw(GetTexture("Glow").GetAsset().Value, new(position.X, position.Y ), null, Color.AliceBlue * glowAlpha, 0f, GetTexture("Glow").GetAsset().Value.Size() / 2f, 0.15f, SpriteEffects.None, 0f);
        }
        return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    }
    public override void HoldItem(Item item, Player player) {
        // UpdateInventory
        if (IsAlchemistPoisoningItems.Contains(item.type)) { player.Get<AlchemistPlayer>().AlchemistDatas[0].IsActive = true; }
        if (FlaskReagents != null) {
            for (int i = 0; i < FlaskReagents.Length; i++) {
                for (int j = 0; j < FlaskReagents.Length; j++) {
                    if (FlaskReagents[i] != null && FlaskReagents[i] != null && FlaskReagents[i] != GetReagent<NoN>() && FlaskReagents[j] != GetReagent<NoN>() && FlaskReagents[i] != GetReagent<Look>() && FlaskReagents[j] != GetReagent<Look>()) {
                        if (FlaskReagents[i].CanBySynergia(FlaskReagents[j])) {
                            FlaskReagents[i].Synergy = true; ;
                        }
                        else { FlaskReagents[i].Synergy = false; }
                        if (FlaskReagents[i].Synergy) {
                            FlaskReagents[i].Synergia(player, item, FlaskReagents[j]);
                        }
                        else { FlaskReagents[i].SetStaticDefaults(); }
                    } 
                }
            }
        }
    }
    public override void UpdateInventory(Item item, Player player) {
        // Book open system
        AlchemistBookPlayer bookPlayer = player.Get<AlchemistBookPlayer>();
        if (Reagent != null && Reagent.HasTexture) {
            for (int i = 0; i < Reagent.CurrentType.ItemID.Count; i++) {
                if (item.type == Reagent.CurrentType.ItemID[i].type) {
                    if (item.stack >= Reagent.CurrentType.ItemID[i].stack) {
                        if (!bookPlayer.OpenType.Exists(x => x == Reagent.CurrentType.Reagent.Name)) {
                            bookPlayer.Current.Remove(Reagent.CurrentType.Reagent.Name);
                            bookPlayer.Locked.Remove(Reagent.CurrentType.Reagent.Name);
                            bookPlayer.LockedType.Remove(Reagent.CurrentType.ItemID[i]);
                            bookPlayer.OpenType.Add(Reagent.CurrentType.Reagent.Name);
                        }
                    }
                    else {
                        if (!bookPlayer.OpenType.Exists(x => x == Reagent.CurrentType.Reagent.Name)) {
                            if (!bookPlayer.LockedType.Exists(x => x.type == Reagent.CurrentType.ItemID[i].type)) {
                                bookPlayer.Current.Add(Reagent.CurrentType.Reagent.Name);
                                bookPlayer.Locked.Add(Reagent.CurrentType.Reagent.Name);
                                bookPlayer.LockedType.Add(item);
                            }
                            for (int k = 0; k < bookPlayer.LockedType.Count; k++) {
                                if (bookPlayer.LockedType[k].type == Reagent.CurrentType.ItemID[i].type) {
                                    if (bookPlayer.LockedType[k].stack < item.stack) {
                                        bookPlayer.LockedType[k].stack = item.stack;
                                    }
                                }
                            }
                        }
                        else { return; }
                    }
                }
            }
        }
        if (!Lists.Items.FlaskItem.Contains(Main.HoverItem.type) && !Lists.Items.ReagentItem.Contains(Main.HoverItem.type) && !Main.LocalPlayer.Get<AlchemistBookPlayer>().HasBook && !Main.LocalPlayer.Get<AlchemistBookPlayer>().VisibleBookInfo) {
            ReagentTooltips = null;
            GetInstance<Romert>().ReagentTooltipsUI.SetState(ReagentTooltips);
        }
    }
    public override bool CanUseItem(Item item, Player player) {
        bool orig = base.CanUseItem(item, player);
        AlchemistPlayer alchemist = player.Get<AlchemistPlayer>();
        if (IsAlchemistPoisoningItems.Contains(item.type)) { alchemist.AddPoints(0, orig); }
        return orig;
    }
    public override void UpdateAccessory(Item item, Player player, bool hideVisual) {
    }
    public void AddSlot(string name, int index0 = -1, int index1 = -1, int index2 = -1, int index3 = -1, int index4 = -1, int index5 = -1) {
        FlaskSlot = [index0, index1, index2, index3, index4, index5];
        for (int i = 0; i < FlaskSlot.Length; i++) {
            if (FlaskSlot[i] != 0 && FlaskSlot[i] != -1 && FlaskSlot[i] != 1) { throw new UnknownNumber(FlaskSlot[i], i, name); }
        }
    }
    public void AddReagent(AlchemistReagent reagent) {
        int slot = -1;
        for (int i = FlaskReagents.Length - 1; i >= 0; i--) { if (FlaskReagents[i] == GetReagent<NoN>()) { slot = i; break; } }
        if (slot != -1) { FlaskReagents[slot] = reagent; }
    }
    public void DrawReagentTooltipHeader(SpriteBatch sb, Vector2 pos, Vector2 centerPos, AlchemistReagent reagent) {
        float tooltipsSize = pos.X + 12 - centerPos.X;
        // I hate it when variables are created just for a one-time use like this, but in this case, there’s no way around it
        Rectangle moreLeft = new((int)(centerPos.X + tooltipsSize + 12), (int)centerPos.Y - 5, (int)Math.Abs(centerPos.X + tooltipsSize - centerPos.X + 30), 10);
        Rectangle moreRight = new((int)(centerPos.X + 19), (int)centerPos.Y - 5, (int)Math.Abs(centerPos.X - tooltipsSize - centerPos.X - 24), 10);

        sb.Draw(GetUI("/Alchemist/HoverReagent_Start_MoreLeft").GetAsset().Value, destinationRectangle: moreLeft, Color.White);
        sb.Draw(GetUI("/Alchemist/HoverReagent_Start_MoreRight").GetAsset().Value, destinationRectangle: moreRight, Color.White);

        sb.Draw(GetUI("/Alchemist/HoverReagent_Start_Center_Empty").GetAsset().Value, position: centerPos, null, Color.White, 0f, GetUI("/Alchemist/HoverReagent_Start_Center_Empty").GetAsset().Value.Size() / 2f, 1, SpriteEffects.None, 1);
        ReagentRarityManager.ColorID.TryGetValue(reagent.Rarity.Power, out string value);
        string textureName = reagent == GetReagent<NoNInItem>() ? GetUI("/Alchemist/HoverReagent_Start_Cristal_Gray") : reagent.Rarity.TexturePatch;
        sb.Draw(textureName.GetAsset().Value, position: new(centerPos.X - 2, centerPos.Y), null, Color.White, 0f, textureName.GetAsset().Size() / 2f, 1, SpriteEffects.None, 1);
        sb.Draw(GetUI("/Alchemist/HoverReagent_Start_LeftEnd").GetAsset().Value, position: new(centerPos.X + tooltipsSize, centerPos.Y), null, Color.White, 0f, GetUI("/Alchemist/HoverReagent_Start_LeftEnd").GetAsset().Value.Size() / 2f, 1, SpriteEffects.None, 1);
        sb.Draw(GetUI("/Alchemist/HoverReagent_Start_RightEnd").GetAsset().Value, position: new(centerPos.X - tooltipsSize + 3, centerPos.Y), null, Color.White, 0f, GetUI("/Alchemist/HoverReagent_Start_RightEnd").GetAsset().Value.Size() / 2f, 1, SpriteEffects.None, 1);
    }
}