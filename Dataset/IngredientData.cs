using Romert.Core;

namespace Romert.Dataset;

public struct IngredientData(AlchemistReagent ingredient, int stack) {
    public AlchemistReagent Ingredient = ingredient;
    public int Stack = stack;
}