namespace Romert.AlienCode.LunarVielMod {
    public class DefaultSparkle : RaritySparkle {
        public DefaultSparkle(int lifetime, float scale, float initialRotation, float rotationSpeed, Vector2 position, Vector2 velocity) {
            Lifetime = lifetime;
            Scale = 0f;
            MaxScale = scale;
            Rotation = initialRotation;
            RotationSpeed = rotationSpeed;
            Position = position;
            Velocity = velocity;
            DrawColor = Color.Lerp(Color.LightBlue, Color.LightCyan, Main.rand.NextFloat(1f));
            Texture = Request<Texture2D>(RarityTextureRegistry.Path("Sparkles/BaseRaritySparkleTexture"), (ReLogic.Content.AssetRequestMode)2).Value;
            BaseFrame = null;
        }
    }
}
