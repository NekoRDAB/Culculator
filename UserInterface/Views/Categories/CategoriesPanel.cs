using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class CategoriesPanel : StackPanel
{
    static string pathToRecipes = Environment.CurrentDirectory.Replace("UserInterface", "Culculator\\RecipesDataBase.db");
    static string pathToIngredients = Environment.CurrentDirectory.Replace("UserInterface", "Culculator\\IngredientsDataBase.db");
    static AutoCategories categories = new (pathToRecipes, pathToIngredients);
    
    public CategoriesPanel(MainWindow mainWindow, bool ascendingOrder, Color categoryColor)
    {
        Margin = new(20);
        Spacing = 10;
        var dir = Environment.CurrentDirectory;
        var pathToRecipes = dir.Replace("UserInterface", "Culculator\\RecipesDataBase.db");
        var pathToIngredients = dir.Replace("UserInterface", "Culculator\\IngredientsDataBase.db");
        //var categories = new AutoCategories(pathToRecipes, pathToIngredients);
        foreach (var category in categories.All)
            Children.Add(new CategoryButton(mainWindow, category, ascendingOrder, categoryColor));
    }
}