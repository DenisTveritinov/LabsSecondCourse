using Gradebook.BLL.Exeptions;
using Gradebook.BLL.Interfaces;
using Gradebook.Core;

namespace Gradebook.BLL.Services
{
    public class GroupService
    {
        private readonly IGroupRepository _groupRepo;
        private readonly IStudentRepository _studentRepo;

        public GroupService(IGroupRepository groupRepo, IStudentRepository studentRepo)
        {
            _groupRepo = groupRepo;
            _studentRepo = studentRepo;
        }

        public void CreateGroup(Group group)
        {
            if (_groupRepo.GetAll().Any(g => g.Name == group.Name))
            {
                throw new ValidationException($"Група з іменем '{group.Name}' вже існує.");
            }
            _groupRepo.Create(group);
        }

        public IEnumerable<Group> GetAllGroups() => _groupRepo.GetAll();

        public void DeleteGroup(int groupId)
        {
            if (_studentRepo.GetAll().Any(s => s.GroupId == groupId))
            {
                throw new ValidationException($"Неможливо видалити групу ID {groupId}. В ній є студенти.");
            }
            _groupRepo.Delete(groupId);
        }

        public Group GetGroupById(int id) => _groupRepo.GetById(id);
    }
}
