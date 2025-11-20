
using Gradebook.BLL.Exeptions;
using Gradebook.BLL.Interfaces;
using Gradebook.Core;

namespace Gradebook.BLL.Services
{
    public class GradeService
    {
        private readonly IGradeRepository _gradeRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly ISubjectRepository _subjectRepo;

        public GradeService(IGradeRepository gradeRepo, IStudentRepository studentRepo, ISubjectRepository subjectRepo)
        {
            _gradeRepo = gradeRepo;
            _studentRepo = studentRepo;
            _subjectRepo = subjectRepo;
        }

        public void AddGrade(Grade grade)
        {
            if (_studentRepo.GetById(grade.StudentId) == null)
                throw new ValidationException("Студент для виставлення оцінки не знайдений.");
            if (_subjectRepo.GetById(grade.SubjectId) == null)
                throw new ValidationException("Предмет для виставлення оцінки не знайдений.");
            if (grade.Mark < 0 || grade.Mark > 100)
                throw new ValidationException("Оцінка має бути у діапазоні від 0 до 100.");

            _gradeRepo.Create(grade);
        }

        public double GetAverageMarkForStudent(int studentId)
        {
            var grades = _gradeRepo.GetGradesByStudent(studentId);

            if (!grades.Any())
            {
                return 0.0;
            }

            return grades.Average(g => g.Mark);
        }

        public IEnumerable<Grade> GetGradesForStudent(int studentId)
        {
            return _gradeRepo.GetGradesByStudent(studentId);
        }
    }
}
