using System.Collections.Generic;
using Terraria.UI;

namespace Romert.Helpers;

public static partial class UIHelper {
    public static void AddLayer(this List<GameInterfaceLayer> layers, int layerIndex, string name, GameInterfaceDrawMethod drawMethod, InterfaceScaleType scaleType = InterfaceScaleType.UI) {
        if (layerIndex != -1) {
            layers.Insert(layerIndex, new LegacyGameInterfaceLayer(name, drawMethod, scaleType));
        }
    }
}