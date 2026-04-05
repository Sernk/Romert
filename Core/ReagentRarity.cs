using Romert.Core.Exceptions;

namespace Romert.Core;

public abstract class ReagentRarity : CleaningType {
    public string Name => GetType().Name;
    public string VanillaPatch => "Romert/Asset/Textures/UI/Alchemist/";

    public int ID { get; private set; }
    public int Power { get; protected set; } = 0;

    public Color Color { get; protected set; } 
    public Color BorderColor { get; protected set; } = Color.Black;

    public bool IsAnimated { get; protected set; }

    public string Tooltips { get; protected set; }

    public void Register() {
        foreach (AlchemistReagent reagent in AlchemistReagentManager.ReagentsData) {
           // reagent.SetRarity(this);
        }
    }
    public void AddID() {
        if (ReagentRarityManager.PostLoad) {
            for (int i = 0; i < ReagentRarityManager.RaritiesData.Count; i++) {
                if (ReagentRarityManager.RaritiesData[i].Name == Name) {
                    ID = i;
                }
            }
        }
    }

    public sealed override void Load(Mod mod) {
        ReagentRarityManager.RaritiesData.Add(this);
        ReagentRarityManager.Rarities.Add(Name, this);
        SettingRarity();
        if (Main.netMode != NetmodeID.Server) {
            if (HasAsset(TexturePatch)) { ReagentRarityManager.ColorID.Add(Power, TexturePatch); }
            else { throw new NoTexture(Name); }
        }
    }
    public virtual string TexturePatch => (GetType().Namespace + "." + Name).Replace('.', '/');
    public virtual Color AnimatedColor() => Animated(null, 0);
    public virtual void SettingRarity() { }

    public Color Animated(Color[] colors, int time) {
        int transitionTime = time;
        int colorCount = colors.Length;
        int totalTime = transitionTime * colorCount;

        int timer = (int)(Main.GameUpdateCount % totalTime);
        int index = timer / transitionTime;
        float t = timer % transitionTime / (float)transitionTime;

        Color from = colors[index % colorCount];
        Color to = colors[(index + 1) % colorCount];

        return Color.Lerp(from, to, t);
    }

}