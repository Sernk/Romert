using Romert.Common.Players;
using Romert.Core;
using Romert.Resources;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace Romert.Common.PlayerLayers;
    public class AlchemistUI : UIPlayerLayer {
    public override bool IsVisible(Player player) => player.Get<AlchemistPlayer>().CurrentAlchemist.IsActive;
    public override void Draw(ref PlayerDrawSet drawInfo, Player player, Vector2 pos) {
        AlchemistPlayer alchemistPlayer = player.Get<AlchemistPlayer>();
        AlchemistData alchemist = alchemistPlayer.CurrentAlchemist;

        Vector2 shake = alchemistPlayer.TimeSnake != 0 ? Main.rand.NextVector2Circular(1f, 1f) : Vector2.Zero;
        Vector2 barPos = new(pos.X, pos.Y + 40);
        barPos += shake;

        Texture2D barEmpty = GetUI(ShortCat[0] + "AlchemistBarFull").GetAsset().Value;
        Texture2D barColor = alchemist.ModName == "Romert" ? GetUI(ShortCat[0] + alchemist.BarColorName).GetAsset().Value : alchemist.BarColorName.GetAsset().Value;

        float progress = (float)alchemist.PointsToDebuffTotal <= 0 ? 1f : (float)alchemist.CurrentProgress / (float)alchemist.PointsToDebuffTotal;
        int barWidth = (int)(barColor.Width * progress);

        Rectangle rect = new((int)barPos.X - 65, (int)barPos.Y - 13, barEmpty.Width + 7, barEmpty.Height + 10);
        //Draw Bar
        DrawData bar = new(barEmpty, barPos, null, Color.White, 0f, barEmpty.Size() * 0.5f, Main.UIScale, SpriteEffects.None, 0);
        DrawData colorBar = new(barColor, barPos, new Rectangle(0, 0, barWidth, barColor.Height), Color.White, 0f, barEmpty.Size() * 0.5f, Main.UIScale, SpriteEffects.None, 0);
        //Draw Text
        if (rect.Contains(new Point(Main.mouseX, Main.mouseY))) { ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, $"{alchemist.CurrentProgress} / {alchemist.PointsToDebuffTotal}", new Vector2(pos.X - 30, pos.Y + 50), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, new Vector2(0.95f), -1f, 2f); }
        //Register
        drawInfo.DrawDataCache.Add(bar);
        drawInfo.DrawDataCache.Add(colorBar);
    }
}