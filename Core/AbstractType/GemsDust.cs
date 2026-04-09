using Romert.Common.GlobalItems;
using Terraria.DataStructures;

namespace Romert.Core.AbstractType;

public abstract class GemsDust : ModItem {
    public abstract int Power { get; }
    public virtual int Rare => ItemRarityID.White;

    public override void SetDefaults() {
        Item.height = 26;
        Item.width = 26;
        Item.rare = Rare;
        Item.Get<CatalystItems>().power = Power;
    }
    public override void SetStaticDefaults() {
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 4));
        ItemID.Sets.AnimatesAsSoul[Type] = true;
    }
    public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
        //Texture2D tex = TextureAssets.Item[Type].Value;
        //Effect effect = InitGraphics.Rainbow.Value;

        //effect.Parameters["uTextureSize"].SetValue(tex.Size());
        //effect.Parameters["Time"].SetValue(Main.GlobalTimeWrappedHourly * 2f);

        //spriteBatch.End();
        //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, effect, Main.UIScaleMatrix);
        //effect.CurrentTechnique.Passes[0].Apply();
        //spriteBatch.Draw(tex, position, frame, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
        //spriteBatch.End();
        //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);

        return true;
    }
}