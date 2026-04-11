using Romert.Common.KeyBindStyles;
using Romert.UIs;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.GameContent.UI.Chat;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Romert.Common.Hooks.Ons;

public class HookForKeyBindings : ILoadable {
    public void Load(Mod mod) {
        On_UIManageControls.CreateElementGroup += EditModName;
        On_UIKeybindingListItem.DrawSelf += EditNameBg;
        On_UIKeybindingSimpleListItem.DrawSelf += EditResetBg;
    }

    #region Vanilla code
    static void PreBg(UIKeybindingListItem self, out CalculatedStyle dimensions, out float num2, out Vector2 position, out bool flag, out Color value, out Color color) {
        dimensions = self.GetDimensions();
        num2 = dimensions.Width + 1f;
        position = new(dimensions.X, dimensions.Y);
        flag = PlayerInput.ListeningTrigger == self._keybind;
        value = flag ? Color.Gold : (self.IsMouseHovering ? Color.White : Color.Silver);
        value = Color.Lerp(value, Color.White, self.IsMouseHovering ? 0.5f : 0f);
        color = self.IsMouseHovering ? self._color : self._color.MultiplyRGBA(new Color(180, 180, 180));
    }
    static void PreBG(UIKeybindingSimpleListItem self, out CalculatedStyle dimensions, out float num2, out Vector2 position, out Vector2 baseScale, out Color value, out Color color) {
        dimensions = self.GetDimensions();
        num2 = dimensions.Width + 1f;
        position = new(dimensions.X, dimensions.Y);
        baseScale = new(0.8f);
        value = (self.IsMouseHovering ? Color.White : Color.Silver);
        value = Color.Lerp(value, Color.White, self.IsMouseHovering ? 0.5f : 0f);
        color = (self.IsMouseHovering ? self._color : self._color.MultiplyRGBA(new Color(180, 180, 180)));
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
    static void PostBg(UIKeybindingSimpleListItem self, Vector2 position, CalculatedStyle dimensions, Vector2 baseScale, SpriteBatch spriteBatch, Color value, float num2) {
        position.X += 8f;
        position.Y += 2f + 6f;
        string text = self._GetTextFunction();
        Vector2 stringSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, text, baseScale);
        if (stringSize.X > dimensions.Width) {
            stringSize.X *= dimensions.Width / stringSize.X;
            baseScale.X *= dimensions.Width / stringSize.X;
        }
        position.X = dimensions.X + dimensions.Width / 2f - stringSize.X / 2f;
        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position, value, 0f, Vector2.Zero, baseScale, num2);
    }
    #endregion

    void EditModName(On_UIManageControls.orig_CreateElementGroup orig, UIManageControls self, UIList parent, List<string> bindings, InputMode currentInputMode, Color color) {    
        int SnapPointIndex = UIManageControls.SnapPointIndex;
        for (int i = 0; i < bindings.Count; i++) {
            #region Vanill code
            UISortableElement uISortableElement = new(i);
            uISortableElement.Width.Set(0f, 1f);
            uISortableElement.Height.Set(30f, 0f);
            uISortableElement.HAlign = 0.5f;
            parent.Add(uISortableElement);
            if (UIManageControls._BindingsHalfSingleLine.Contains(bindings[i])) {
                UIElement uIElement = self.CreatePanel(bindings[i], currentInputMode, color);
                uIElement.Width.Set(0f, 0.5f);
                uIElement.HAlign = 0.5f;
                uIElement.Height.Set(0f, 1f);
                uIElement.SetSnapPoint("Wide", SnapPointIndex++);
                uISortableElement.Append(uIElement);
                continue;
            }
            if (UIManageControls._BindingsFullLine.Contains(bindings[i])) {
                UIElement uIElement2 = self.CreatePanel(bindings[i], currentInputMode, color);
                uIElement2.Width.Set(0f, 1f);
                uIElement2.Height.Set(0f, 1f);
                uIElement2.SetSnapPoint("Wide", SnapPointIndex++);
                uISortableElement.Append(uIElement2);
                continue;
            }
            #endregion
            if (UIManageControls._ModNames.Contains(bindings[i])) {
                UIElement uIElement3;

                if (Romert.Instruction.DisplayName.Contains(bindings[i])) { uIElement3 = new CustomHeaderElement(bindings[i]); }
                else { uIElement3 = new HeaderElement(bindings[i]); }

                uIElement3.Width.Set(0f, 1f);
                uIElement3.Height.Set(0f, 1f);
                uIElement3.SetSnapPoint("Wide", SnapPointIndex++);
                uISortableElement.Append(uIElement3);
                continue;
            }
            #region Vanill code
            UIElement uIElement4 = self.CreatePanel(bindings[i], currentInputMode, color);
            uIElement4.Width.Set(-5f, 0.5f);
            uIElement4.Height.Set(0f, 1f);
            uIElement4.SetSnapPoint("Thin", SnapPointIndex++);
            uISortableElement.Append(uIElement4);
            i++;
            if (i < bindings.Count) {
                uIElement4 = self.CreatePanel(bindings[i], currentInputMode, color);
                uIElement4.Width.Set(-5f, 0.5f);
                uIElement4.Height.Set(0f, 1f);
                uIElement4.HAlign = 1f;
                uIElement4.SetSnapPoint("Thin", SnapPointIndex++);
                uISortableElement.Append(uIElement4);
            }
            #endregion
        }
    }
    void EditNameBg(On_UIKeybindingListItem.orig_DrawSelf orig, UIKeybindingListItem self, SpriteBatch spriteBatch) {
        if (KeyBindStyle.Styles.Count != 0) {
            for (int i = 0; i < KeyBindStyle.Styles.Count; i++) {
                if (KeyBindStyle.Styles[i].Active(self.GetFriendlyName())) {
                    PreBg(self, out CalculatedStyle dimensions, out float num2, out Vector2 position, out bool flag, out Color value, out Color color);
                    KeyBindStyle.Styles[i].PreBgEditName(spriteBatch, position, self.IsMouseHovering, color);
                    KeyBindStyle.Styles[i].NamePos = position;
                    PostBg(self, spriteBatch, dimensions, num2, position, flag, value, out _);
                    KeyBindStyle.Styles[i].PostBgEditName(spriteBatch, position, self.IsMouseHovering, color);
                } 
                else { orig(self, spriteBatch); }
            }
        } 
        else { orig(self, spriteBatch); }
    }
    void EditResetBg(On_UIKeybindingSimpleListItem.orig_DrawSelf orig, UIKeybindingSimpleListItem self, SpriteBatch spriteBatch) {
        CalculatedStyle dimensions = self.GetDimensions();
        Vector2 position = new(dimensions.X, dimensions.Y);
        position.X += 8f;
        position.Y += 8f;
        if (KeyBindStyle.Styles.Count != 0) {
            for (int i = 0; i < KeyBindStyle.Styles.Count; i++) {
                if (position.Y == KeyBindStyle.Styles[i].NamePos.Y + 8) {
                    PreBG(self, out dimensions, out float num2, out position, out Vector2 baseScale, out Color value, out Color color);
                    KeyBindStyle.Styles[i].PreBgEditReset(spriteBatch, position, self.IsMouseHovering, color);
                    PostBg(self, position, dimensions, baseScale, spriteBatch, value, num2);
                    KeyBindStyle.Styles[i].PostBgEditReset(spriteBatch, position, self.IsMouseHovering, color);
                } 
                else { orig(self, spriteBatch); }
            }
        } 
        else { orig(self, spriteBatch); }
    }

    public void Unload() {
        On_UIManageControls.CreateElementGroup -= EditModName;
        On_UIKeybindingListItem.DrawSelf -= EditNameBg;
        On_UIKeybindingSimpleListItem.DrawSelf -= EditResetBg;
    }
}