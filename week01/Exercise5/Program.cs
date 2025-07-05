using System;

class Program
{
    // Function 1: DisplayWelcome
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // Function 2: PromptUserName
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    // Function 3: PromptUserNumber
    static int PromptUserNumber()
    {
        Console.Write("What is your favorite number? ");
        return int.Parse(Console.ReadLine());
    }

    // Function 4: SquareNumber
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Function 5: DisplayResult
    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}.");
    }

    // Main Function
    static void Main()
    {
        DisplayWelcome();                          // Step 1: Greet the user

        string userName = PromptUserName();        // Step 2: Get user name
        int userNumber = PromptUserNumber();       // Step 3: Get user's favorite number

        int squared = SquareNumber(userNumber);    // Step 4: Square the number

        DisplayResult(userName, squared);          // Step 5: Display result
    }
}
