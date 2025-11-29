using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using StudentProject.Models;

namespace StudentProject.DAL
{
    public class FileRepository
    {
        private readonly string _filePath = "students.json";

        public void SaveStudents(List<Student> students)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(students, options);
            File.WriteAllText(_filePath, jsonString);
        }

        public List<Student> LoadStudents()
        {
            if (!File.Exists(_filePath))
                return new List<Student>();

            string jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Student>>(jsonString);
        }
    }
}