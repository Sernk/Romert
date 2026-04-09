using Romert.Core;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.GameContent.UI.Chat;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Romert.Common.Hooks.Ons;

public class HookForKeyBindings : ILoadable {
    public List<BgReagent> ActiveReagents = [];

    public void Load(Mod mod) => On_UIKeybindingListItem.DrawSelf += EditNameBg;
    static void PreBg(UIKeybindingListItem self, out CalculatedStyle dimensions, out float num2, out Vector2 position, out bool flag, out Color value, out Color color) {
        dimensions = self.GetDimensions();
        num2 = dimensions.Width + 1f;
        position = new(dimensions.X, dimensions.Y);
        flag = PlayerInput.ListeningTrigger == self._keybind;
        value = (flag ? Color.Gold : (self.IsMouseHovering ? Color.White : Color.Silver));
        value = Color.Lerp(value, Color.White, self.IsMouseHovering ? 0.5f : 0f);
        color = (self.IsMouseHovering ? Color.White : Color.White.MultiplyRGBA(new Color(180, 180, 180)));
    }
    static void PostBg(UIKeybindingListItem self, SpriteBatch spriteBatch, CalculatedStyle dimensions, float num2, Vector2 position, bool flag, Color value, out Vector2 newPosition) {
        position.X += 8f;
        position.Y += 2f + 6f;
        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, self.GetFriendlyName(), position, value, 0f, Vector2.Zero, new(0.8f), num2);
        position.X -= 17f;
        List<string> list = PlayerInput.CurrentProfile.InputModes[self._inputmode].KeyStatus[self._keybind];
        string text = self.GenInput(list);
        if (string.IsNullOrEmpty(text)) {
            text = Lang.menu[195].Value;
            if (!flag) { value = new Color(80, 80, 80); }
        }
        position = new Vector2(dimensions.X + dimensions.Width - ChatManager.GetStringSize(FontAssets.ItemStack.Value, text, new(0.8f)).X - 10f, dimensions.Y + 2f + 6f);
        if (self._inputmode == InputMode.XBoxGamepad || self._inputmode == InputMode.XBoxGamepadUI) { position += new Vector2(0f, -3f); }
        GlyphTagHandler.GlyphsScale = 0.85f;
        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position, value, 0f, Vector2.Zero, new(0.8f), num2);
        GlyphTagHandler.GlyphsScale = 1f;
        newPosition = position;
    }
    void EditNameBg(On_UIKeybindingListItem.orig_DrawSelf orig, UIKeybindingListItem self, SpriteBatch spriteBatch) {
        if (self.GetFriendlyName() != Romert.ToggleAuraModeKeybind.DisplayName.Value) { orig(self, spriteBatch); }
        else {
            PreBg(self, out CalculatedStyle dimensions, out float num2, out Vector2 position, out bool flag, out Color value, out Color color);
            spriteBatch.Draw(GetUI(ShortCat[1] + "Settings_Pane_Book").GetAsset().Value, position: new Vector2(position.X + 1, position.Y), color);
            PostBg(self, spriteBatch, dimensions, num2, position, flag, value, out _);
            if (ActiveReagents.Count < 3 && Main.rand.NextBool(20)) {
                int index = Main.rand.Next(AlchemistReagentManager.ReagentsData.Count);
                if (AlchemistReagentManager.ReagentsData[index].HasTexture) {
                    BgReagent reagent = new() { TexturePatch = AlchemistReagentManager.ReagentsData[index].TexturePatch, Position = new Vector2(Main.rand.Next(15, 290), Main.rand.Next(10, 18)) };
                    ActiveReagents.Add(reagent);
                }
            }
            for (int i = ActiveReagents.Count - 1; i >= 0; i--) {
                ActiveReagents[i].Update(ref ActiveReagents);
                if (ActiveReagents.Count != 0) { ActiveReagents[i].Draw(spriteBatch, position + ActiveReagents[i].Position); }
            }
        }
    }
    public void Unload() => On_UIKeybindingListItem.DrawSelf -= EditNameBg;
}