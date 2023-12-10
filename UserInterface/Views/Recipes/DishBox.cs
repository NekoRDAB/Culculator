﻿using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class DishBox : Panel
{
    public DishBox(MainWindow mainWindow, Category category, Dish dish, SortType sortType, Color categoryColor)
    {
        Width = 620;
        Height = 120;
        Children.Add(
            new DishDescriptionButton(mainWindow, category, dish, Width, Height, sortType, categoryColor));
        Children.Add(new DishName(dish.Name));
        Children.Add(new RightPart(dish));
    }
}