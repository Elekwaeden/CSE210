public class Word
{
    private string text;
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        this.text = text;
        this.IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public string GetDisplay()
    {
        return IsHidden ? new string('_', text.Length) : text;
    }
}
