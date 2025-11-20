using Gradebook.BLL.Interfaces;
using Gradebook.Core;
using System.Text.Json;

namespace Gradebook.DAL.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly string _filePath = "groups.json";
        private List<Gradebook.Core.Group> _groups;

        public GroupRepository() 
        { 
            _groups = LoadData(); 
        }

        private List<Gradebook.Core.Group> LoadData()
        {
            if (!File.Exists(_filePath))
                return new List<Gradebook.Core.Group>();

            string json = File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<List<Gradebook.Core.Group>>(json);
        }

        private void SaveData()
        {
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true 
            };
            string json = JsonSerializer.Serialize(_groups, options);
            File.WriteAllText(_filePath, json);
        }

        public void Create(Gradebook.Core.Group group)
        {
            int newId = (_groups.Count == 0) ? 1 : _groups.Max(g => g.Id) + 1;
            group.Id = newId;
            _groups.Add(group);
            SaveData();
        }

        public void Update(Gradebook.Core.Group group)
        {
            var existingStudent = GetById(group.Id);
            if (existingStudent != null)
            {
                existingStudent.Id = group.Id;
                existingStudent.Name = group.Name;
                SaveData();
            }
        }

        public void Delete(int id)
        {
            var group = GetById(id);
            if (group != null)
            {
                _groups.Remove(group);
                SaveData();
            }
        }

        public IEnumerable<Gradebook.Core.Group> GetAll() => _groups;

        public Gradebook.Core.Group GetById(int id) => _groups.FirstOrDefault(g => g.Id == id);
    }
}