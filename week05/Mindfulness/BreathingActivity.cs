using System;

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly.\nClear your mind and focus on your breathing.") {}

    public void Run()
    {
        DisplayStartingMessage();
        int duration = GetDuration();
        int interval = 6;

        while (duration > 0)
        {
            Console.Write("\nBreathe in...");
            ShowCountdown(3);
            Console.Write("Breathe out...");
            ShowCountdown(3);
            duration -= interval;
        }

        DisplayEndingMessage();
    }
}
