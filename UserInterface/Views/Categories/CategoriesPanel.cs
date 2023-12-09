using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

public class CategoriesPanel : StackPanel
{
    static ICategoriesFactory _categories;
    
    public CategoriesPanel(MainWindow mainWindow, ICategoriesFactory categories, bool ascendingOrder, Color categoryColor)
    {
        _categories = categories;
        Margin = new(20);
        Spacing = 10;
        var dir = Environment.CurrentDirectory;
        var pathToRecipes = dir.Replace("UserInterface", "Culculator\\RecipesDataBase.db");
        var pathToIngredients = dir.Replace("UserInterface", "Culculator\\IngredientsDataBase.db");
        var pathToAddedRecipes = dir.Replace("UserInterface", "Culculator\\AddedRecipesDataBase.db");
        //var categories = new AutoCategories(pathToRecipes, pathToIngredients);
        foreach (var category in _categories.Create(pathToRecipes, pathToIngredients, pathToAddedRecipes).All)
            Children.Add(new CategoryButton(mainWindow, category, ascendingOrder, categoryColor));
    }
}