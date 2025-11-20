
using Gradebook.BLL.Interfaces;
using Gradebook.Core;
using System.Text.Json;

namespace Gradebook.DAL.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly string _filePath = "subjects.json";
        private List<Subject> _subjects;

        public SubjectRepository() { _subjects = LoadData(); }

        private List<Subject> LoadData()
        {
            if (!File.Exists(_filePath)) return new List<Subject>();
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Subject>>(json);
        }

        private void SaveData()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_subjects, options);
            File.WriteAllText(_filePath, json);
        }

        public void Create(Subject subject)
        {
            int newId = (_subjects.Count == 0) ? 1 : _subjects.Max(s => s.Id) + 1;
            subject.Id = newId;
            _subjects.Add(subject);
            SaveData();
        }

        public void Delete(int id)
        {
            var subject = GetById(id);
            if (subject != null) 
            { 
                _subjects.Remove(subject);
                SaveData();
            }
        }

        public IEnumerable<Subject> GetAll() => _subjects;

        public Subject GetById(int id) => _subjects.FirstOrDefault(s => s.Id == id);

        public void Update(Subject subject)
        {
            var existingSubject = GetById(subject.Id);
            if (existingSubject != null)
            {
                existingSubject.Name = subject.Name;
                SaveData();
            }
        }
    }
}
