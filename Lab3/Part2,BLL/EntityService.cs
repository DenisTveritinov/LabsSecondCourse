using System;
using System.Collections.Generic;
using System.Linq;
using Part2.BLL;
using Part2.DAL;

namespace Part2.BLL
{
    public class EntityService
    {
        private readonly EntityContext _context;
        private List<Student> _students;

        public EntityService()
        {
            _context = new EntityContext();
            _students = _context.LoadData();
        }

        public void AddStudent(Student student)
        {
            if (student.AverageGrade < 0 || student.AverageGrade > 100)
                throw new StudentServiceException("Середній бал має бути від 0 до 100.");

            if (student.Course < 1 || student.Course > 6)
                throw new StudentServiceException("Курс має бути від 1 до 6.");

            if (string.IsNullOrWhiteSpace(student.Surname))
                throw new StudentServiceException("Прізвище не може бути порожнім.");

            _students.Add(student);
        }

        public void SaveChanges()
        {
            _context.SaveData(_students);
        }

        public List<Student> GetAll()
        {
            return _students;
        }

        public List<Student> GetForeignExcellentFirstYearStudents()
        {

            var result = _students.Where(s =>
                s.Course == 1 &&
                s.AverageGrade >= 90 &&
                !s.Country.Equals("Украина", StringComparison.OrdinalIgnoreCase) &&
                !s.Country.Equals("Ukraine", StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (result.Count == 0)
            {

                throw new StudentServiceException("Студентів за даними критеріями не знайдено.");
            }

            return result;
        }
    }
}