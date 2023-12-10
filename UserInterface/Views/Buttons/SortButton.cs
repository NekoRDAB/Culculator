using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;

namespace UserInterface.Views;

public enum SortType
{
    AscendingByPortionPrice,
    DescendingByPortionPrice,
    AscendingByTotalPrice,
    DescendingByTotalPrice
}

class SortButton : Panel
{
    private readonly Category _currentCategory;
    private MainWindow mainWindow;
    private SortType sortType;
    private Color categoryColor;

    public SortButton(MainWindow mainWindow, Category currentCategory, SortType sortType, Color categoryColor)
    {
        this.mainWindow = mainWindow;
        _currentCategory = currentCategory;
        this.sortType = sortType;
        this.categoryColor = categoryColor;

        var sortBox = new ComboBox()
        {
            Width = 135,
            Height = 40,
            FontSize = 15,
            PlaceholderText = "Сортировка",
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        
        var sortTypesDict = new Dictionary<SortType, string>()
        {
            { SortType.AscendingByTotalPrice, "Цена ↑" },
            { SortType.DescendingByTotalPrice, "Цена ↓" },
            { SortType.AscendingByPortionPrice, "Цена за порцию ↑" },
            { SortType.DescendingByPortionPrice, "Цена за порцию ↓" },
        };
        foreach (var sort in sortTypesDict.Values)
        {
            sortBox.Items.Add(sort);
        }
        
        var applySortButton = new Button
        {
            Width = 135,
            Height = 40,
            Content = "Применить",
            FontSize = 15,
            Background = null,
            Foreground = Brushes.DarkGray,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Left
        };

        var panel = new StackPanel();
        panel.Children.Add(sortBox);
        panel.Children.Add(applySortButton);
        Children.Add(panel);

        applySortButton.Click += (sender, args) =>
        {
            var selectedSortOption = sortTypesDict.FirstOrDefault(x => x.Value == sortBox.SelectedItem?.ToString()).Key;
            mainWindow.Content = new DishesMenu(mainWindow, _currentCategory, selectedSortOption, categoryColor);
        };
    }
}