﻿using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class DishDescription : Panel
{
    public DishDescription(MainWindow mainWindow, Category category, Dish dish, Color categoryColor)
    {
        Children.Add(new DishDescriptionPanels(dish, categoryColor));
        Children.Add(new ReturnToDishesMenuButton(mainWindow, category, categoryColor));
    }
}