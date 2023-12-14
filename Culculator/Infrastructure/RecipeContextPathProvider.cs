namespace Culculator.Infrastructure;

public class RecipeContextPathProvider
{
    public readonly string PathToDefaultDB;
    public readonly string PathToAddedDB;
    
    public RecipeContextPathProvider(string pathToDefaultDb, string pathToAddedDb)
    {
        PathToDefaultDB = pathToDefaultDb;
        PathToAddedDB = pathToAddedDb;
    }
}