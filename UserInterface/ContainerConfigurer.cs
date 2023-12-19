using System;
using Avalonia.Media;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Parameters;
using UserInterface.Views;
using UserInterface.Views.IngredientAddition;

namespace UserInterface;

public static class ContainerConfigurer
{
    private static readonly string pathToRecipes;
    private static readonly string pathToAddedRecipes;
    static ContainerConfigurer()
    {
        var dir = Environment.CurrentDirectory;
        pathToRecipes = dir.Replace("UserInterface", "Culculator\\RecipesDataBase.db")
            .Replace("\\bin\\Release\\net6.0", "");
        pathToAddedRecipes = dir.Replace("UserInterface", "Culculator\\AddedRecipesDataBase.db")
            .Replace("\\bin\\Release\\net6.0", "");
    }
    private static StandardKernel ConfigureKernel()
    {
        
        var kernel = new StandardKernel();
        kernel.Bind<ICategories>().To<AutoCategoriesCollector>();
        kernel.Bind<IRecipesContext>().To<RecipesContextSQLite>();
        kernel.Bind<IAddedRecipeContext>().To<AddedRecipeContext>();
        kernel.Bind<DishesCollector>().ToSelf();
        kernel.Bind<IRepository>().To<Repository>();
        kernel.Bind<RecipeContextPathProvider>()
            .ToConstant(new RecipeContextPathProvider(pathToRecipes, pathToAddedRecipes));
        kernel.Bind(x => x
            .FromAssemblyContaining<ISortMethod>()
            .SelectAllClasses()
            .InheritedFrom<ISortMethod>()
            .BindAllInterfaces());
        return kernel;
    }

    public static CategoriesPanel GetCategoriesPanel(MainWindow mainWindow, Color categoryColor)
    {
        var container = ConfigureKernel();
        return container.Get<CategoriesPanel>(new ConstructorArgument("mainWindow", mainWindow),
            new ConstructorArgument("categoryColor", categoryColor));
    }

    public static SortButton GetSortButton(MainWindow mainWindow, Category category, Color categoryColor)
    {
        var container = ConfigureKernel();
        return container.Get<SortButton>(new ConstructorArgument("mainWindow", mainWindow),
            new ConstructorArgument("currentCategory", category),
            new ConstructorArgument("categoryColor", categoryColor));
    }

    public static IngredientInput GetIngredientInput(MainWindow mainWindow)
    {
        var container = ConfigureKernel();
        return container.Get<IngredientInput>(new ConstructorArgument("parent", mainWindow));
    }
}