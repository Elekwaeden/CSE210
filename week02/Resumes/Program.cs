/* Exceeding expectations:
- Implemented mood tracking for journal entries.
- Added tagging system for better organization.
- Enabled CSV export for journal entries.
- User-Centered Features – Input prompts were designed to address real journaling barriers, making the experience more engaging and helpful.
*/

using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        string choice = "";

     while (choice != "6")
{
    Console.WriteLine("\nJournal Menu:");
    Console.WriteLine("1. Write a new entry");
    Console.WriteLine("2. Display the journal");
    Console.WriteLine("3. Save the journal to a file (Text)");
    Console.WriteLine("4. Load the journal from a file");
    Console.WriteLine("5. Save the journal to a CSV file");
    Console.WriteLine("6. Exit");
    Console.Write("Select an option (1–6): ");
    choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            journal.WriteNewEntry();
            break;
        case "2":
            journal.DisplayJournal();
            break;
        case "3":
            Console.Write("Enter filename to save (e.g., journal.txt): ");
            journal.SaveToFile(Console.ReadLine());
            break;
        case "4":
            Console.Write("Enter filename to load (e.g., journal.txt): ");
            journal.LoadFromFile(Console.ReadLine());
            break;
        case "5":
            Console.Write("Enter filename to save as CSV (e.g., journal.csv): ");
            journal.SaveToCsv(Console.ReadLine());
            break;
        case "6":
            Console.WriteLine("👋 Goodbye!");
            break;
        default:
            Console.WriteLine("❗ Invalid choice. Please select between 1–6.");
            break;
    }
}

        Console.WriteLine("👋 Goodbye!");
    }
}