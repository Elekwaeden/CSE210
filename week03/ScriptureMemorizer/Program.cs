using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

// ================================================================
// Enhancements Report - Exceeding Core Requirements
//
// 1. **Scripture Library**:
//    Instead of using a single hardcoded scripture, this program includes 
//    a list of scriptures and randomly selects one at runtime. 
//    This mimics a "scripture library" to help users practice different verses.
//
// 2. **Selective Hiding**:
//    The program intelligently hides only words that are currently visible,
//    rather than randomly selecting already-hidden words. This ensures smoother 
//    and more purposeful progression in memorization.
//
// 3. **Verse Range Support**:
//    The ScriptureReference class supports both single verses 
//    (e.g., John 3:16) and verse ranges (e.g., Proverbs 3:5-6).
//
// 4. **Final Message**:
//    When all words are hidden, the program provides an encouraging message,
//    adding a friendly finish to the experience.
//
// These features together enhance the utility, user experience, and functionality
// of the scripture memorization tool.
// ================================================================

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptureLibrary = new List<Scripture>
        {
            new Scripture(new ScriptureReference("John", 3, 16),
                "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),

            new Scripture(new ScriptureReference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."),

            new Scripture(new ScriptureReference("Philippians", 4, 13),
                "I can do all this through him who gives me strength."),

            new Scripture(new ScriptureReference("Romans", 5, 11), 
                "And not only so, but we also joy in God through our Lord Jesus Christ, by whom we have now received the atonement."),






        };

        Random rand = new Random();
        Scripture scripture = scriptureLibrary[rand.Next(scriptureLibrary.Count)];

        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit:");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit") break;

            scripture.HideRandomWords(3);
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nAll words are now hidden. Great job memorizing!");
    }
}
