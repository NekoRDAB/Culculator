namespace Culculator.Application;

public interface ISortMethod
{
    public Category SortAscending(Category category);
    public Category SortDescending(Category category);
    public string AscendingSortDescription { get; }
    public string DescendingSortDescription { get; }
}