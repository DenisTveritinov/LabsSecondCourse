using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Part2.DAL;

namespace Part2.DAL
{
    public class EntityContext
    {
        private const string FilePath = "students_data.json";

        public void SaveData(List<Student> students)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(students, options);
            File.WriteAllText(FilePath, json);
        }

        public List<Student> LoadData()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Student>();
            }

            string json = File.ReadAllText(FilePath);
            try
            {
                return JsonSerializer.Deserialize<List<Student>>(json);
            }
            catch
            {
                return new List<Student>();
            }
        }
    }
}