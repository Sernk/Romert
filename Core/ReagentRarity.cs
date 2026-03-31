namespace Romert.Core;

public abstract class ReagentRarity : CleaningType {
    public string Name => GetType().Name;

    public int Time { get; protected set; } = 60;
    public int ID { get; private set; }

    public Color Color { get; protected set; } 
    public Color BorderColor { get; protected set; } = Color.Black;
    public Color[] Colors { get; protected set; }

    public bool IsAnimated { get; protected set; }
    public string Tooltips { get; protected set; }

    public void Register() {
        foreach (AlchemistReagent reagent in AlchemistReagentManager.ReagentsData) {
            reagent.SetRarity(this);
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
    }
    public virtual Color AnimatedColor(Color[] colors, int time = 60) => Animated(colors, time);
    public Color Animated(Color[] colors, int time = 60) {
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

    public virtual void SettingRarity() { }
}
