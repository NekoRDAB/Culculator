using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class BlackBorder : Border
{
    public BlackBorder(double width, double height, int thickness)
    {
        BorderThickness = new Thickness(thickness);
        BorderBrush = Brushes.DarkSlateGray;
        Width = width;
        Height = height;
    }
}