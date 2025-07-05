using System;

class Program
{
    static void Main(string[] args)
    {
        // This program prompts the user for their first and last name
        // and stores them in variables.
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();

        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        Console.Write($"Your name is {lastName}, {firstName} {lastName}"Eden);
    }
}