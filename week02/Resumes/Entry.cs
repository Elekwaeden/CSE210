using System;
using System.Collections.Generic;
using System.Text;

public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Mood { get; set; }
    public List<string> Tags { get; set; }

    public Entry(string prompt, string response, string mood = "", List<string> tags = null)
    {
        Date = DateTime.Now.ToString("yyyy-MM-dd");
        Prompt = prompt;
        Response = response;
        Mood = mood;
        Tags = tags ?? new List<string>();
    }

    public override string ToString()
    {
        string tagDisplay = Tags.Count > 0 ? string.Join(", ", Tags) : "None";
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\nMood: {Mood}\nTags: {tagDisplay}\n";
    }

    // For saving as a simple line (pipe-delimited)
    public string ToFileFormat()
    {
        string tagsAsString = string.Join(",", Tags);
        return $"{Date}|{Prompt}|{Response}|{Mood}|{tagsAsString}";
    }

    public static Entry FromFileFormat(string line)
    {
        string[] parts = line.Split('|');
        List<string> tags = parts.Length > 4 ? new List<string>(parts[4].Split(',')) : new List<string>();
        return new Entry(parts[1], parts[2], parts.Length > 3 ? parts[3] : "", tags)
        {
            Date = parts[0]
        };
    }

    // For exporting as CSV (safe for Excel)
    public string ToCsvFormat()
    {
        string cleanPrompt = Prompt.Replace("\"", "\"\"");
        string cleanResponse = Response.Replace("\"", "\"\"");
        string cleanMood = Mood.Replace("\"", "\"\"");
        string tagLine = string.Join(",", Tags).Replace("\"", "\"\"");

        return $"\"{Date}\",\"{cleanPrompt}\",\"{cleanResponse}\",\"{cleanMood}\",\"{tagLine}\"";
    }
}
