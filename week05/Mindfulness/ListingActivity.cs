using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.") {}

    public void Run()
    {
        DisplayStartingMessage();
        Random rand = new Random();
        int duration = GetDuration();

        Console.WriteLine($"\n{_prompts[rand.Next(_prompts.Count)]}");
        Console.WriteLine("You may begin listing in...");
        ShowCountdown(3);

        List<string> entries = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            entries.Add(Console.ReadLine());
        }

        Console.WriteLine($"\nYou listed {entries.Count} items.");
        DisplayEndingMessage();
    }
}
