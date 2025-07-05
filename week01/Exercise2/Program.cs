using System;

class Program
{
    static void Main()
    {
        // Ask the user for their grade percentage
        Console.Write("What is your grade percentage? ");
        string input = Console.ReadLine();
        int gradePercentage = int.Parse(input);

        // Create a variable to hold the letter grade
        string letter = "";

        // Determine the letter grade
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Print the letter grade
        Console.WriteLine($"Your letter grade is: {letter}");

        // Check if the user passed or not
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course. 🎉");
        }
        else
        {
            Console.WriteLine("Don't give up! Keep trying and you'll succeed next time. 💪");
        }
    }

}