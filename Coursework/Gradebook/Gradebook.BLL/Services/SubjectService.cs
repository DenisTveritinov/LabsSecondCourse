using Gradebook.BLL.Exeptions;
using Gradebook.BLL.Interfaces;
using Gradebook.Core;

namespace Gradebook.BLL.Services
{
    public class SubjectService
    {
        private readonly ISubjectRepository _subjectRepo;
        private readonly IGradeRepository _gradeRepo; 

        public SubjectService(ISubjectRepository subjectRepo, IGradeRepository gradeRepo)
        {
            _subjectRepo = subjectRepo;
            _gradeRepo = gradeRepo;
        }

        public void CreateSubject(Subject subject)
        {
            if (_subjectRepo.GetAll().Any(s => s.Name == subject.Name))
            {
                throw new ValidationException($"Предмет '{subject.Name}' вже існує.");
            }
            _subjectRepo.Create(subject);
        }

        public IEnumerable<Subject> GetAllSubjects() => _subjectRepo.GetAll();

        public void DeleteSubject(int subjectId)
        {
            if (_gradeRepo.GetAll().Any(g => g.SubjectId == subjectId))
            {
                throw new ValidationException("Неможливо видалити предмет, по ньому вже виставлені оцінки.");
            }
            _subjectRepo.Delete(subjectId);
        }
    }
}
