using Romert.Common.Players;
using Romert.Core.Exceptions;
using Romert.Resources;
using System.Collections.Generic;
using Romert.Content.Reagents;
using Romert.Core;

namespace Romert.Common.GlobalItems;

public class AlchemicalItems : GlobalItem {
    public HashSet<int> IsAlchemistPoisoningItems = [];

    public AlchemistReagent Reagent;

    public bool isFlask;
    public bool isAlchemistMaterials;

    float glowAlpha = 0f;

    public int[] PotionSlot { get; private set; } = new int[6]; // -1 is close, 0 is empty, 1 is slot has value. -2..., 2... is Exception 

    public override bool InstancePerEntity => true;
    public override void SetDefaults(Item entity) {
        if (isAlchemistMaterials) { entity.material = true; }
        if (entity.type == ItemID.Gel) {
            Slime slime = AlchemistReagent.Get<Slime>();
            Reagent = slime;
        }
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
        foreach (TooltipLine line in tooltips) {
            if (isAlchemistMaterials) {
                if (line.Name == "Material") { line.Text = Loc(LocCategory[0] + "." + LocCategory[1], "Material"); }
            }
        }
        //if (Reagent != null) { Main.NewText(Reagent.Name); }
    }
    public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
        bool active = Main.LocalPlayer.Get<AlchemistTilePlayer>().ActiveAlchemistUI;
        if (isFlask) {
            float target = active ? 1f : 0f;
            glowAlpha = MathHelper.Lerp(glowAlpha, target, 0.1f);
            spriteBatch.Draw(GetTexture("CoreGlow").GetAsset().Value, new(position.X - 5, position.Y - 5), null, Color.AliceBlue * glowAlpha, 0f, GetTexture("Glow").GetAsset().Value.Size() / 2f, 0.15f, SpriteEffects.None, 0f);
        }
        return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    }
    public override void HoldItem(Item item, Player player) {
        AlchemistPlayer alchemist = player.Get<AlchemistPlayer>();
        if (IsAlchemistPoisoningItems.Contains(item.type)) { alchemist.AlchemistDatas[0].IsActive = true; }
    }
    public override bool CanUseItem(Item item, Player player) {
        bool orig = base.CanUseItem(item, player);
        AlchemistPlayer alchemist = player.Get<AlchemistPlayer>();
        if (IsAlchemistPoisoningItems.Contains(item.type)) { alchemist.AddPoints(0, orig); }
        return orig;
    }
    public override void UpdateAccessory(Item item, Player player, bool hideVisual) {
        //if(item.type == 5000) {
        //    //AlchemistPlayer player1 = player.GetModPlayer<AlchemistPlayer>();
        //    //player1.CurrentAlchemist.ModifyPointsEarned += 9;
        //    //player1.BonusPointsEarned += 20;
        //    ////player1.CurrentAlchemist.ResetPoints();
        //    //Main.NewText(player1.CurrentAlchemist.CurrentProgress);
        //}
    }
    public void AddSlot(int index0 = -1, int index1 = -1, int index2 = -1, int index3 = -1, int index4 = -1, int index5 = -1) {
        PotionSlot = [index0, index1, index2, index3, index4, index5];
        for (int i = 0; i < PotionSlot.Length; i++) {
            if (PotionSlot[i] != 0 && PotionSlot[i] != -1 && PotionSlot[i] != 1) { throw new UnknownNumber(PotionSlot[i], i); }
        }
    }
}