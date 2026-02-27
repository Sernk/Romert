using ReLogic.Content;

namespace Romert.Resources {
    public static partial class Textures {
        public static string GetTextureName(string name) => "Romert/Asset/Textures/" + name;
        public static Asset<Texture2D> GetAsset(this string name) => Request<Texture2D>(name);
    }
}