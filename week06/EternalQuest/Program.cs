/*
  Notes on exceeding requirements (to include in Program.cs comment as requested):
  - Added NegativeGoal (penalty when recording) and ProgressGoal (accumulating units toward a target) as extra goal types.
  - Implemented a leveling system and titles that change with level.
  - Persistence via a simple pipe-separated text format to allow saving/loading of diverse goal types without complex JSON converters.
  - Encapsulation: private fields with public read-only properties where appropriate; behavior is encapsulated within classes.
  - Inheritance & Polymorphism: base Goal class with derived classes overriding RecordEvent, IsComplete, GetDetails, and Serialize.
*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Eternal Quest
// C# Console Application
// Fulfills requirements: SimpleGoal, EternalGoal, ChecklistGoal
// Exceeds requirements: NegativeGoal, ProgressGoal, Leveling system, custom titles, persistence, and clear comments.

abstract class Goal
{
    // encapsulated fields
    private string name;
    private string description;
    private int pointsPerEvent;

    protected Goal(string name, string description, int pointsPerEvent)
    {
        this.name = name;
        this.description = description;
        this.pointsPerEvent = pointsPerEvent;
    }

    public string Name => name;
    public string Description => description;
    public int PointsPerEvent => pointsPerEvent;

    // RecordEvent returns how many points were awarded (can be negative)
    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDetails();

    // For persistence - each derived class will provide a pipe-separated save line
    public abstract string Serialize();
    public static Goal Deserialize(string line)
    {
        // Format: Type|...fields...
        var parts = line.Split('|');
        var type = parts[0];
        try
        {
            switch (type)
            {
                case "Simple":
                    // Simple|name|description|points|completed
                    return new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]));
                case "Eternal":
                    // Eternal|name|description|points
                    return new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
                case "Checklist":
                    // Checklist|name|description|points|current|target|bonus
                    return new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[6]), int.Parse(parts[4]));
                case "Negative":
                    // Negative|name|description|points
                    return new NegativeGoal(parts[1], parts[2], int.Parse(parts[3]));
                case "Progress":
                    // Progress|name|description|pointsPerUnit|currentUnits|targetUnits
                    return new ProgressGoal(parts[1], parts[2], int.Parse(parts[3]), double.Parse(parts[4]), double.Parse(parts[5]));
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }
}

class SimpleGoal : Goal
{
    private bool completed;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        completed = false;
    }

    // used for deserialization convenience
    public SimpleGoal(string name, string description, int points, bool completed) : base(name, description, points)
    {
        this.completed = completed;
    }

    public override int RecordEvent()
    {
        if (!completed)
        {
            completed = true;
            return PointsPerEvent;
        }
        return 0;
    }

    public override bool IsComplete() => completed;

    public override string GetDetails()
    {
        return $"[{(completed ? 'X' : ' ')}] {Name} - {Description} (one-time rewards: {PointsPerEvent} pts)";
    }

    public override string Serialize()
    {
        return $"Simple|{Escape(Name)}|{Escape(Description)}|{PointsPerEvent}|{completed}";
    }

    private string Escape(string s) => s.Replace("|", "\\|");
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordEvent() => PointsPerEvent;
    public override bool IsComplete() => false;
    public override string GetDetails() => $"[∞] {Name} - {Description} (per event: {PointsPerEvent} pts)";
    public override string Serialize() => $"Eternal|{Escape(Name)}|{Escape(Description)}|{PointsPerEvent}";
    private string Escape(string s) => s.Replace("|", "\\|");
}

class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;
    private int bonusPoints;

    public ChecklistGoal(string name, string description, int pointsPerEvent, int targetCount, int bonusPoints)
        : base(name, description, pointsPerEvent)
    {
        this.targetCount = targetCount;
        this.currentCount = 0;
        this.bonusPoints = bonusPoints;
    }

    // deserialization helper: points param order adjusted in Deserialize
    public ChecklistGoal(string name, string description, int pointsPerEvent, int currentCount, int targetCount, int bonusPoints)
        : base(name, description, pointsPerEvent)
    {
        this.currentCount = currentCount;
        this.targetCount = targetCount;
        this.bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (IsComplete()) return 0;
        currentCount++;
        int total = PointsPerEvent;
        if (currentCount >= targetCount)
        {
            total += bonusPoints;
        }
        return total;
    }

    public override bool IsComplete() => currentCount >= targetCount;

    public override string GetDetails()
    {
        return $"[{(IsComplete() ? 'X' : ' ')}] {Name} - {Description} (Completed {currentCount}/{targetCount}) +{PointsPerEvent}/event";
    }

    public override string Serialize()
    {
        return $"Checklist|{Escape(Name)}|{Escape(Description)}|{PointsPerEvent}|{currentCount}|{targetCount}|{bonusPoints}";
    }

    private string Escape(string s) => s.Replace("|", "\\|");
}

// Extra creative: NegativeGoal (lose points)
class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int penaltyPoints) : base(name, description, penaltyPoints) { }

    public override int RecordEvent() => -Math.Abs(PointsPerEvent);
    public override bool IsComplete() => false;
    public override string GetDetails() => $"[!] {Name} - {Description} (negative: {PointsPerEvent} pts when recorded)";
    public override string Serialize() => $"Negative|{Escape(Name)}|{Escape(Description)}|{PointsPerEvent}";
    private string Escape(string s) => s.Replace("|", "\\|");
}

// Extra creative: ProgressGoal (accumulate units until target)
class ProgressGoal : Goal
{
    private double currentUnits;
    private double targetUnits;

    public ProgressGoal(string name, string description, int pointsPerUnit, double currentUnits, double targetUnits)
        : base(name, description, pointsPerUnit)
    {
        this.currentUnits = currentUnits;
        this.targetUnits = targetUnits;
    }

    public override int RecordEvent()
    {
        Console.Write($"Enter units progressed (e.g., km): ");
        if (double.TryParse(Console.ReadLine(), out double units))
        {
            currentUnits += units;
            int award = (int)Math.Round(units * PointsPerEvent);
            if (currentUnits >= targetUnits)
            {
                Console.WriteLine("Target reached! Bonus 500 pts awarded.");
                award += 500;
            }
            return award;
        }
        Console.WriteLine("Invalid units entered. No points awarded.");
        return 0;
    }

    public override bool IsComplete() => currentUnits >= targetUnits;

    public override string GetDetails() => $"[{(IsComplete() ? 'X' : ' ')}] {Name} - {Description} (Progress {currentUnits}/{targetUnits})";

    public override string Serialize() => $"Progress|{Escape(Name)}|{Escape(Description)}|{PointsPerEvent}|{currentUnits}|{targetUnits}";
    private string Escape(string s) => s.Replace("|", "\\|");
}

class QuestLog
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public int Score => score;

    public void AddGoal(Goal g) => goals.Add(g);

    public void ListGoals()
    {
        Console.WriteLine("\n--- Your Goals ---");
        if (!goals.Any()) Console.WriteLine("(No goals yet)");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetDetails()}");
        }
    }

    public void RecordGoalEvent(int index)
    {
        if (index < 0 || index >= goals.Count) { Console.WriteLine("Invalid goal index."); return; }
        var g = goals[index];
        int awarded = g.RecordEvent();
        score += awarded;
        Console.WriteLine($"You received {awarded} points. Total score: {score}");
    }

    public void Save(string filename)
    {
        using (var w = new StreamWriter(filename))
        {
            w.WriteLine(score);
            foreach (var g in goals)
            {
                w.WriteLine(g.Serialize());
            }
        }
        Console.WriteLine($"Saved to {filename}");
    }

    public void Load(string filename)
    {
        if (!File.Exists(filename)) { Console.WriteLine("File not found."); return; }
        var lines = File.ReadAllLines(filename);
        if (lines.Length == 0) { Console.WriteLine("File empty."); return; }
        goals.Clear();
        if (int.TryParse(lines[0], out int loadedScore)) score = loadedScore; else score = 0;
        for (int i = 1; i < lines.Length; i++)
        {
            var g = Goal.Deserialize(lines[i]);
            if (g != null) goals.Add(g);
        }
        Console.WriteLine($"Loaded {goals.Count} goals. Score: {score}");
    }

    public int Level => Math.Max(1, (Score / 1000) + 1);

    public string Title
    {
        get
        {
            var lvl = Level;
            if (lvl < 3) return "Apprentice";
            if (lvl < 6) return "Adept";
            if (lvl < 10) return "Champion";
            return "Legendary Ninja Unicorn";
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var log = new QuestLog();
        Console.WriteLine("Welcome to Eternal Quest — track your sacred and mundane goals with gamification.");

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== Menu ===");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. List goals");
            Console.WriteLine("3. Record an event");
            Console.WriteLine("4. Show score & level");
            Console.WriteLine("5. Save goals");
            Console.WriteLine("6. Load goals");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");
            var opt = Console.ReadLine();
            switch (opt)
            {
                case "1": CreateGoalMenu(log); break;
                case "2": log.ListGoals(); break;
                case "3":
                    log.ListGoals();
                    Console.Write("Choose goal number to record: ");
                    if (int.TryParse(Console.ReadLine(), out int idx)) log.RecordGoalEvent(idx - 1);
                    else Console.WriteLine("Invalid number.");
                    break;
                case "4":
                    Console.WriteLine($"Score: {log.Score} pts | Level: {log.Level} | Title: {log.Title}");
                    break;
                case "5":
                    Console.Write("Enter filename to save (e.g., save.txt): ");
                    var sf = Console.ReadLine();
                    log.Save(sf);
                    break;
                case "6":
                    Console.Write("Enter filename to load (e.g., save.txt): ");
                    var lf = Console.ReadLine();
                    log.Load(lf);
                    break;
                case "7": running = false; break;
                default: Console.WriteLine("Unknown option."); break;
            }
        }

        Console.WriteLine("Goodbye — may your quest be blessed.");
    }

    static void CreateGoalMenu(QuestLog log)
    {
        Console.WriteLine("\n-- Create Goal --");
        Console.WriteLine("Types: 1) Simple 2) Eternal 3) Checklist 4) Negative 5) Progress");
        Console.Write("Choose type: ");
        var t = Console.ReadLine();
        Console.Write("Name: "); var name = Console.ReadLine();
        Console.Write("Description: "); var desc = Console.ReadLine();
        switch (t)
        {
            case "1":
                Console.Write("Points awarded when completed: ");
                if (int.TryParse(Console.ReadLine(), out int p1)) log.AddGoal(new SimpleGoal(name, desc, p1));
                break;
            case "2":
                Console.Write("Points per recording: ");
                if (int.TryParse(Console.ReadLine(), out int p2)) log.AddGoal(new EternalGoal(name, desc, p2));
                break;
            case "3":
                Console.Write("Points per recording: "); int.TryParse(Console.ReadLine(), out int pc);
                Console.Write("Target count to complete: "); int.TryParse(Console.ReadLine(), out int target);
                Console.Write("Bonus on completion: "); int.TryParse(Console.ReadLine(), out int bonus);
                log.AddGoal(new ChecklistGoal(name, desc, pc, target, bonus));
                break;
            case "4":
                Console.Write("Penalty points (positive number): "); if (int.TryParse(Console.ReadLine(), out int pen)) log.AddGoal(new NegativeGoal(name, desc, pen));
                break;
            case "5":
                Console.Write("Points per unit (e.g., points per km): "); int.TryParse(Console.ReadLine(), out int punit);
                Console.Write("Target units to reach: "); double.TryParse(Console.ReadLine(), out double targetUnits);
                log.AddGoal(new ProgressGoal(name, desc, punit, 0, targetUnits));
                break;
            default:
                Console.WriteLine("Unknown type."); break;
        }

        Console.WriteLine("Goal created.");
    }
}


