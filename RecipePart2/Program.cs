using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipePart2
{
    // Define delegate type for calorie notification
    public delegate void CalorieNotification(double totalCalories);

    class Program
    {
        static void Main()
        {
            // Initialize a recipe object
            Recipe recipe = new Recipe();
            recipe.NotifyCalorieExceedance += HandleCalorieExceedance;

            // Method to handle calorie exceedance notification
            static void HandleCalorieExceedance(double totalCalories)
            {
                //display warning message if calories exceed 300
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Warning: Total calories ({totalCalories}) exceed 300!");
                //add explanation so that user can understand why the got a warning about exceeding 300
                Console.WriteLine($"Note:Consuming more calories than you burn leads to excess energy being stored as fat.");
                Console.ResetColor();
            }

            bool exit = false;

            // Add welcome message for the user
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to The GoodFood Recipe Application!");
            Console.ResetColor();

            // Create loop for user interaction
            while (!exit)
            {
                // App menu options
                Console.WriteLine("\nPlease Select an option\n"
                                  + "1. - Enter new Recipe\n"
                                  + "2. - Enter Recipe Details\n"
                                  + "3. - Display List of Recipes\n"
                                  + "4. - Display Full Recipe\n"
                                  + "5. - Scale Recipe\n"
                                  + "6. - Reset Quantities\n"
                                  + "7. - Clear All Data\n"
                                  + "8. - Exit Program");

                int choice; // Declare int variable for the number options
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    // Switch case to handle user choice
                    switch (choice)
                    {
                        case 1:
                            recipe.EnterNewRecipe();
                            break;
                        case 2:
                            recipe.EnterRecipeDetails();
                            break;
                        case 3:
                            recipe.DisplayListOfRecipes();
                            break;
                        case 4:
                            recipe.DisplayFullRecipe();
                            break;
                        case 5:
                            recipe.ScaleRecipe();
                            break;
                        case 6:
                            recipe.ResetQuantities();
                            break;
                        case 7:
                            recipe.ClearAllData();
                            break;
                        case 8:
                            exit = true;
                            break;
                        default:
                            // Add color for the error message 
                            Console.ForegroundColor = ConsoleColor.Red;
                            // Add error message for invalid choice selection
                            Console.WriteLine("Invalid selection. Please kindly try again.");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Add error message for invalid input
                    Console.WriteLine("Invalid input. Please enter a number from the options");
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }
    }

    class RecipeDetails
    {
        public List<string> recipeNames { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public List<string> foodgroup { get; set; }

        public RecipeDetails()
        {
            //initialize lists
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
            recipeNames = new List<string>();
            foodgroup = new List<string>();
        }
    }

    // Create an ingredient class to represent each ingredient type in the recipe
    class Ingredient
    {
        // Add get and set methods
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double OriginalQuantity { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }

        // Add this method to reset the ingredient quantity to its original form
        public void ResetQuantity()
        {
            Quantity = OriginalQuantity;
        }
    }

    // Create recipe class
    class Recipe
    {
        private List<Ingredient> ingredients;
        private List<string> steps;
        private List<string> recipeNames;
        private List<string> foodGroups;
        private Dictionary<string, RecipeDetails> recipeDetailsMap;

        // Declare event for calorie notification
        public event CalorieNotification NotifyCalorieExceedance;

        public Recipe()
        {
            //initialize lists and dictionary
            ingredients = new List<Ingredient>();
            steps = new List<string>();
            recipeNames = new List<string>();
            foodGroups = new List<string>();
            recipeDetailsMap = new Dictionary<string, RecipeDetails>();
        }

      

        // Method to calculate total calories
        public double CalculateTotalCalories()
        {
            return ingredients.Sum(ingredient => ingredient.Calories * ingredient.Quantity);
        }

        //method to enter a new recipe
        public void EnterNewRecipe()
        {
            Console.WriteLine("Please enter the name of the recipe: ");
            string recipeName = Console.ReadLine();
            recipeNames.Add(recipeName);
            recipeNames.Sort(); // Sort the recipe names
            recipeDetailsMap[recipeName] = new RecipeDetails();

            //output success message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Recipe has been successfully entered!");
            Console.ResetColor();
        }

        // Create method to enter recipe
        public void EnterRecipeDetails()
        {
            if (recipeNames.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Add error message if no recipe was found
                Console.WriteLine("No recipe found. Please enter recipe first");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Please Enter the number of the recipe you want to add details to:");
            int selectedRecipeIndex;
            for (int i = 0; i < recipeNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipeNames[i]}");
            }
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out selectedRecipeIndex) && selectedRecipeIndex >= 1 && selectedRecipeIndex <= recipeNames.Count)
            {
                string selectedRecipeName = recipeNames[selectedRecipeIndex - 1];
                RecipeDetails details = recipeDetailsMap[selectedRecipeName];

                Console.WriteLine("Please enter the number of ingredients: ");
                int numberOfIngredients;
                while (!int.TryParse(Console.ReadLine(), out numberOfIngredients))
                {
                    //display error message for invalid input
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a number.");
                    Console.ResetColor();
                }

                for (int i = 0; i < numberOfIngredients; i++)
                {
                    Ingredient ingredient = new Ingredient();

                    Console.WriteLine($"Please enter the name of ingredient {i + 1}: ");
                    ingredient.Name = Console.ReadLine();

                    //loop to get valid food group input
                    while (true)
                    {
                        Console.WriteLine($"Please enter the Food group for ingredient {i + 1} from the options below:\n"
                                         + "1. - Fruit and Vegetables\n"
                                         + "2. - Dairy\n"
                                         + "3. - Carbohydrates and Grains\n"
                                         + "4. - Protein\n"
                                         + "5. - Fats and Sugars");
                        int foodGroupChoice;
                        if (int.TryParse(Console.ReadLine(), out foodGroupChoice) && foodGroupChoice >= 1 && foodGroupChoice <= 5)
                        {
                            string foodGroupName = foodGroupChoice switch
                            {
                                1 => "Fruit and Vegetables",
                                2 => "Dairy",
                                3 => "Carbohydrates and Grains",
                                4 => "Protein",
                                5 => "Fats and Sugars",
                                _ => "Unknown"
                            };
                            ingredient.FoodGroup = foodGroupName;
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid selection. Please kindly try again.");
                            Console.ResetColor();
                        }
                    }

                    //loop to get valid quantity input
                    while (true)
                    {
                        Console.WriteLine($"Please enter the quantity of ingredient {i + 1}: ");
                        if (double.TryParse(Console.ReadLine(), out double ingredientQuantity))
                        {
                            ingredient.Quantity = ingredientQuantity;
                            ingredient.OriginalQuantity = ingredientQuantity; // Store original quantity
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input! The ingredient Quantity must be a number.");
                            Console.ResetColor();
                        }
                    }

                    Console.WriteLine($"Please enter the unit of measurement for ingredient {i + 1}: ");
                    ingredient.UnitOfMeasurement = Console.ReadLine();

                    while (true)
                    {
                        //prompt user to enter the amount of calories, also added an explanation in brackets
                        Console.WriteLine($"Please enter the number of calories for ingredient {i + 1} (per unit of measurement e.g per liters/cup): ");
                        if (double.TryParse(Console.ReadLine(), out double ingredientCalories))
                        {
                            ingredient.Calories = ingredientCalories;
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input! The ingredient calories must be a number.");
                            Console.ResetColor();
                        }
                    }

                    details.Ingredients.Add(ingredient);
                }

                //get number of steps
                Console.WriteLine($"Please Enter the number of steps: ");
                int numberOfSteps;
                while (!int.TryParse(Console.ReadLine(), out numberOfSteps))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a valid positive integer i.e. a number.");
                    Console.ResetColor();
                }

                //get steps details
                for (int i = 0; i < numberOfSteps; i++)
                {
                    Console.WriteLine($"Please enter a description of what the user should do for step {i + 1}:");
                    details.Steps.Add(Console.ReadLine());
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Recipe details have been successfully entered!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please select a valid recipe number.");
                Console.ResetColor();
            }
        }

        public void DisplayListOfRecipes()
        {
            if (recipeNames.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Add error message if no recipe was found
                Console.WriteLine("No recipe found. Please enter recipe first");
                Console.ResetColor();
                return;
            }

            //display list of recipes
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("List of recipes:");
            foreach (string recipeName in recipeNames)
            {
                Console.WriteLine(recipeName);
            }
            Console.ResetColor();
        }

        //method to see full recipe
        public void DisplayFullRecipe()
        {
            if (recipeNames.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Add error message if no recipe was found
                Console.WriteLine("No recipe found. Please enter recipe first");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Please Enter the number of the recipe you want to display:");
            int selectedRecipeIndex;
            for (int i = 0; i < recipeNames.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"{i + 1}. {recipeNames[i]}");
                Console.ResetColor();
            }

            if (int.TryParse(Console.ReadLine(), out selectedRecipeIndex) && selectedRecipeIndex >= 1 && selectedRecipeIndex <= recipeNames.Count)
            {
                //display selected recipe
                string selectedRecipeName = recipeNames[selectedRecipeIndex - 1];
                RecipeDetails details = recipeDetailsMap[selectedRecipeName];

                if (details.Ingredients.Count == 0 || details.Steps.Count == 0)
                {
                    //error message when user asks for a recipe that doesnt have details
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No recipe details found. Please enter recipe details first.");
                    Console.ResetColor();
                    return;
                }

                //display full recipe
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"How to make {selectedRecipeName} in just {details.Steps.Count} simple step(s).");
                Console.WriteLine($"Let's get started!");

                Console.WriteLine($"\nName of Recipe: {selectedRecipeName}");
                Console.WriteLine($"Number of ingredients: {details.Ingredients.Count}");
                Console.WriteLine($"Number of steps: {details.Steps.Count}");

                Console.WriteLine("\nIngredients:");
                for (int i = 0; i < details.Ingredients.Count; i++)
                {
                    Console.WriteLine($" {i + 1}. {details.Ingredients[i].Quantity} {details.Ingredients[i].UnitOfMeasurement} of {details.Ingredients[i].Name}");
                }

                Console.WriteLine("\nSteps:");
                for (int i = 0; i < details.Steps.Count; i++)
                {
                    Console.WriteLine($"Step {i + 1}: {details.Steps[i]}");
                }

                Console.WriteLine("\nNutritional Facts: ");
                foreach (Ingredient ingredient in details.Ingredients)
                {
                    Console.WriteLine($"{ingredient.Name} belongs to food group: {ingredient.FoodGroup}");
                }

                //display total calories
                double totalCalories = details.Ingredients.Sum(ingredient => ingredient.Calories * ingredient.Quantity);
                Console.WriteLine($"\nTotal Calories: {totalCalories}");
                //add explanation as to what a calorie is
                Console.WriteLine($"(Calories are units of energy that our bodies get " +
                                    $"from the food and drinks we consume)");
                if (totalCalories > 300 && NotifyCalorieExceedance != null)
                {
                    NotifyCalorieExceedance(totalCalories);
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nWe Hope you enjoy!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Recipe not found");
                Console.ResetColor();
            }
        }

        //method to scale recipe
        public void ScaleRecipe()
        {
            if (recipeNames.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Add error message if no recipe was found
                Console.WriteLine("No recipe details found. Please enter recipe details first");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Please Enter the number of the recipe you want to scale:");
            int selectedRecipeIndex;
            for (int i = 0; i < recipeNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipeNames[i]}");
            }
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out selectedRecipeIndex) && selectedRecipeIndex >= 1 && selectedRecipeIndex <= recipeNames.Count)
            {
                string selectedRecipeName = recipeNames[selectedRecipeIndex - 1];
                RecipeDetails details = recipeDetailsMap[selectedRecipeName];

                if (details.Ingredients.Count == 0 || details.Steps.Count == 0)
                {
                    //enter this message when users ask for a non existing recipe
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No recipe details found. Please enter recipe details first.");
                    Console.ResetColor();
                    return;
                }

                Console.WriteLine("Please enter the scaling factor from one of these options\n"
                                   + " 0.5 (Scale by a half)\n"
                                   + " 2 (Scale by a double)\n"
                                   + " 3 ( Scale by a triple)");
                if (!double.TryParse(Console.ReadLine(), out double scaleFactor))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Scaling factor must be a number");
                    Console.ResetColor();
                    return;
                }

                switch (scaleFactor)
                {
                    case 0.5:
                    case 2:
                    case 3:
                        foreach (Ingredient ingredient in details.Ingredients)
                        {
                            ingredient.Quantity = ingredient.OriginalQuantity * scaleFactor;
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You have successfully scaled the recipe");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input! Scaling factor must be 0.5, 2 or 3");
                        Console.ResetColor();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Recipe not found");
                Console.ResetColor();
            }
        }

        //method for resetting quantity
        public void ResetQuantities()
        {
            if (recipeNames.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Add error message if no recipe was found
                Console.WriteLine("No recipe details found. Please enter recipe details first");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Please Enter the number of the recipe you want to reset:");
            int selectedRecipeIndex;
            for (int i = 0; i < recipeNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipeNames[i]}");
            }
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out selectedRecipeIndex) && selectedRecipeIndex >= 1 && selectedRecipeIndex <= recipeNames.Count)
            {
                //displays selected recipe for reset
                string selectedRecipeName = recipeNames[selectedRecipeIndex - 1];
                RecipeDetails details = recipeDetailsMap[selectedRecipeName];

                if (details.Ingredients.Count == 0 || details.Steps.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No recipe details found. Please enter recipe details first.");
                    Console.ResetColor();
                    return;
                }

                foreach (Ingredient ingredient in details.Ingredients)
                {
                    ingredient.ResetQuantity();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Resetting quantities to original values.....");
                Console.WriteLine("Quantities have reset to original values successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Recipe not found");
                Console.ResetColor();
            }
        }

        //method for clearing data
        public void ClearAllData()
        {
            if (recipeNames.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Add error message if no recipe was found
                Console.WriteLine("No recipe details found. Please enter recipe details first");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Please Enter the number of the recipe you want to clear:");
            int selectedRecipeIndex;
            for (int i = 0; i < recipeNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipeNames[i]}");
            }
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out selectedRecipeIndex) && selectedRecipeIndex >= 1 && selectedRecipeIndex <= recipeNames.Count)
            {
                string selectedRecipeName = recipeNames[selectedRecipeIndex - 1];
                RecipeDetails details = recipeDetailsMap[selectedRecipeName];

                if (details.Ingredients.Count == 0 || details.Steps.Count == 0)
                {
                    //add errror message
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No recipe details found. Please enter recipe details first.");
                    Console.ResetColor();
                    return;
                }

                //ask for confirmation before clearing
                Console.WriteLine("Are you sure you want to clear all data? (Y/N)");
                string confirmation = Console.ReadLine().ToUpper();

                if (confirmation == "Y")
                {
                    recipeDetailsMap.Remove(selectedRecipeName);
                    recipeNames.RemoveAt(selectedRecipeIndex - 1);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Recipe {selectedRecipeName} and its data have been cleared successfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Operation canceled. Data remains intact.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Recipe not found");
                Console.ResetColor();
            }
        }
    }
}
