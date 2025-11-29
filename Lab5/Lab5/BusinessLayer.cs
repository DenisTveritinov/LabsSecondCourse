using System.Collections.Generic;
using System.Linq;
using StudentProject.Models;

namespace StudentProject.BLL
{
    public class StudentService
    {
        public List<Student> GetExcellentForeignFreshmen(List<Student> students)
        {
            return students
                .Where(s => s.Course == 1)
                .Where(s => s.Country != "Ukraine")
                .Where(s => s.AverageGrade >= 90)
                .ToList();
        }

        public bool CheckCitizenshipEligibility(Student student)
        {
            return student.YearsInUkraine >= 5;
        }
    }
}