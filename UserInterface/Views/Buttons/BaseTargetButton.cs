using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ReactiveUI;
using System;

namespace UserInterface.Views
{
    public class BaseTargetButton : Panel
    {
        public BaseTargetButton(Action commandHandler, string pathToImage)
        {
            Children.Add(new Button
            {
                Width = 70,
                Height = 40,
                Content = new Image
                {
                    Source = new Bitmap(pathToImage),
                    Width = 30,
                    Height = 30,
                },
                Background = Brushes.Transparent,
                Foreground = Brushes.DarkGray,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Left,
                Command = ReactiveCommand.Create(commandHandler)
            });
        }
    }
}