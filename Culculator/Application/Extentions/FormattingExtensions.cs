namespace Culculator.Application;

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
}