using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace UserInterface.Views;

class DishImage : Image
{
    public DishImage(Dish dish)
    {
        Margin = new Thickness(10);
        Width = 150;
        Height = 100;
        HorizontalAlignment = HorizontalAlignment.Right;
        if (dish is DishWithImage dishImage)
            try
            {
                Source = new Avalonia.Media.Imaging.Bitmap(dishImage.PathToImage)
                    .CreateScaledBitmap(new PixelSize((int)Width, (int)Height));
            }
            catch
            {
                Console.WriteLine($"Изображение {dishImage.PathToImage} не найдено");
            }
    }
}