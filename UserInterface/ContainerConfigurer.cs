using Avalonia.Media;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Parameters;
using UserInterface.Views;

namespace UserInterface;

public static class ContainerConfigurer
{
    private static StandardKernel ConfigureKernel()
    {
        var kernel = new StandardKernel();
        kernel.Bind<ICategories>().To<AutoCategories>();
        kernel.Bind<IIngredientContext>().To<IngredientsContextSQLite>();
        kernel.Bind<IRecipesContext>().To<RecipesContextSQLite>();
        kernel.Bind<IAddedRecipeContext>().To<AddedRecipeContext>();
        kernel.Bind<Application>().ToSelf();
        kernel.Bind<IRepository>().To<Repository>();
        kernel.Bind<ICategoriesFactory>().ToFactory();
        kernel.Bind<IIngredientContextFactory>().ToFactory();
        kernel.Bind<IRecipeContextFactory>().ToFactory();
        kernel.Bind<IApplicationFactory>().ToFactory();
        kernel.Bind<IRepositoryFactory>().ToFactory();
        kernel.Bind<IAddedRecipeContextFactory>().ToFactory();
        return kernel;
    }

    public static CategoriesPanel GetCategoriesPanel(MainWindow mainWindow, bool ascendingOrder, Color categoryColor)
    {
        var container = ConfigureKernel();
        return container.Get<CategoriesPanel>(new ConstructorArgument("mainWindow", mainWindow),
            new ConstructorArgument("ascendingOrder", ascendingOrder),
            new ConstructorArgument("categoryColor", categoryColor));
    }
}