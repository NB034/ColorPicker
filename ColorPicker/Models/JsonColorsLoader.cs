using System.IO;
using System.Text.Json;
using System.Windows.Media;

namespace ColorPicker.Models
{
    public class JsonColorsLoader : IColorsLoader
    {
        public readonly string defaultFileName = "Colors.json";
        public string FileName { get; set; }

        public JsonColorsLoader()
        {
            FileName = defaultFileName;
        }

        public Color[] Load()
        {
            using (FileStream stream = new FileStream(FileName, FileMode.OpenOrCreate))
            using (TextReader reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();
                Color[]? colors = null;
                if(jsonString.Length != 0)
                {
                    colors = JsonSerializer.Deserialize<Color[]>(jsonString);
                }
                return colors ?? System.Array.Empty<Color>();
            }
        }

        public void Save(Color[] brushes)
        {
            string jsonString = JsonSerializer.Serialize(brushes);
            File.WriteAllText(FileName, jsonString);
        }
    }
}
