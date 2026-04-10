using Romert.Core;
using System.Collections.Generic;

namespace Romert.Common.KeyBindStyles;

public class Book : KeyBindStyle {
    public List<BgReagent> ActiveReagents = [];

    public override bool Active(string keyName) => keyName == Romert.ToggleAuraModeKeybind.DisplayName.Value;
    public override void PreBgEditName(SpriteBatch spriteBatch, Vector2 pos, bool hover, Color color) => spriteBatch.Draw(GetUI(ShortCat[1] + "Settings_Pane_Book_Name").GetAsset().Value, position: new Vector2(pos.X + 1, pos.Y), hover ? Color.White : Color.White.MultiplyRGBA(new Color(180, 180, 180)));
    public override void PostBgEditName(SpriteBatch spriteBatch, Vector2 pos, bool hover, Color color) {
        if (ActiveReagents.Count < 3 && Main.rand.NextBool(20)) {
            int index = Main.rand.Next(AlchemistReagentManager.ReagentsData.Count);
            if (AlchemistReagentManager.ReagentsData[index].HasTexture) {
                BgReagent reagent = new() { TexturePatch = AlchemistReagentManager.ReagentsData[index].TexturePatch, Position = new Vector2(Main.rand.Next(15, 290), Main.rand.Next(10, 18)) };
                ActiveReagents.Add(reagent);
            }
        }
        for (int i = ActiveReagents.Count - 1; i >= 0; i--) {
            ActiveReagents[i].Update(ref ActiveReagents);
            if (ActiveReagents.Count != 0) { ActiveReagents[i].Draw(spriteBatch, pos + ActiveReagents[i].Position); }
        }
    }
    public override void PreBgEditReset(SpriteBatch spriteBatch, Vector2 pos, bool hover, Color color) => spriteBatch.Draw(GetUI(ShortCat[1] + "Settings_Pane_Book_Reset").GetAsset().Value, new Vector2(pos.X + 2, pos.Y), hover ? Color.White : Color.White.MultiplyRGBA(new Color(180, 180, 180)));
    public override void PostBgEditReset(SpriteBatch spriteBatch, Vector2 pos, bool hover, Color color) { }
}