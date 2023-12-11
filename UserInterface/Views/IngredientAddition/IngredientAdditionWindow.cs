using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using DynamicData;

namespace UserInterface.Views.IngredientAddition;

public class IngredientAdditionWindow : Panel
{
    public IngredientAdditionWindow(MainWindow parent, Color categoryColor)
    {
        Children.Add(new TextBlock()
        {
            Text = "Culculator", FontSize = 80,
            TextAlignment = TextAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top
        });
        Children.Add(ContainerConfigurer.GetIngredientInput(parent));
        Children.Add(new ReturnButton(parent, categoryColor));
    }
}