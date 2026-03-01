using ReLogic.Content;

namespace Romert.RomertUtil {
    public static class RegisterTexture {
        public static void RegisterTextureElement(Asset<Texture2D>[] assets, byte countElement, string nameElement) {
            for (int i = 0; i < countElement; i++) {
                assets[i] = Request<Texture2D>(Resources.Textures.GetTexture(nameElement + i.ToString()));
            }
        }
    }
}
