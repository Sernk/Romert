using ReLogic.Content;

namespace Romert.GraphicsSetting;

public class InitGraphics {
    //public static string Rainbow;
    public static Asset<Effect> Rainbow { get; private set; }
    public static Asset<Effect> Droj { get; private set; }

    public static string FilePath(string name) => $"Asset/Effects/{name}";

    public static void Init(AssetRepository assets) {
        if (!Main.dedServ) {
            Rainbow = assets.Request<Effect>(FilePath("Rainbow"), AssetRequestMode.ImmediateLoad);
            Droj = assets.Request<Effect>(FilePath("Droj"), AssetRequestMode.ImmediateLoad);
        }
    }
}