namespace Culculator.Application.Extensions;

public static class FormattingExtensions
{
    public static string FormatPortionsNumber(this int number)
    {
        var suffix = "порций";
        switch (number % 10)
        {
            case 1 when number % 100 != 11:
                suffix = "порция";
                break;
            case >= 2 and <= 4 when !(number % 100 >= 12 && number % 100 <= 14):
                suffix = "порции";
                break;
        }
        return $"{number} {suffix}";
    }
    
    public static string ReformatMeasurementUnit(this string measurementUnit)
    {
        return measurementUnit switch
        {
            "Кг" => "Гр.",
            "Л" => "Мл.",
            _ => "Шт."
        };
    }
    
    public static double GetPriceByMeasurementUnit(this string price, string measurementUnit)
    {
        if (price == null)
            return 0;
        switch (measurementUnit)
        {
            case "Шт":
                return int.Parse(price);
            case "Кг":
            case "Л":
                return double.Parse(price) / 1000;
            default:
                return 0;
        }
    }
}