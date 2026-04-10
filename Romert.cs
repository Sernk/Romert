using Romert.Core;
using Terraria.UI;

namespace Romert;

public class Romert : Mod {
    public static Mod Instruction { get; private set; }
    Romert() { Instruction = this; }

    public const string ModName = "Romert";

    public UserInterface AlchemistTableUI { get; private set; }
    public UserInterface AlchemistBookUI { get; private set; }
    public UserInterface ReagentTooltipsUI { get; private set; }

    public static ModKeybind ToggleAuraModeKeybind { get; private set; }

    public override void Load() {
        AlchemistTableUI = new();
        AlchemistBookUI = new();
        ReagentTooltipsUI = new();
        ToggleAuraModeKeybind = KeybindLoader.RegisterKeybind(this, "OpenAlchemistBook", "J");
        GraphicsSetting.InitGraphics.Init(Assets);
        //On_UIManageControls.CreateElementGroup += (orig, self, parent, bindings, currentInputMode, color) => {
        //    int SnapPointIndex = UIManageControls.SnapPointIndex;
        //    for (int i = 0; i < bindings.Count; i++) {
        //        _ = bindings[i];
        //        UISortableElement uISortableElement = new UISortableElement(i);
        //        uISortableElement.Width.Set(0f, 1f);
        //        uISortableElement.Height.Set(30f, 0f);
        //        uISortableElement.HAlign = 0.5f;
        //        parent.Add(uISortableElement);
        //        if (UIManageControls._BindingsHalfSingleLine.Contains(bindings[i])) {
        //            UIElement uIElement = self.CreatePanel(bindings[i], currentInputMode, color);
        //            uIElement.Width.Set(0f, 0.5f);
        //            uIElement.HAlign = 0.5f;
        //            uIElement.Height.Set(0f, 1f);
        //            uIElement.SetSnapPoint("Wide", SnapPointIndex++);
        //            uISortableElement.Append(uIElement);
        //            continue;
        //        }

        //        if (UIManageControls._BindingsFullLine.Contains(bindings[i])) {
        //            UIElement uIElement2 = self.CreatePanel(bindings[i], currentInputMode, color);
        //            uIElement2.Width.Set(0f, 1f);
        //            uIElement2.Height.Set(0f, 1f);
        //            uIElement2.SetSnapPoint("Wide", SnapPointIndex++);
        //            uISortableElement.Append(uIElement2);
        //            continue;
        //        }

        //        if (UIManageControls._ModNames.Contains(bindings[i])) {
        //            //UIElement uIElement3 = new HeaderElement(bindings[i]);
        //            //uIElement3.Width.Set(0f, 1f);
        //            //uIElement3.Height.Set(0f, 1f);
        //            //uIElement3.SetSnapPoint("Wide", SnapPointIndex++);
        //            //uISortableElement.Append(uIElement3);
        //            continue;
        //        }

        //        UIElement uIElement4 = self.CreatePanel(bindings[i], currentInputMode, color);
        //        uIElement4.Width.Set(-5f, 0.5f);
        //        uIElement4.Height.Set(0f, 1f);
        //        uIElement4.SetSnapPoint("Thin", SnapPointIndex++);
        //        uISortableElement.Append(uIElement4);
        //        i++;
        //        if (i < bindings.Count) {
        //            uIElement4 = self.CreatePanel(bindings[i], currentInputMode, color);
        //            uIElement4.Width.Set(-5f, 0.5f);
        //            uIElement4.Height.Set(0f, 1f);
        //            uIElement4.HAlign = 1f;
        //            uIElement4.SetSnapPoint("Thin", SnapPointIndex++);
        //            uISortableElement.Append(uIElement4);
        //        }
        //    }
        //};
    }

    public override void PostSetupContent() {
        foreach (CleaningType type in GetContent<CleaningType>()) if (type is IPostSetup setup) { setup.PostSetup(this); }
    }
}