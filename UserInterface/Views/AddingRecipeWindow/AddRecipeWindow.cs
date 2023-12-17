using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Castle.Core.Internal;
using Culculator.Application.Extensions;

namespace UserInterface.Views;

class AddRecipeLogic : Panel
{
    public AddRecipeLogic(MainWindow mainWindow, Category category, Color categoryColor)
    {
        var recipeParameters = new AddRecipeParameters();
        Children.Add(recipeParameters);

        var returnImageContent = new ContentControl()
        {
            Content = new Image
            {
                Source = new Bitmap("Images/ReturnButton.png"),
                Width = 30,
                Height = 30,
            }
        };
        
        var addImageContent = new ContentControl()
        {
            Content = new Image
            {
                Source = new Bitmap("Images/AddButton.png"),
                Width = 30,
                Height = 30,
            }
        };
        
        var returnToDishesMenuButton = new BaseTargetButton(90, 50, returnImageContent,  Brushes.Transparent, Brushes.Gray, null, VerticalAlignment.Bottom, HorizontalAlignment.Left,
            () => { mainWindow.Content = new DishesMenu(mainWindow, category, categoryColor); });
        Children.Add(returnToDishesMenuButton);

        var addRecipeButton = new BaseTargetButton(90, 50, addImageContent, Brushes.Transparent, Brushes.Gray, null, VerticalAlignment.Bottom, HorizontalAlignment.Right,
            () =>
        {
            var repository = new Repository();
            
            var ingredients =
                Parser.ConvertIngredientsEntryToIngredients(recipeParameters.selectedIngredients);
         
            var dish = new Dish
            (ingredients,
                int.Parse(recipeParameters.portionsCountTextBox.Text),
                recipeParameters.recipeInfoTextBox.Text,
                recipeParameters.recipeNameTextBox.Text,
                category.Name);
            
            var newDishEntry = new DishEntry
            {
                Name = dish.Name,
                Category = dish.Category,
                Ingredients = Parser.ConvertIngredientsToString(ingredients),
                PortionsAmount = dish.NumberOfPortions,
                RecipeInfo = dish.Recipe
            };
            
            repository.AddRecipeToPersonalDB(newDishEntry);
            category.Dishes.Add(dish);
            mainWindow.Content = new DishesMenu(mainWindow, category, categoryColor);
        });
        Children.Add(addRecipeButton);
    }
}

class AddRecipeParameters : Panel
{
    public TextBox recipeNameTextBox;
    public TextBox portionsCountTextBox;
    public TextBox recipeInfoTextBox;
    public readonly Dictionary<IngredientEntry, int> selectedIngredients = new();

    public AddRecipeParameters()
    {
        void AddSearchedIngredients(TextBox textBox, ComboBox comboBox, List<string> list)
        {
            var searchText = textBox.Text.ToLower();
            comboBox.Items.Clear();
            var filteredIngredients = list
                .Where(ingredient => ingredient.ToLower().Contains(searchText)
                                     && !string.IsNullOrEmpty(ingredient));
            foreach (var ingr in filteredIngredients)
            {
                comboBox.Items.Add(ingr);
            }
        }

        var stackPanel = new StackPanel { VerticalAlignment = VerticalAlignment.Top };
        Children.Add(stackPanel);

        recipeNameTextBox = new TextBox
        {
            Width = 200,
            Watermark = "Введите название рецепта",
            Margin = new Thickness(5)
        };
        stackPanel.Children.Add(recipeNameTextBox);

        portionsCountTextBox = new TextBox
        {
            MaxLength = 3,
            Width = 200,
            Watermark = "Введите количество порций",
            Margin = new Thickness(5)
        };
        portionsCountTextBox.KeyDown += (sender, args) =>
        {
            var inputText = args.Key.ToString();
            var isNumeric = inputText.IsNumeric(portionsCountTextBox.Text);
            var caretIndex = portionsCountTextBox.CaretIndex;
            if (inputText == "D0" && caretIndex == 0)
                isNumeric = false;
            args.Handled = !isNumeric;
        };
        stackPanel.Children.Add(portionsCountTextBox);

        var selectedIngredientsPanel = new StackPanel
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Orientation = Orientation.Vertical,
        };
        Children.Add(selectedIngredientsPanel);

