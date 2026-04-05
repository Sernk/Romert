using Romert.Common.Players;
using Romert.Content.Reagents;
using Romert.Content.Reagents.Flask;
using Romert.Core;
using Terraria.UI;

namespace Romert.UIs;

public class ReagentTooltips(Vector2 pos, Vector2 centerPos, Item item, AlchemistReagent reagent, AlchemistReagent[] reagents) : UIState {
    public override void Update(GameTime gameTime) {
        if (Main.HoverItem.type == 0 || item == null && reagents[0] == null && reagent == GetReagent<NoNInItem>() && !Main.LocalPlayer.Get<AlchemistBookPlayer>().HasBook && !Main.LocalPlayer.Get<AlchemistBookPlayer>().VisibleBookInfo) {
            GetInstance<Romert>().ReagentTooltipsUI.SetState(null);
        }
    }
    protected override void DrawSelf(SpriteBatch spriteBatch) {
        AlchemistBookPlayer player = Main.LocalPlayer.Get<AlchemistBookPlayer>();
        if (reagents[0] != null) {
            bool active = false;
            for (int i = 0; i < reagents.Length; i++) { if (reagents[i].Name != GetReagent<NoN>().Name && reagents[i].Name != GetReagent<Look>().Name) { active = true; } }
            if (active) { ReagentTooltipsFlask.Draw(spriteBatch, pos, centerPos, reagents); }
            if (player.ActiveRecipeUI) { ReagentTooltipsFlask.DrawInUI(spriteBatch, pos, centerPos, player.PreviewReagent); }
        }
        else { ReagentTooltipsReagent.Draw(spriteBatch, pos, centerPos, reagent); }
    }
    public static Texture2D GetAlchemistTexture(string name) => Request<Texture2D>("Romert/Asset/Textures/UI" + ShortCat[0] + name).Value;
}