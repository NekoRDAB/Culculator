using Avalonia.Controls;

namespace UserInterface.Views;

class IngredientDescription : Panel
{
    public IngredientDescription(Ingredient ingredient)
    {
        Children.Add(new IngredientAndAmount(ingredient));
    }
}