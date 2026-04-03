using System.Collections.Generic;
using Terraria.UI;

namespace Romert.Common.ModSystems;

public class RomertUILayer : ModSystem {
    static readonly Romert mod = GetInstance<Romert>();

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
        int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
        int cursorIndex = layers.FindIndex(layers => layers.Name.Equals("Vanilla: Cursor"));
        layers.AddLayer(inventoryIndex, Romert.ModName + " Alchemist Table UI", () => { mod.AlchemistTableUI.Draw(Main.spriteBatch, new GameTime()); return true; });
        layers.AddLayer(inventoryIndex + 1, Romert.ModName + " Alchemist Book UI", () => { mod.AlchemistBookUI.Draw(Main.spriteBatch, new GameTime()); return true; });
        layers.AddLayer(cursorIndex, Romert.ModName + " Reagent Tooltips UI", () => { mod.ReagentTooltipsUI.Draw(Main.spriteBatch, new GameTime()); return true; });
    }
    public override void UpdateUI(GameTime gameTime) {
        mod.AlchemistTableUI?.Update(gameTime);
        mod.AlchemistBookUI?.Update(gameTime);
        mod.ReagentTooltipsUI?.Update(gameTime);
    }
}