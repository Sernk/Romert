using ReLogic.Content;
using Romert.Common.Players;
using System.Collections.Generic;
using Terraria.GameContent;

namespace Romert.Common.ResourceOverlays {
    public class AlchemistHeart : ModResourceOverlay {
        private readonly Dictionary<string, Asset<Texture2D>> vanillaAssetCache = [];
        public string baseFolder = "Romert/Asset/Textures/UI" + ShortCat[0] + "Heart/";

        public enum ResourceType { Life, Mana }
        private string GetTexturePath(ResourceType type, bool isMini) {
            AlchemistPlayer alchemist = AlchemistPlayer.GetPlayer(Main.LocalPlayer);
            string folder = baseFolder;

            if (type == ResourceType.Life) folder += isMini ? "MiniHP" : "HP";
            else folder += isMini ? "MiniMP" : "MP";

            if (type == ResourceType.Life) {
                if (alchemist.CurrentAlchemist.CurrentProgress <= 25) { return folder + "HeartDermo"; }
            }
            else {  }
            return string.Empty;
        }
        public string LifeTexturePath() => GetTexturePath(ResourceType.Life, false);
        public string ManaTexturePath() => GetTexturePath(ResourceType.Mana, false);
        public string MiniLifeTexturePath() => GetTexturePath(ResourceType.Life, true);
        public string MiniManaTexturePath() => GetTexturePath(ResourceType.Mana, true);
        public override void PostDrawResource(ResourceOverlayDrawContext context) {
            Asset<Texture2D> asset = context.texture;
            string fancyFolder = "Images/UI/PlayerResourceSets/FancyClassic/";
            string barsFolder = "Images/UI/PlayerResourceSets/HorizontalBars/";

            if (LifeTexturePath() == string.Empty) return;

            if (asset == TextureAssets.Heart || asset == TextureAssets.Heart2 || CompareAssets(asset, fancyFolder + "Heart_Fill") || CompareAssets(asset, fancyFolder + "Heart_Fill_B")) {
                context.texture = ModContent.Request<Texture2D>(LifeTexturePath() + "Heart");
                context.Draw();
            }
            else if (CompareAssets(asset, barsFolder + "HP_Fill") || CompareAssets(asset, barsFolder + "HP_Fill_Honey")) {
                context.texture = ModContent.Request<Texture2D>(LifeTexturePath() + "Bar");
                context.Draw();
            }
            if (ManaTexturePath() != string.Empty) {
                if (asset == TextureAssets.Mana || CompareAssets(asset, fancyFolder + "Star_Fill")) {
                    context.texture = ModContent.Request<Texture2D>(ManaTexturePath() + "Star");
                    context.Draw();
                }
                else if (CompareAssets(asset, barsFolder + "MP_Fill")) {
                    context.texture = ModContent.Request<Texture2D>(ManaTexturePath() + "Bar");
                    context.Draw();
                }
            }
            if (CompareAssets(asset, barsFolder + "HP_Panel_Right")) {
                Texture2D tex = ModContent.Request<Texture2D>(MiniLifeTexturePath() + "HeartMini").Value;
                Vector2 pos = context.position;
                pos += new Vector2(20.25f, 11.5f);
                Main.spriteBatch.Draw(tex, pos, Color.White);
            }
            if (CompareAssets(asset, barsFolder + "MP_Panel_Right")) {
                Texture2D tex = ModContent.Request<Texture2D>(MiniManaTexturePath() + "ManaMini").Value;
                Vector2 pos = context.position;
                pos += new Vector2(20.30f, 4f);
                Main.spriteBatch.Draw(tex, pos, Color.White);
            }
        }
        private bool CompareAssets(Asset<Texture2D> currentAsset, string compareAssetPath) {
            if (!vanillaAssetCache.TryGetValue(compareAssetPath, out var asset)) asset = vanillaAssetCache[compareAssetPath] = Main.Assets.Request<Texture2D>(compareAssetPath);
            return currentAsset == asset;
        }
    }
}
