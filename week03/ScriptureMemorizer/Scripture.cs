using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private ScriptureReference reference;
    private List<Word> words;

    public Scripture(ScriptureReference reference, string text)
    {
        this.reference = reference;
        this.words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideRandomWords(int count)
    {
        Random rand = new Random();
        var visibleWords = words.Where(w => !w.IsHidden).ToList();

        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = rand.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool AllWordsHidden()
    {
        return words.All(w => w.IsHidden);
    }

    public string GetDisplayText()
    {
        return reference.GetDisplay() + " " + string.Join(" ", words.Select(w => w.GetDisplay()));
    }
}
