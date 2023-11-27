using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class RoundBorder : Border
{
    public RoundBorder(int thickness)
    {
        BorderThickness = new Thickness(thickness);
        BorderBrush = Brushes.DarkSlateGray;
        CornerRadius = new CornerRadius(10);
    }
}