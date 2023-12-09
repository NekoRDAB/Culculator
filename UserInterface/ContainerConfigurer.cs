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
        kernel.Bind<ICategoriesFactory>().ToFactory();
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