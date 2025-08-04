using System;

class Program
{
    static void Main(string[] args)
    {
        // Base Assignment test
        Assignment assignment = new Assignment("Eden", "Chemical Reactions");
        Console.WriteLine(assignment.GetSummary());

        // MathAssignment test
        MathAssignment mathAssignment = new MathAssignment("Eden", "Algebra", "5.3", "1-10, 15");
        Console.WriteLine(mathAssignment.GetSummary());
        Console.WriteLine(mathAssignment.GetHomeworkList());

        // WritingAssignment test
        WritingAssignment writingAssignment = new WritingAssignment("Eden", "Literature", "The Role of Symbolism in Poetry");
        Console.WriteLine(writingAssignment.GetSummary());
        Console.WriteLine(writingAssignment.GetWritingInformation());
    }
}

