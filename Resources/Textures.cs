using ReLogic.Content;

namespace Romert.Resources {
    public static partial class Textures {
        public static string[] ShortCat { get; private set; } = ["/Alchemist/", "/KeyBord/"];
        public static string GetTexture(string name) => "Romert/Asset/Textures/" + name;
        public static string GetUI(string name) => "Romert/Asset/Textures/UI" + name;
        public static Asset<Texture2D> GetAsset(this string name) => Request<Texture2D>(name);
    }
}