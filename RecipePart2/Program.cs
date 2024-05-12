using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    //define delegate type for calorie notification
    public delegate void CalorieNotification(double totalCalories);
    class Program
    {
        static void Main()
        {
            //Initialise a recipe object
            Recipe recipe = new Recipe();
            recipe.NotifyCalorieExceedance += HandleCalorieExceedance;

            //method to handle calorie exceedance notification
            static void HandleCalorieExceedance(double totalCalories)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Warning: Total calories ({totalCalories}) exceed 300!");
                Console.ResetColor();
            }

            bool exit = false;

            //add welcome message for the user
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to The GoodFood Recipe Application!");
            Console.ResetColor();

            //create loop for user interaction
            while (!exit)
            {
                //App menu options
                Console.WriteLine("\nPlease Select an option\n"
                                  + "1. - Enter new Recipe\n"
                                  + "2. - Enter Recipe Details\n"
                                  + "3. - Display All Recipes\n"  
                                  + "4. - Display Full Recipe\n"
                                  + "5. - Scale Recipe\n"
                                  + "6. - Reset Quantities\n"
                                  + "7. - Clear All Data\n"
                                  + "8. - Exit Program");

                int choice; //declare int variable for the number options
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    //switch case to handle user choice
                    switch (choice)
                    {
                        case 1:
                            recipe.EnterNewRecipe();
                            break;
                        case 2:
                            recipe.EnterRecipeDetails();
                            break;
                        case 3:
                            recipe.DisplayAllRecipes();
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
                            //add colour for the error message 
                            Console.ForegroundColor = ConsoleColor.Red;
                            //add error message for invalid choice selection
                            Console.WriteLine("Invalid selection. Please kindly try again.");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    //add error message for invalid input
                    Console.WriteLine("Invalid input. Please enter a number from the options");
                    Console.ResetColor();
                }

                Console.WriteLine();

            }
        }
    }

    //create an ingredient class to represent each ingredient type in the recipe
    class Ingredient
    {
        //add get and set methods
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double OriginalQuantity { get; set; }
        public double Calories {  get; set; }
        public string Foodgroup { get; set;}
        

        //add this method to reset the ingredient quantity to its original form
        public void ResetQuantity()
        {
            Quantity = OriginalQuantity;
        }
    }


    //create recipe class
    class Recipe
    {

        private List<Ingredient> ingredients;
        private List<string> steps;
        private List<string> recipeNames;
        //declare event for calorie notification
        public event CalorieNotification NotifyCalorieExceedance;
        

        public Recipe()
        {
            ingredients = new List<Ingredient>(); 
            steps = new List<string>();
            recipeNames = new List<string>();            
        }

        public void EnterNewRecipe()
        {
            Console.WriteLine("Please enter the name of the recipe: ");
            string recipeName = Console.ReadLine();
            recipeNames.Add(recipeName);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Recipe has been successfully entered!");
            Console.ResetColor();
        }

        //create method to enter recipe
        public void EnterRecipeDetails()
        {

            if (recipeNames.Count == 0 )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //add error message if no recipe was found
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

            // display full details of selected recipe
            if (int.TryParse(Console.ReadLine(), out selectedRecipeIndex) && selectedRecipeIndex >= 1 && selectedRecipeIndex <= recipeNames.Count)
            {

                Console.WriteLine("Please enter the number of ingredients: ");
                int numberOfIngredients;
                while (!int.TryParse(Console.ReadLine(), out numberOfIngredients))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a number.");
                    Console.ResetColor();
                }

                //ingredients = new Ingredient[numberOfIngredients]; // Resize the array based on user input

                //loop to input each ingredient
                for (int i = 0; i < numberOfIngredients; i++)
                {
                    Ingredient ingredient = new Ingredient();

                    //ask user to enter ingredient name
                    Console.WriteLine($"Please enter the name of ingredient {i + 1}: ");
                    ingredient.Name = Console.ReadLine();

                    Console.WriteLine($"Please enter the Food group for ingredient {i + 1} from the options below:\n"
                                     + "1. - Fruit and Vegetables\n"
                                     + "2. - Dairy\n"
                                     + "3. - Carbohydrants and Grains\n"
                                     + "4. - Protein\n"
                                     + "5. - Fats and Sugars");
                    int foodgroup;
                    if (int.TryParse(Console.ReadLine(), out foodgroup))

                    switch (foodgroup)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            //Console.ForegroundColor = ConsoleColor.Green;
                            //show success message
                            //Console.WriteLine("You have successfully scaled the recipe");
                            //Console.ResetColor();
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            // add error message for invalid input
                            Console.WriteLine("Invalid selection. Please kindly try again.");
                            Console.ResetColor();
                            break;

                    }

                    ingredient.Foodgroup = Console.ReadLine();
                    
                    //prompt user to enter ingredient quantity
                    Console.WriteLine($"Please enter the quantity of ingredient {i + 1}: ");
                    double ingredientQuantity;
                    if (double.TryParse(Console.ReadLine(), out ingredientQuantity))
                    {
                        ingredient.Quantity = ingredientQuantity;
                        ingredient.OriginalQuantity = ingredientQuantity; // Store original quantity
                    }
                    else
                    {
                        //display error message for invalid input
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input! The ingredient Quantity must be a number.");
                        Console.ResetColor();
                        return;
                    }

                    //prompt user to enter unit of measurement
                    Console.WriteLine($"Please enter the unit of measurement for ingredient {i + 1}: ");
                    ingredient.UnitOfMeasurement = Console.ReadLine();

                    Console.WriteLine($"Please enter the number of calories for ingredient {i + 1} (per unit of measurent e.g per liters/cup): ");
                    double ingredientCalories;
                    if (double.TryParse(Console.ReadLine(), out ingredientCalories))
                    {
                        ingredient.Calories = ingredientCalories;
                        //ingredient.OriginaCalories = ingredientCalories; // Store original quantity
                    }
                    else
                    {
                        //display error message for invalid input
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input! The ingredient calories must be a number.");
                        Console.ResetColor();
                        return;
                    }

                    ingredients.Add(ingredient);
                }

                //ask user to enter the number of steps
                Console.WriteLine($"Enter the number of steps: ");
                int numberOfSteps;
                while (true)
                {
                    if (!int.TryParse(Console.ReadLine(), out numberOfSteps))
                    {
                        //add error message for invalid input
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input! Please enter a valid positive integer i.e. a number.");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }

                //steps = new string[numberOfSteps]; // Resize the array based on user input

                //loop to input each step
                for (int i = 0; i < numberOfSteps; i++)
                {
                    Console.WriteLine($"Please enter a description of what the user should do for step {i + 1}:");
                    steps.Add(Console.ReadLine());
                }

                //add successful message 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Recipe details have been successfully entered!");
                Console.ResetColor();
            }
        }

        //add method is display the full recipe

        //display a list of all the recipes to the user in alphabetical order by name
        public void DisplayAllRecipes()
        {
            if (recipeNames.Count == 0) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //add error message if no recipe name was found
                Console.WriteLine("No recipes found. Please enter recipes first");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Your List of Recipes");
            foreach (string recipeName in recipeNames.OrderBy(name => name))
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(recipeName);
                Console.ResetColor();
            }
        }
        // user can choose which recipe to display from the list

        public void DisplayFullRecipe()
        {
            if (ingredients.Count == 0 || steps.Count == 0)
            {
                //add colour to error message to make it stand out
                Console.ForegroundColor = ConsoleColor.Red;
                //add error message if no recipe details were found
                Console.WriteLine("No recipe details found. Please enter recipe details first");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Please Enter the number of the recipe you want to view:");
            int selectedRecipeIndex;
            for (int i = 0; i < recipeNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipeNames[i]}");
            }
            Console.ResetColor();

            // display full details of selected recipe
            if (int.TryParse(Console.ReadLine(), out selectedRecipeIndex) && selectedRecipeIndex >= 1 && selectedRecipeIndex <= recipeNames.Count)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                selectedRecipeIndex--;
                Console.WriteLine($"How to make {selectedRecipeIndex} in just {steps.Count} simple step(s).");
                Console.WriteLine($"Let's get started!");


                Console.WriteLine($"\nNumber of ingredients: {ingredients.Count}");
                Console.WriteLine($"Number of steps: {steps.Count} ");

                //output the recipe ingredients
                Console.WriteLine("\nIngredients:");
                for (int i = 0; i < ingredients.Count; i++)
                {
                    Console.WriteLine($" {i + 1}. {ingredients[i].Quantity} {ingredients[i].UnitOfMeasurement} of {ingredients[i].Name}");
                }

                //display the recipe steps
                Console.WriteLine("\nSteps:");
                for (int i = 0; i < steps.Count; i++)
                {
                    Console.WriteLine($"Step {i + 1}: {steps[i]}");
                }

                Console.WriteLine("\nNutritional Facts: ");
                
                for (int i = 0; i < ingredients.Count; i++)
                {
                    Console.WriteLine($"Foodgroups : {ingredients[i].Name} = {ingredients[i].Foodgroup}");
                }

                //the software shall notify the user when the total calories of a recipe exceed 300
                double totalCalories = ingredients.Sum(ingredient => ingredient.Calories * ingredient.Quantity);
                if (totalCalories > 300 && NotifyCalorieExceedance !=null )
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
        // add method to scale the recipe quantity
        public void ScaleRecipe()
        {
            if (ingredients.Count == 0)
            {
                //add colour to the error message
                Console.ForegroundColor = ConsoleColor.Red;
                //add error message if no recipe details are found
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
                selectedRecipeIndex--;
                Console.WriteLine("Please enter the scaling factor from one of these options\n"
                               + " 0.5 (Scale by a half)\n"
                               + " 2 (Scale by a double)\n"
                               + " 3 ( Scale by a triple");
                double scaleFactor;
                if (!double.TryParse(Console.ReadLine(), out scaleFactor))
                {
                    //add colour to error message
                    Console.ForegroundColor = ConsoleColor.Red;
                    //display error message for invalid input
                    Console.WriteLine("Invalid input! Scaling factor must be a number");
                    Console.ResetColor();
                    return;
                }

                switch (scaleFactor)
                {
                    case 0.5:
                    case 2:
                    case 3:

                        foreach (Ingredient ingredient in ingredients)
                        {
                            ingredient.Quantity = ingredient.OriginalQuantity * scaleFactor;
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        //show success message
                        Console.WriteLine("You have successfully scaled the recipe");
                        Console.ResetColor();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        // add error message for invalid input
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

        //add method
        public void ResetQuantities()
        {
            if (ingredients.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //add error message if no recipe details are foung
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
                selectedRecipeIndex--;

                //reset each ingredient quantity
                foreach (Ingredient ingredient in ingredients)
                {
                    ingredient.ResetQuantity();
                }

                //add colour for message
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

        //create method to clear all the reipe data
        public void ClearAllData()
        {
            if (ingredients.Count == 0)
            {
                // Display error message if no recipe details were found
                Console.ForegroundColor = ConsoleColor.Red;
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
                selectedRecipeIndex--;
                //ask user for confirmation before clearing
                Console.WriteLine("Are you sure you want to clear all data? (Y/N)");
                string confirmation = Console.ReadLine().ToUpper();

                //check the user's confirmation
                if (confirmation == "Y")
                {
                    //clear all the ingredients and steps
                    //ingredients = new Ingredient[0];  // Resetting to empty arrays
                    //steps = new string[0];
                    ingredients.Clear();
                    steps.Clear();

                    //ingredientCount = 0;  // Resetting ingredientCount
                    //stepCount = 0;  // Resetting stepCount

                    Console.ForegroundColor = ConsoleColor.Green;
                    //display success message
                    Console.WriteLine("All data has been cleared successfully!");
                    Console.ResetColor();
                }
                else
                {
                    //diplay message for the cancellation of clearing the data
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
