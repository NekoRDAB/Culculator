using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;

public class DishWithImage : Dish
{
    public string PathToImage;

    public DishWithImage(DishEntry dishEntry, Parser parser, string pathToImage) : base(dishEntry, parser)
    {
        PathToImage = pathToImage;
    }
}