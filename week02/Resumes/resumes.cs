using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private List<string> _prompts = new List<string>
{
    "Who was the most interesting person I interacted with today?",
    "What was the best part of my day?",
    "How did I see the hand of the Lord in my life today?",
    "What was the strongest emotion I felt today?",
    "If I had one thing I could do over today, what would it be?",
    "What am I most grateful for right now?",
    "What challenge did I face today, and how did I respond?"
};

public void SaveToCsv(string filename)
{
    using (StreamWriter writer = new StreamWriter(filename))
    {
        writer.WriteLine("Date,Prompt,Response,Mood,Tags"); // header
        foreach (Entry entry in _entries)
        {
            writer.WriteLine(entry.ToCsvFormat());
        }
    }

    Console.WriteLine($"🧾 CSV journal saved to \"{filename}\"");
}

    public void WriteNewEntry()
{
    Random random = new Random();
    string prompt = _prompts[random.Next(_prompts.Count)];

    Console.WriteLine($"\n📝 Prompt: {prompt}");
    Console.Write("Your response: ");
    string response = Console.ReadLine();

    Console.Write("How are you feeling today? (e.g., Happy, Grateful, Tired): ");
    string mood = Console.ReadLine();

    Console.Write("Add some tags (separate with commas, e.g., family,gratitude,faith): ");
    string tagInput = Console.ReadLine();
    List<string> tags = new List<string>(tagInput.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));

    _entries.Add(new Entry(prompt, response, mood, tags));
    Console.WriteLine("✅ Entry added with mood and tags!\n");
}


    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No journal entries to display.");
            return;
        }

        Console.WriteLine("\n--- Journal Entries ---\n");
        foreach (Entry entry in _entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.ToFileFormat());
            }
        }
        Console.WriteLine($"💾 Journal saved to \"{filename}\"");
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("❌ File not found.");
            return;
        }

        _entries.Clear();
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            _entries.Add(Entry.FromFileFormat(line));
        }

        Console.WriteLine($"📂 Journal loaded from \"{filename}\"");
    }
}
