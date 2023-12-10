using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;

namespace UserInterface.Views;


public class SortButton : Panel
{
    private readonly Category _currentCategory;
    private MainWindow mainWindow;
    private Color categoryColor;

    public SortButton(MainWindow mainWindow, ISortMethod[] sortMethods, Category currentCategory, Color categoryColor)
    {
        this.mainWindow = mainWindow;
        _currentCategory = currentCategory;
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

        var sortTypesDict = new Dictionary<string, Func<Category, Category>>();
        foreach (var sortMethod in sortMethods)
        {
            sortTypesDict[sortMethod.AscendingSortDescription] = sortMethod.SortAscending;
            sortTypesDict[sortMethod.DescendingSortDescription] = sortMethod.SortDescending;
        }
        foreach (var sort in sortTypesDict.Keys)
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
            if (sortBox.SelectedItem != null)
            {
                var sortedCategory = sortTypesDict[(string)sortBox.SelectedItem](currentCategory);
                mainWindow.Content = new DishesMenu(mainWindow, sortedCategory,categoryColor);
            }
        };
    }
}