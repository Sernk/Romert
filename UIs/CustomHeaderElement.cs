using Terraria.GameContent;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace Romert.UIs;

public class CustomHeaderElement(string header) : HeaderElement(header) {
    public override void DrawSelf(SpriteBatch spriteBatch) {
        base.DrawSelf(spriteBatch);
        CalculatedStyle dimensions = GetDimensions();
        Vector2 position = new Vector2(dimensions.X, dimensions.Y) + new Vector2(8f);
        Vector2 stringSize = FontAssets.MouseText.Value.MeasureString(header);
        spriteBatch.Draw(GetUI(ShortCat[0] + "AlchemistBar_Skull").GetAsset().Value, new(position.X + stringSize.X, position.Y + 10), null, Color.White, 0f, GetUI(ShortCat[0] + "AlchemistBar_Skull").GetAsset().Value.Size() / 2f, 0.75f, SpriteEffects.None, 0f);
        spriteBatch.Draw(GetUI(ShortCat[0] + "AlchemistBar_Skull").GetAsset().Value, new(position.X + stringSize.X - stringSize.X - 8, position.Y + 10), null, Color.White, 0f, GetUI(ShortCat[0] + "AlchemistBar_Skull").GetAsset().Value.Size() / 2f, 0.75f, SpriteEffects.None, 0f);
    }
}