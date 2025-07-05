using System;

class Program
{
    static void Main()
    {
        // Step 1: Generate a random magic number between 1 and 100
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101); // 1 to 100 inclusive

        int guess = -1;

        // Step 2: Loop until the user guesses the magic number
        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        }
    }
}
