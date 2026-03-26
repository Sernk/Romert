using Romert.Dataset;
using System.Collections.Generic;

namespace Romert.Core;

public class Alchemy : CleaningType {
    public static List<AlchemyManager> Manager { get; private set; }
    AlchemyManager currentRecipe;

    public sealed override void Load(Mod mod) {
        Manager = [];
        currentRecipe = null;
        if (AlchemistReagentManager.PostLoad) {
            foreach (AlchemistReagent reagent in AlchemistReagentManager.ReagentsData) { reagent.Recipe(this); }
        }
    }
    public void Create(AlchemistReagent createType) => currentRecipe = new() { CreateType = createType };
    public void AddIngredient(AlchemistReagent ingredient, int stack) => currentRecipe.Ingredients.Add(new IngredientData(ingredient, stack));
    public void Register() => Manager.Add(currentRecipe);
    public sealed override void PostSetup(Mod mod) { }
    public sealed override void Unload() { }
}