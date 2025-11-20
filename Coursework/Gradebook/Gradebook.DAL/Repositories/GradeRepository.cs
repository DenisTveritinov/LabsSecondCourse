using Gradebook.BLL.Interfaces;
using Gradebook.Core;
using System.Text.Json;

namespace Gradebook.DAL.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly string _filePath = "grades.json";
        private List<Grade> _grades;

        public GradeRepository() 
        { 
            _grades = LoadData(); 
        }

        private List<Grade> LoadData()
        {
            if (!File.Exists(_filePath)) 
                return new List<Grade>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Grade>>(json);
        }

        //CRUD
        private void SaveData()
        {
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true 
            };
            string json = JsonSerializer.Serialize(_grades, options);
            File.WriteAllText(_filePath, json);
        }

        public void Create(Grade grade)
        {
            int newId = (_grades.Count == 0) ? 1 : _grades.Max(g => g.Id) + 1;
            grade.Id = newId;
            _grades.Add(grade);
            SaveData();
        }

        public void Delete(int id)
        {
            var grade = GetById(id);
            if (grade != null) 
            { 
                _grades.Remove(grade); 
                SaveData(); 
            }
        }

        public void Update(Grade grade)
        {
            var existingGrade = GetById(grade.Id);
            if (existingGrade != null)
            {
                existingGrade.Mark = grade.Mark;
                existingGrade.Date = grade.Date;
                SaveData();
            }
        }

        public void DeleteByStudentId(int studentId)
        {
            _grades.RemoveAll(g => g.StudentId == studentId);

            SaveData();
        }

        public IEnumerable<Grade> GetAll() => _grades;

        public Grade GetById(int id) => _grades.FirstOrDefault(g => g.Id == id);

        public IEnumerable<Grade> GetGradesByStudent(int studentId)
        {
            return _grades.Where(g => g.StudentId == studentId);
        }

        

        
    }
}
