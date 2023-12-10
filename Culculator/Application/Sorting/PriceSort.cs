namespace Culculator.Application;

public class PriceSort : ISortMethod
{
    public Category SortAscending(Category category)
    {
        return new Category(category.Name, category.Dishes.OrderBy(d => d.Price));
    }

    public Category SortDescending(Category category)
    {
        return new Category(category.Name, category.Dishes.OrderByDescending(d => d.Price));
    }

    public string AscendingSortDescription => "Сортировать по возрастанию цены";
    public string DescendingSortDescription => "Сортировать по убыванию цены";
}