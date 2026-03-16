using Romert.Common.Players;
using Romert.Resources;
using System.Collections.Generic;

namespace Romert.Common.GlobalItems;

public class AlchemicalItems : GlobalItem {
    public HashSet<int> IsAlchemistPoisoningItems = [];

    public bool isFlask;
    public bool isAlchemistMaterials;

    float glowAlpha = 0f;

    public override bool InstancePerEntity => true;
    public override void SetDefaults(Item entity) {
        if (isAlchemistMaterials) { entity.material = true; }
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
            spriteBatch.Draw(GetTexture("CoreGlow").GetAsset().Value, new(position.X - 5, position.Y - 5), null, Color.AliceBlue * glowAlpha, 0f, GetTexture("Glow").GetAsset().Value.Size() / 2f, 0.15f, SpriteEffects.None, 0f);
        }
        return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    }
    public override void HoldItem(Item item, Player player) {
        AlchemistPlayer alchemist = player.Get<AlchemistPlayer>();
        if (IsAlchemistPoisoningItems.Contains(item.type)) { alchemist.AlchemistDatas[0].IsActive = true; }
    }
    public override bool CanUseItem(Item item, Player player) {
        bool orgin = base.CanUseItem(item, player);
        AlchemistPlayer alchemist = player.Get<AlchemistPlayer>();
        if (IsAlchemistPoisoningItems.Contains(item.type)) { alchemist.AddPoints(0, orgin); }
        return orgin;
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
}