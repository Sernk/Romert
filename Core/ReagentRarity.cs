namespace Romert.Core;

public abstract class ReagentRarity : CleaningType {
    public string Name => GetType().Name;

    public int ID { get; private set; } // Уникальный идентификатор для каждой редкости, рекомендуется использовать последовательные числа, начиная с 0
    public Color Color { get; protected set; } // Цвет для отображения в книге и на фласке, рекомендуется использовать яркие цвета для лучшей видимости
    public string Tooltips { get; protected set; } // Внутри книги будет отображаться как описание редкости, а при наведении на фласку будет отображаться как подсказка редкости

    public void Register() {
        foreach (AlchemistReagent reagent in AlchemistReagentManager.ReagentsData) {
            reagent.SetRarity(this);
        }
    }

    public sealed override void Load(Mod mod) {
        ReagentRarityManager.RaritiesData.Add(this);
        SettingRarity();
        if (ReagentRarityManager.PostLoad) {
            for (int i = 0; i < ReagentRarityManager.RaritiesData.Count; i++) {
                if (ReagentRarityManager.RaritiesData[i].Name == Name) {
                    ID = i;
                }
            }
            ReagentRarityManager.Rarities.Add(Name, this);
        }
    }

    public virtual void SettingRarity() { }
}
