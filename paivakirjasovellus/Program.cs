DiaryEntry entry = new DiaryEntry(1, "Testiotsikko", "Testisisältö");
Console.WriteLine(entry);
Console.ReadLine();
DiaryManager manager = new DiaryManager();
manager.AddEntry();
manager.ShowAllEntries();
Console.ReadLine();