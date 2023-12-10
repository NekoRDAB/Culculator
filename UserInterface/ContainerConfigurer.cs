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
        kernel.Bind<IRecipesContext>().To<RecipesContextSQLite>();
        kernel.Bind<IAddedRecipeContext>().To<AddedRecipeContext>();
        kernel.Bind<Application>().ToSelf();
        kernel.Bind<IRepository>().To<Repository>();
        kernel.Bind<ICategoriesFactory>().ToFactory();
        kernel.Bind<IRecipeContextFactory>().ToFactory();
        kernel.Bind<IApplicationFactory>().ToFactory();
        kernel.Bind<IRepositoryFactory>().ToFactory();
        kernel.Bind<IAddedRecipeContextFactory>().ToFactory();
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
}