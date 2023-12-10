using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

public class CategoriesPanel : StackPanel
{
    static ICategoriesFactory _categories;
    
    public CategoriesPanel(MainWindow mainWindow, ICategoriesFactory categories, Color categoryColor)
    {
        _categories = categories;
        Margin = new(20);
        Spacing = 10;
        var dir = Environment.CurrentDirectory;
        var pathToRecipes = dir.Replace("UserInterface", "Culculator\\RecipesDataBase.db")
            .Replace("\\bin\\Release\\net6.0", "");
        var pathToIngredients = dir.Replace("UserInterface", "Culculator\\IngredientsDataBase.db")
            .Replace("\\bin\\Release\\net6.0", "");
        var pathToAddedRecipes = dir.Replace("UserInterface", "Culculator\\AddedRecipesDataBase.db")
            .Replace("\\bin\\Release\\net6.0", "");
        foreach (var category in _categories.Create(pathToRecipes, pathToIngredients, pathToAddedRecipes).All)
            Children.Add(new CategoryButton(mainWindow, category, categoryColor));
    }
}