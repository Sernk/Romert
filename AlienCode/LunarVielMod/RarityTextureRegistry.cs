using Romert.Resources;

namespace Romert.AlienCode.LunarVielMod {
    public class RarityTextureRegistry : ModSystem {
        public static Texture2D BaseRarityGlow { get; private set; }
        public static Texture2D BaseRaritySparkleTexture { get; private set; }
        public static Texture2D ThornedRarityGlow { get; private set; }

        public override void Load() {
            BaseRarityGlow = Request<Texture2D>(Path("Sparkles/BaseRarityGlow"), (ReLogic.Content.AssetRequestMode)1).Value;
            BaseRaritySparkleTexture = Request<Texture2D>(Path("Sparkles/BaseRaritySparkleTexture"), (ReLogic.Content.AssetRequestMode)1).Value;
            ThornedRarityGlow = Request<Texture2D>(Path("Sparkles/ThornedRarityGlow"), (ReLogic.Content.AssetRequestMode)1).Value;
        }
        public override void Unload() {
            BaseRarityGlow = null;
            BaseRaritySparkleTexture = null;
            ThornedRarityGlow = null;
        }

        public static string Path(string name) => Textures.GetTextureName("Particles/" + name);
    }
}
