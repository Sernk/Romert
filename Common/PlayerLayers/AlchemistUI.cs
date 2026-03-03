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
            Vector2 skullPos = new(barPos.X + 32, barPos.Y);
            barPos += shake;
            skullPos += shake;
            string skullName = alchemist.BarColorName + "_Skull";

            Texture2D barEmpty   = GetUI(ShortCat[0] + "AlchemistBar").GetAsset().Value;
            Texture2D skullEmpty = GetUI(ShortCat[0] + "AlchemistBar_Skull").GetAsset().Value;
            Texture2D barColor   = alchemist.ModName == "Romert" ? GetUI(ShortCat[0] + alchemist.BarColorName).GetAsset().Value : alchemist.BarColorName.GetAsset().Value;
            Texture2D skullColor = alchemist.ModName == "Romert" ? GetUI(ShortCat[0] + alchemist.BarColorName + "_Skull").GetAsset().Value : skullName.GetAsset().Value;

            float progress = (float)alchemist.PointsToDebuffTotal <= 0 ? 1f : (float)alchemist.CurrentProgress / (float)alchemist.PointsToDebuffTotal;
            int barWidth = (int)(barColor.Width * progress);
            float skullStart = 0.7f;
            float skullProgress = 0f;
            if (progress > skullStart) { skullProgress = (progress - skullStart) / (1f - skullStart); }
            skullProgress = MathHelper.Clamp(skullProgress, 0f, 1f);
            int skullHeight = (int)(skullColor.Height * skullProgress);
            Rectangle rect = new((int)barPos.X - 40, (int)barPos.Y - 13, barEmpty.Width + 7, barEmpty.Height + 10);
            //Draw Bar
            DrawData bar = new(barEmpty, barPos, null, Color.White, 0f, barEmpty.Size() * 0.5f, Main.UIScale, SpriteEffects.None, 0);
            DrawData colorBar = new(barColor, barPos, new Rectangle(0, 0, barWidth, barColor.Height), Color.White, 0f, barEmpty.Size() * 0.5f, Main.UIScale, SpriteEffects.None, 0);
            //Draw Skull
            DrawData skull = new(skullEmpty, skullPos, null, Color.White, 0f, skullEmpty.Size() * 0.5f, Main.UIScale, SpriteEffects.None, 0);
            DrawData colorSkull = new(skullColor, skullPos, new(0, 0, skullColor.Width, skullHeight), Color.White, 0f, skullEmpty.Size() * 0.5f, Main.UIScale, SpriteEffects.None, 0);
            //Draw Text
            if (rect.Contains(new Point(Main.mouseX, Main.mouseY))) { ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, $"{alchemist.CurrentProgress} / {alchemist.PointsToDebuffTotal}", new Vector2(pos.X - 30, pos.Y + 50), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, new Vector2(0.95f), -1f, 2f); }
            //Register
            drawInfo.DrawDataCache.Add(bar);
            drawInfo.DrawDataCache.Add(colorBar);
            drawInfo.DrawDataCache.Add(skull);
            drawInfo.DrawDataCache.Add(colorSkull);
        }
    }
}