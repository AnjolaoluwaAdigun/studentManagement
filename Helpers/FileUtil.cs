using System.Text.Json;

namespace StudentManagement.Helpers
{
    public static class FileUtil
    {
        private static readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");

        public static List<T> ReadFromFile<T>(string fileName)
        {
            var filePath = Path.Combine(_path, fileName);

            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(_path);
                File.WriteAllText(filePath, "[]");
                return new List<T>();
            }

            var json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<T>();

            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public static void SaveToFile<T>(List<T> data, string fileName)
        {
            Directory.CreateDirectory(_path);
            var filePath = Path.Combine(_path, fileName);
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
