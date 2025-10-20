using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application
{
    public class StudentServices : IStudentServices
    {
        public int CountExcellentForeigners(Student[] students)
        {
            int count = 0;

            foreach (var student in students)
            {
                bool isForeign = student.country != "Україна";
                bool isFirstCourse = student.course == 1;
                bool isExcellent = student.averageGrade >= 90;

                if (isForeign && isFirstCourse && isExcellent)
                    count++;
            }

            return count;
        }
    }
}
