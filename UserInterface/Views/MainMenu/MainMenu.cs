using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using DynamicData;
using ReactiveUI;
using UserInterface.Views.IngredientAddition;

namespace UserInterface.Views;

public class MainMenu : StackPanel
{
    public MainMenu(MainWindow mainWindow, Color categoryColor)
    {
        Margin = new Thickness(20);
        Spacing = 7;
        Children.Add(new Title());
        Children.Add(new Button
        {
            Content = "Добавить свой ингредиент",
            Command = ReactiveCommand.Create(
                () => { mainWindow.Content = new IngredientAdditionWindow(mainWindow, categoryColor); })
        });
        Children.Add(ContainerConfigurer.GetCategoriesPanel(mainWindow, categoryColor));
    }
}