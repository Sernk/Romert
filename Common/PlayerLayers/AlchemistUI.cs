using Romert.Common.Players;
using Romert.Core;
using Romert.Resources;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace Romert.Common.PlayerLayers {
    public class AlchemistUI : UIPlayerLayer {
        public override bool IsVisible(Player player) => AlchemistPlayer.GetPlayer(player).CurrentAlchemist.IsActive;
        public override void Draw(ref PlayerDrawSet drawInfo, Player player, Vector2 pos) {
            AlchemistData alchemist = AlchemistPlayer.GetPlayer(player).CurrentAlchemist;
            Vector2 shake = AlchemistPlayer.GetPlayer(player).TimeSnake != 0 ? Main.rand.NextVector2Circular(1f, 1f) : Vector2.Zero;
            Vector2 barPos = new(pos.X, pos.Y + 40);
            barPos += shake;
            Texture2D bar = GetUI(ShortCat[0] + "AlchemistBar").GetAsset().Value;
            Texture2D barColor = GetUI(ShortCat[0] + alchemist.BarColorName).GetAsset().Value;
            float progress = (float)alchemist.PointsToDebuffTotal <= 0 ? 1f : (float)alchemist.CurrentProgress / (float)alchemist.PointsToDebuffTotal;
            int barWidth = (int)(barColor.Width * MathHelper.Clamp(progress, 0f, 1f));
            DrawData barData = new(bar, barPos, null, Color.White, 0f, bar.Size() * 0.5f, Main.UIScale, SpriteEffects.None, 0);
            DrawData dataColorBar = new(barColor, barPos, new Rectangle(0, 0, barWidth, barColor.Height), Color.White, 0f, bar.Size() * 0.5f, Main.UIScale, SpriteEffects.None, 0);
            Rectangle rect = new((int)barPos.X - 40, (int)barPos.Y - 13, bar.Width + 7, bar.Height + 10);
            Point mousePos = new(Main.mouseX, Main.mouseY);
            bool hover = rect.Contains(mousePos);
            if (hover) { ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, $"{alchemist.CurrentProgress} / {alchemist.PointsToDebuffTotal}", new Vector2(pos.X - 30, pos.Y + 50), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, new Vector2(0.95f), -1f, 2f); }
            drawInfo.DrawDataCache.Add(barData);
            drawInfo.DrawDataCache.Add(dataColorBar);
        }
    }
}