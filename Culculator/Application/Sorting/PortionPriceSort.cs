namespace Culculator.Application;

public class PortionPriceSort : ISortMethod
{
    public Category SortAscending(Category category)
    {
        return new Category(category.Name, category.Dishes.OrderBy(d => d.PricePerPortion));
    }

    public Category SortDescending(Category category)
    {
        return new Category(category.Name, category.Dishes.OrderByDescending(d => d.PricePerPortion));
    }

    public string AscendingSortDescription => "Сортировать по возрастанию цены за порцию";
    public string DescendingSortDescription => "Сортировать по убыванию цены за порцию";
}