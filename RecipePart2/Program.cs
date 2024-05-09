﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    class Program
    {
        static void Main()
        {
            //Initialise a recipe object
            Recipe recipe = new Recipe();
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

        //add arrays
        private Ingredient[] ingredients;
        private string[] steps;
        private int ingredientCount;
        private int stepCount;
        private List<string> recipeNames;
       // private int recipeCount;

        //add constructor to initialise arrays
        public Recipe()
        {
            ingredients = new Ingredient[10]; // Initial size, can be adjusted as needed
            steps = new string[10]; // Initial size, can be adjusted as needed
            ingredientCount = 0;
            stepCount = 0;
            recipeNames = new List<string>();
            //recipeCount = 0;
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

                ingredients = new Ingredient[numberOfIngredients]; // Resize the array based on user input

                //loop to input each ingredient
                for (int i = 0; i < numberOfIngredients; i++)
                {
                    Ingredient ingredient = new Ingredient();

                    //ask user to enter ingredient name
                    Console.WriteLine($"Please enter the name of ingredient {i + 1}: ");
                    ingredient.Name = Console.ReadLine();

                    //for each ingredient, the user shall additioonally be able to enter the number of calories and the food griup that the ingredient belongs to
                    //foreach (ingredient.Name)
                    {
                        Console.WriteLine($"Please enter the number of calories for ingredient {i + 1}: ");
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
                            Console.WriteLine("Invalid input! The ingredient Quantity must be a number.");
                            Console.ResetColor();
                            return;
                        }

                        Console.WriteLine($"Please enter the Food group for ingredient {i + 1}: ");
                        ingredient.Foodgroup = Console.ReadLine();
                    }

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

                    ingredients[i] = ingredient;
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

                steps = new string[numberOfSteps]; // Resize the array based on user input

                //loop to input each step
                for (int i = 0; i < numberOfSteps; i++)
                {
                    Console.WriteLine($"Please enter a description of what the user should do for step {i + 1}:");
                    steps[i] = Console.ReadLine();
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


            Console.ForegroundColor= ConsoleColor.DarkMagenta;
            Console.WriteLine("Your List of Recipes");
            foreach (string recipeName in recipeNames.OrderBy(name => name))
            {
                Console.WriteLine(recipeNames);
            }
            Console.ResetColor();
        }
        // user can choose which recipe to display from the list

        public void DisplayFullRecipe()
        {
            if (ingredients.Length == 0 || steps.Length == 0)
            {
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
            if (int.TryParse(Console.ReadLine(), out selectedRecipeIndex) && selectedRecipeIndex >=1 && selectedRecipeIndex <= recipeNames.Count )
            {
                selectedRecipeIndex--;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Recipe: {selectedRecipeIndex}");
                Console.ResetColor();

                Console.WriteLine($"Number of ingredients: {ingredients.Length}");
                Console.WriteLine($"Number of steps: {steps.Length} ");
                //the software shall notify the user when the total calories of a recipe exceed 300
                double totalCalories = ingredients.Sum(ingredient => ingredient.Calories * ingredient.Quantity);
                Console.WriteLine($"Total Calories: {totalCalories}");

                if (totalCalories > 300)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Warning: Total calories exceed 300!");
                    Console.ResetColor();
                }

                //output the recipe ingredients
                Console.WriteLine("\nIngredients:");
                for (int i = 0; i < ingredients.Length; i++)
                {
                    Console.WriteLine($" {i + 1}. {ingredients[i].Quantity} {ingredients[i].UnitOfMeasurement} of {ingredients[i].Name}({ingredients[i].Foodgroup})");
                }

                //display the recipe steps
                Console.WriteLine("\nSteps:");
                for (int i = 0; i < steps.Length; i++)
                {
                    Console.WriteLine($"Step {i + 1}: {steps[i]}");
                }
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Recipe not found");
            }
        }        
        // add method to scale the recipe quantity
        public void ScaleRecipe()
        {
            if (ingredients.Length == 0)
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
                Console.WriteLine("Recipe not found");
            }
        }

        //add method
        public void ResetQuantities()
        {
            if (ingredients.Length == 0)
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
                Console.WriteLine("No recepes found");
            }
        }

        //create method to clear all the reipe data
        public void ClearAllData()
        {
            if (ingredients.Length == 0)
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
                    ingredients = new Ingredient[0];  // Resetting to empty arrays
                    steps = new string[0];

                    ingredientCount = 0;  // Resetting ingredientCount
                    stepCount = 0;  // Resetting stepCount

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
        }   
    }
}