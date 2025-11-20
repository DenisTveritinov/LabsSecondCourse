using System;
using Gradebook.BLL.Exeptions;
using Gradebook.BLL.Interfaces;
using Gradebook.Core;

namespace Gradebook.BLL.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly IGradeRepository _gradeRepo;

        public StudentService(IStudentRepository studentRepo, IGroupRepository groupRepo, IGradeRepository gradeRepo)
        {
            _studentRepo = studentRepo;
            _groupRepo = groupRepo;
            _gradeRepo = gradeRepo;
        }

        public void CreateStudent(Student student)
        {
            if (_groupRepo.GetById(student.GroupId) == null)
            {
                throw new ValidationException($"Група з ID {student.GroupId} не знайдена.");
            }
            if (_studentRepo.GetAll().Any(s => s.FirstName == student.FirstName && s.LastName == student.LastName))
            {
                throw new ValidationException($"Студент {student.FirstName} {student.LastName} вже існує.");
            }

            _studentRepo.Create(student);
        }

        public void DeleteStudent(int id)
        {
            var studentToDelete = _studentRepo.GetById(id);

            if (studentToDelete == null)
            {
                throw new ValidationException($"Студент з ID {id} не знайдений. Видалення неможливе.");
            }
            _gradeRepo.DeleteByStudentId(id);

            _studentRepo.Delete(id);
        }

        public void UpdateStudent(Student student)
        {
            if (_groupRepo.GetById(student.GroupId) == null)
            {
                throw new ValidationException($"Група з ID {student.GroupId} не знайдена. Оновлення неможливе.");
            }

            _studentRepo.Update(student);
        }

        public Student GetStudentById(int id)
        {
            return _studentRepo.GetById(id);
        }

        public IEnumerable<Student> GetStudentsByGroup(int groupId)
        {
            var allStudents = _studentRepo.GetAll();

            return allStudents.Where(s => s.GroupId == groupId);
        }

        public IEnumerable<Student> GetAllStudents() => _studentRepo.GetAll();

        public IEnumerable<Student> SearchStudents(string query)
        {
            query = query.ToLower();
            return _studentRepo.GetAll()
                .Where(s => s.FirstName.ToLower().Contains(query) || s.LastName.ToLower().Contains(query));
        }
    }
}
