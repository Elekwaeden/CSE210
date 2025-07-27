using System;
using System.Collections.Generic;

// Comment class
public class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string name, string text)
    {
        CommenterName = name;
        CommentText = text;
    }
}

// Video class
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }

    private List<Comment> _comments = new List<Comment>();

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public void DisplayVideoDetails()
    {
        Console.WriteLine($"\nTitle: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");

        foreach (var comment in _comments)
        {
            Console.WriteLine($"  - {comment.CommenterName}: {comment.CommentText}");
        }
    }
}

// Program entry point
public class Program
{
    public static void Main()
    {
        // Create videos
        Video video1 = new Video { Title = "How to Cook Jollof Rice", Author = "Naija Kitchen", LengthInSeconds = 420 };
        video1.AddComment(new Comment("Ada", "This looks so tasty!"));
        video1.AddComment(new Comment("Tunde", "Thanks for the recipe."));
        video1.AddComment(new Comment("Chika", "I tried it and my family loved it!"));

        Video video2 = new Video { Title = "Python Basics Tutorial", Author = "CodeWithEden", LengthInSeconds = 900 };
        video2.AddComment(new Comment("John", "Very clear explanation, thank you."));
        video2.AddComment(new Comment("Maya", "This helped me so much."));
        video2.AddComment(new Comment("Obi", "Subscribed!"));

        Video video3 = new Video { Title = "Lagos Vlog: Day in My Life", Author = "TravelNaija", LengthInSeconds = 600 };
        video3.AddComment(new Comment("Zainab", "Love how real this is."));
        video3.AddComment(new Comment("Chris", "Great editing!"));
        video3.AddComment(new Comment("Ella", "I miss Lagos 😢"));

        Video video4 = new Video { Title = "Learn Guitar in 10 Minutes", Author = "MusicZone", LengthInSeconds = 300 };
        video4.AddComment(new Comment("Sam", "That chord transition tip was gold."));
        video4.AddComment(new Comment("Lola", "Perfect for beginners."));
        video4.AddComment(new Comment("Alex", "Now I can finally play a song!"));

        // Put videos into a list
        List<Video> videos = new List<Video> { video1, video2, video3, video4 };

        // Display details of each video
        Console.WriteLine("=== YouTube Video Tracker ===");
        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
        }
    }
}
