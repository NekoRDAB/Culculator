using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Input;
using Avalonia.Media;
using ReactiveUI;

namespace UserInterface.Views;

public partial class MainWindow : Window
{
    static MainWindow _this;
    private bool ascendingOrder;
    private Color categoryColor;

    public MainWindow()
    {
        Environment.CurrentDirectory = Environment.CurrentDirectory.Replace("bin\\Debug\\net6.0", "");
        _this = this;
        InitializeComponent();
        Background = new SolidColorBrush(Colors.Linen);
        Width = 900;
        Height = 600;
        Content = new MainMenu(_this, ascendingOrder, categoryColor);
    }
}