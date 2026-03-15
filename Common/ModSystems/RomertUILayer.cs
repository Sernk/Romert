using Romert.Helpers;
using System.Collections.Generic;
using Terraria.UI;

namespace Romert.Common.ModSystems;

public class RomertUILayer : ModSystem {
    static readonly Romert mod = GetInstance<Romert>();

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
        int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
        layers.AddLayer(inventoryIndex, Romert.ModName + " Alchemist Table UI. My frist UI in this mod!", () => { mod.AlchemistTableUI.Draw(Main.spriteBatch, new GameTime()); return true; });
    }
    public override void UpdateUI(GameTime gameTime) {
        mod.AlchemistTableUI?.Update(gameTime);
    }
}