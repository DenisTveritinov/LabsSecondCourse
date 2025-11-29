using Xunit;
using System.Collections.Generic;
using StudentProject.BLL;
using StudentProject.Models;

namespace StudentTests
{
    public class StudentServiceTests
    {
        [Fact]
        public void GetExcellentForeignFreshmen_ShouldReturnOnlyMatchingStudents()
        {
            var service = new StudentService();
            var list = new List<Student>
            {
                new Student { Surname = "Valid1", Course = 1, Country = "Poland", AverageGrade = 95 },
                new Student { Surname = "Local", Course = 1, Country = "Ukraine", AverageGrade = 95 },
                new Student { Surname = "BadGrade", Course = 1, Country = "Poland", AverageGrade = 60 },
                new Student { Surname = "Old", Course = 2, Country = "Poland", AverageGrade = 95 }
            };

            var result = service.GetExcellentForeignFreshmen(list);

            Assert.Equal(1, result.Count);
            Assert.Equal("Valid1", result[0].Surname);
        }

        [Fact]
        public void CheckCitizenshipEligibility_MoreThan5Years_ReturnsTrue()
        {
            var service = new StudentService();
            var student = new Student { YearsInUkraine = 6 };

            bool result = service.CheckCitizenshipEligibility(student);

            Assert.True(result);
        }

        [Fact]
        public void CheckCitizenshipEligibility_Exactly5Years_ReturnsTrue()
        {
            var service = new StudentService();
            var student = new Student { YearsInUkraine = 5 };

            bool result = service.CheckCitizenshipEligibility(student);

            Assert.True(result);
        }

        [Fact]
        public void CheckCitizenshipEligibility_LessThan5Years_ReturnsFalse()
        {
            var service = new StudentService();
            var student = new Student { YearsInUkraine = 4 };

            bool result = service.CheckCitizenshipEligibility(student);

            Assert.False(result);
        }
    }
}