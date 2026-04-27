using System;
using System.Collections.Generic;

public class DiaryManager
{
    private List<DiaryEntry> entries = new List<DiaryEntry>();

    public void AddEntry()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n─── Uusi merkintä ───────────────────────");
        Console.ResetColor();

        Console.Write("\n  Otsikko: ");
        string title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  Virhe: otsikko ei voi olla tyhjä!");
            Console.ResetColor();
            return;
        }

        Console.Write("  Sisältö: ");
        string content = Console.ReadLine();

        int newId = entries.Count + 1;
        DiaryEntry entry = new DiaryEntry(newId, title, content);
        entries.Add(entry);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n  ✓ Merkintä lisätty! ({entry.Date:dd.MM.yyyy})");
        Console.ResetColor();
    }

    public void ShowAllEntries()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n─── Kaikki merkinnät ────────────────────");
        Console.ResetColor();

        if (entries.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Ei merkintöjä vielä.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine();
        foreach (DiaryEntry entry in entries)
        {
            Console.WriteLine($"  {entry}");
        }
    }

    public int GetCount()
    {
        return entries.Count;
    }
}