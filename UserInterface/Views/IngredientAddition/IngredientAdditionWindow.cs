using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;

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
        var returnImageContent = new ContentControl()
        {
            Content = new Image
            {
                Source = new Bitmap("Images/ReturnButton.png"),
                Width = 30,
                Height = 30,
            }
        };
        var returnToMainMenuButton = new BaseTargetButton(90, 50, returnImageContent,  Brushes.Transparent, Brushes.Gray, null,VerticalAlignment.Bottom, HorizontalAlignment.Left,
            () => { parent.Content = new MainMenu(parent, categoryColor); });
        Children.Add(returnToMainMenuButton);
    }
}