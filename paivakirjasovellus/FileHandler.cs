using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace paivakirjasovellus
{
    internal class FileHandler
    {
        private readonly string _kansio;
        private readonly string _tiedostoPolku;

        public FileHandler(string tiedostonNimi = "merkinnat.json")
        {
            _kansio = Path.Combine(AppContext.BaseDirectory, "Tallennukset");
            _tiedostoPolku = Path.Combine(_kansio, tiedostonNimi);

            if (!Directory.Exists(_kansio))
            {
                Directory.CreateDirectory(_kansio);
            }
        }

        public void TallennaMerkinnat(List<DiaryEntry> entries)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(entries, options);
            File.WriteAllText(_tiedostoPolku, json);
        }

        public List<DiaryEntry> LataaMerkinnat()
        {
            if (!File.Exists(_tiedostoPolku))
                return new List<DiaryEntry>();

            string json = File.ReadAllText(_tiedostoPolku);

            if (string.IsNullOrWhiteSpace(json))
                return new List<DiaryEntry>();

            var entries = JsonSerializer.Deserialize<List<DiaryEntry>>(json);
            return entries ?? new List<DiaryEntry>();
        }
    }
}