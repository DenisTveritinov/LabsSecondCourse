using System.Text.Json;
using Gradebook.BLL.Interfaces;
using Gradebook.Core;


namespace Gradebook.DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _filePath = "students.json";
        private List<Student> _students;

        public StudentRepository() 
        { 
            _students = LoadData(); 
        }

        private List<Student> LoadData()
        {
            if (!File.Exists(_filePath)) 
                return new List<Student>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Student>>(json);
        }

        private void SaveData()
        {
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true 
            };
            string json = JsonSerializer.Serialize(_students, options);
            File.WriteAllText(_filePath, json);
        }

        public void Create(Student student)
        {
            int newId = (_students.Count == 0) ? 1 : _students.Max(s => s.Id) + 1;
            student.Id = newId;
            _students.Add(student);
            SaveData();
        }

        public void Delete(int id)
        {
            var student = GetById(id);
            if (student != null) 
            { 
                _students.Remove(student); 
                SaveData(); 
            }
        }

        public IEnumerable<Student> GetAll() => _students;
        public Student GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

        public void Update(Student student)
        {
            var existingStudent = GetById(student.Id);
            if (existingStudent != null)
            {
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.GroupId = student.GroupId;
                SaveData();
            }
        }
    }
}