        var ingredientsBox = new ComboBox()
        {
            Width = 220,
            PlaceholderText = "Выберите ингредиент",
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(5)
        };

        var searchTextBox = new TextBox()
        {
            Margin = new Thickness(5),
            Width = 220,
            HorizontalAlignment = HorizontalAlignment.Left
        };

        var repository = new Repository();
        var allIngredients = repository.GetIngredientsNames();
        allIngredients.Sort();
        foreach (var ingredient in allIngredients)
        {
            ingredientsBox.Items.Add(ingredient);
        }

        searchTextBox.KeyUp += (sender, a) =>
        {
            ingredientsBox.IsDropDownOpen = true;
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                AddSearchedIngredients(searchTextBox, ingredientsBox, allIngredients);
            }
        };

        ingredientsBox.DropDownOpened += (sender, a) =>
        {
            if (searchTextBox.Text == null)
            {
                ingredientsBox.Items.Clear();
                foreach (var ingr in allIngredients)
                {
                    ingredientsBox.Items.Add(ingr);
                }
            }
            else
            {
                AddSearchedIngredients(searchTextBox, ingredientsBox, allIngredients);
            }
        };


        var stp = new StackPanel()
        {
            Orientation = Orientation.Vertical,
        };

        stp.Children.Add(searchTextBox);
        stp.Children.Add(ingredientsBox);


        selectedIngredientsPanel.Children.Add(stp);


        var addButton = new Button
        {
            Width = 200,
            Height = 50,
            Content = "Добавить ингредиент",
            FontSize = 16,
            Background = null,
            Foreground = Brushes.DarkGray,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(10)
        };

        addButton.Click += (sender, args) =>
        {
            if (ingredientsBox.SelectedItem is string selectedIngredientName)
            {
                var selectedIngredient = repository.GetIngredientFromDB(selectedIngredientName);
                var name = selectedIngredient.Name;
                var words = name.Split("_");
                if (words.Length > 1)
                    name = words.Aggregate("", (current, t) => current + (t + "\n"));
                
                if (selectedIngredient != null)
                {
                    var ingredientPanel = new StackPanel { Orientation = Orientation.Horizontal };

                    ingredientsBox.Items.Clear();
                    searchTextBox.Text = "";

                    foreach (var ingr in allIngredients)
                        ingredientsBox.Items.Add(ingr);

                    var textBlock = new TextBlock
                    {
                        Text = name,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(3, 3),
                    };

                    ingredientPanel.Children.Add(textBlock);

                    var quantityTextBox = new TextBox
                    {
                        MaxLength = 6,
                        Width = 100,
                        Height = 20 * words.Length,
                        Watermark = "Количество",
                        Margin = new Thickness(3, 3),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    quantityTextBox.KeyDown += (sender, args) =>
                    {
                        var inputText = args.Key.ToString();
                        var isNumeric = inputText.IsNumeric(quantityTextBox.Text);
                        var caretIndex = quantityTextBox.CaretIndex;
                        if (inputText == "D0" && caretIndex == 0)
                            isNumeric = false;
                        args.Handled = !isNumeric;
                    };

                    ingredientPanel.Children.Add(quantityTextBox);

                    selectedIngredientsPanel.Children.Add(ingredientPanel);
                    selectedIngredients.Add(selectedIngredient, 0);

                    quantityTextBox.TextChanged += (textSender, textArgs) =>
                    {
                        if (int.TryParse(quantityTextBox.Text, out int quantity))
                        {
                            selectedIngredients[selectedIngredient] = quantity;
                        }
                    };
                }
            }
        };
        selectedIngredientsPanel.Children.Add(addButton);

        recipeInfoTextBox = new TextBox
        {
            Width = 600,
            Height = 400,
            Margin = new Thickness(220,20,50,10),
            Watermark = "Введите описание рецепта",
        };
        stackPanel.Children.Add(recipeInfoTextBox);
    }
}