public class DiaryEntry
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public DiaryEntry(int id, string title, string content)
    {
        Id = id;
        Title = title;
        Content = content;
        Date = DateTime.Now;
    }

    public override string ToString()
    {
        return $"[{Id}] {Date:dd.MM.yyyy} - {Title}";
    }
}