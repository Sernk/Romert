using System.Collections.Generic;

namespace Romert.Core;

public class AlchemyManager {
    public AlchemistReagent CreateType;
    public List<IngredientData> Ingredients = [];
}
public struct IngredientData(AlchemistReagent ingredient, int stack) {
    public AlchemistReagent Ingredient = ingredient;
    public int Stack = stack;
}