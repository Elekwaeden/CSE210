public class ScriptureReference
{
    private string book;
    private int chapter;
    private int verseStart;
    private int? verseEnd;

    // Single verse
    public ScriptureReference(string book, int chapter, int verse)
    {
        this.book = book;
        this.chapter = chapter;
        this.verseStart = verse;
        this.verseEnd = null;
    }

    // Verse range
    public ScriptureReference(string book, int chapter, int verseStart, int verseEnd)
    {
        this.book = book;
        this.chapter = chapter;
        this.verseStart = verseStart;
        this.verseEnd = verseEnd;
    }

    public string GetDisplay()
    {
        if (verseEnd.HasValue)
            return $"{book} {chapter}:{verseStart}-{verseEnd.Value}";
        else
            return $"{book} {chapter}:{verseStart}";
    }
}
