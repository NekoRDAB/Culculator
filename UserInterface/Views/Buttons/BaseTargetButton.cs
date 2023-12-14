using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;
using System;

namespace UserInterface.Views
{
    public class BaseTargetButton : Panel
    {
        public BaseTargetButton(int? width, int? height, ContentControl? content, IBrush? background, IBrush? foreground, double? fontsize,
            VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Action? commandHandler)
        {
            var button = new Button
            {
                VerticalAlignment = verticalAlignment,
                HorizontalAlignment = horizontalAlignment
            };
            if (width != null && height != null)
            {
                button.Width = (int)width;
                button.Height = (int)height;
            }
            if (commandHandler != null)
                button.Command = ReactiveCommand.Create(commandHandler);
            if (content != null)
                button.Content = content;
            if (background != null && foreground != null)
            {
                button.Background = background;
                button.Foreground = foreground;
            }

            if (fontsize != null)
                button.FontSize = (double)fontsize;
            Children.Add(button);
        }
    }
}