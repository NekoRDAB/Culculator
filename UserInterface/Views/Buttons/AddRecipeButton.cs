using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace UserInterface.Views;

public class AddRecipeButton : Panel
{
    public AddRecipeButton(MainWindow mainWindow, Category category, Color categoryColor)
    {
        Children.Add(new Button
        {
            Width = 70,
            Height = 40,
            Content = new Image
            {
                Source = new Bitmap("Images/AddButton.png"),
                Width = 30,  
                Height = 30,
            },
            Background = Brushes.Transparent,
            VerticalContentAlignment = VerticalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Right,
            Command = ReactiveCommand.Create(() =>
            {
                var recipeParameters = new AddRecipeParameters();
                var addReturnButton = new AddAndReturnButton(mainWindow, category, categoryColor, recipeParameters.recipeNameTextBox, recipeParameters.selectedIngredients, recipeParameters.recipeInfoTextBox, recipeParameters.portionsCountTextBox);
                var returnToDishesMenuButton = new BaseTargetButton(
                    () => { mainWindow.Content = new DishesMenu(mainWindow, category, categoryColor); }, "Images/ReturnButton.png");
                mainWindow.Content = new AddRecipeWindow(recipeParameters, addReturnButton,returnToDishesMenuButton);
            })
        });
    }

    class AddRecipeWindow : Panel
    {
        public AddRecipeWindow(AddRecipeParameters recipeParameters, AddAndReturnButton addReturnButton, BaseTargetButton returnButton)
        {
            Children.Add(recipeParameters);
            Children.Add(addReturnButton);
            Children.Add(returnButton);
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
            var stackPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Top
            };
            Children.Add(stackPanel);
            
            recipeNameTextBox = new TextBox
            {
                Width = 200,
                Watermark = "Введите название рецепта"
            };
            stackPanel.Children.Add(recipeNameTextBox);

            portionsCountTextBox = new TextBox
            {
                MaxLength = 3,
                Width = 200,
                Watermark = "Введите количество порций"
            };
            portionsCountTextBox.KeyDown += (sender, args) =>
            {
                var inputText = args.Key.ToString();
                var isNumeric = inputText.IsNumeric(portionsCountTextBox.Text);
                args.Handled = !isNumeric;
            };
            stackPanel.Children.Add(portionsCountTextBox);
            
            var repository = new Repository();
            var availableIngredients = repository.GetIngredients();

            var selectedIngredientsPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Orientation = Orientation.Vertical,
            };
            Children.Add(selectedIngredientsPanel);
            
            var ingredientsBox = new ComboBox()
            {
                Width = 200,
                PlaceholderText = "Выберите ингредиент",
                VerticalAlignment = VerticalAlignment.Top,
            };

            var searchTextBox = new TextBox();
            var allIngredients = availableIngredients.Select(ingredient => ingredient.Name).ToList();
            allIngredients.Sort();
            foreach (var ingredient in allIngredients)
            {
                ingredientsBox.Items.Add(ingredient);
            }
            
            searchTextBox.KeyUp +=  (sender, a) =>
            {
                ingredientsBox.IsDropDownOpen = true;
                if(searchTextBox.Text != "")
                {
                    var searchText = searchTextBox.Text.ToLower();
                    var filteredIngredients = allIngredients
                        .Where(ingredient => ingredient.ToLower().Contains(searchText))
                        .Where(ingredient => ingredient != "")
                        .ToList();
                    ingredientsBox.Items.Clear();
                    foreach (var ingr in filteredIngredients)
                    {
                        ingredientsBox.Items.Add(ingr);
                    }
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
                    var searchText = searchTextBox.Text.ToLower();
                    var filteredIngredients = allIngredients
                        .Where(ingredient => ingredient.ToLower().Contains(searchText))
                        .Where(ingredient => ingredient != "")
                        .ToList();
                    ingredientsBox.Items.Clear();
                    foreach (var ingr in filteredIngredients)
                    {
                        ingredientsBox.Items.Add(ingr);
                    }
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
            };

            addButton.Click += (sender, args) =>
            {
                if (ingredientsBox.SelectedItem is string selectedIngredientName)
                {
                    var selectedIngredient = repository.GetIngredientFromDB(selectedIngredientName);

                    if (selectedIngredient != null)
                    {
                        var ingredientPanel = new StackPanel { Orientation = Orientation.Horizontal };

                        ingredientsBox.Items.Clear();
                        searchTextBox.Text = "";
                        foreach (var ingr in allIngredients)
                        {
                            ingredientsBox.Items.Add(ingr);
                        }

                        var textBlock = new TextBlock
                        {
                            Text = selectedIngredient.Name,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(3, 3)
                        };

                        ingredientPanel.Children.Add(textBlock);

                        var quantityTextBox = new TextBox
                        {
                            MaxLength = 3,
                            Width = 100,
                            Height = 20,
                            Watermark = "Количество",
                            Margin = new Thickness(3,3)
                        };
                        quantityTextBox.KeyDown += (sender, args) =>
                        {
                            var inputText = args.Key.ToString();
                            var isNumeric = inputText.IsNumeric(quantityTextBox.Text);
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
                Margin = new Thickness(0, 30, 50, 30),
                Watermark = "Введите описание рецепта",
                HorizontalAlignment = HorizontalAlignment.Right
            };
            stackPanel.Children.Add(recipeInfoTextBox);
        }
    }

    class AddAndReturnButton : Panel
    {
        public AddAndReturnButton(MainWindow mainWindow, Category category, Color categoryColor, TextBox recipeTextBox, Dictionary<IngredientEntry, int> selectedIngredients, TextBox recipe, TextBox count)
        {
            Children.Add(new Button
            {
                Width = 70,
                Height = 40,
                Content = new Image
                {
                    Source = new Bitmap("Images/AddButton.png"),
                    Width = 30,  
                    Height = 30,
                },
                Background = Brushes.Transparent,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
                Command = ReactiveCommand.Create(
                    () =>
                    {
                        var ingredients = new List<Ingredient>();
                        var ingr = new StringBuilder();
                 
                        foreach (var kvp in selectedIngredients)
                        {
                            ingredients.Add(new Ingredient(kvp.Key.id, kvp.Key.Name, kvp.Value, kvp.Key.MeasurementUnit, kvp.Key.Price));
                            
                            ingr.Append(kvp.Key.Name + " " + kvp.Value);

                            if (!kvp.Equals(selectedIngredients.Last()))
                            {
                                ingr.Append("; ");
                            }
                        }

                        var repository = new Repository();
                        var dish = new Dish(ingredients, int.Parse(count.Text),
                            recipe.Text, recipeTextBox.Text, category.Name);
                        var newDishEntry = new DishEntry
                        {
                            Name = dish.Name,
                            Category = dish.Category,
                            Ingredients = ingr.ToString(), 
                            PortionsAmount = dish.NumberOfPortions,
                            RecipeInfo = dish.Recipe
                        };
                        repository.AddRecipeToPersonalDB(newDishEntry);
                        category.Dishes.Add(dish);
                        mainWindow.Content = new DishesMenu(mainWindow, category, categoryColor);
                    })
            });
        }
    }
}