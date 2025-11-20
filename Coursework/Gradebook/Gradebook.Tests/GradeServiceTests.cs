using Xunit;
using Moq;
using Gradebook.BLL.Services;
using Gradebook.BLL.Interfaces;
using Gradebook.BLL.Exeptions;
using Gradebook.Core;
using System.Linq;

namespace Gradebook.Tests
{
    public class GradeServiceTests
    {
        private readonly Student _existingStudent = new Student { Id = 1, FirstName = "A", GroupId = 1 };
        private readonly Subject _existingSubject = new Subject { Id = 1, Name = "Math" };


        [Fact]
        public void GetAverageMarkForStudent_CalculatesAverageCorrectly()
        {
            const int studentId = 1;
            var mockGradeRepo = new Mock<IGradeRepository>();

            mockGradeRepo
                .Setup(repo => repo.GetGradesByStudent(studentId))
                .Returns(new List<Grade>
                {
                    new Grade { Mark = 80 },
                    new Grade { Mark = 90 },
                    new Grade { Mark = 100 }
                });

            var service = new GradeService(mockGradeRepo.Object, null, null);

            var actualAverage = service.GetAverageMarkForStudent(studentId);


            Assert.Equal(90.0, actualAverage);
        }



        [Fact]
        public void AddGrade_StudentDoesNotExist_ThrowsValidationException()
        {
            var mockStudentRepo = new Mock<IStudentRepository>();
            var mockSubjectRepo = new Mock<ISubjectRepository>();
            var newGrade = new Grade { StudentId = 999, SubjectId = 1 };

            mockStudentRepo.Setup(repo => repo.GetById(999)).Returns((Student)null);
            mockSubjectRepo.Setup(repo => repo.GetById(1)).Returns(_existingSubject);

            var service = new GradeService(new Mock<IGradeRepository>().Object,
                                            mockStudentRepo.Object,
                                            mockSubjectRepo.Object);

            Assert.Throws<ValidationException>(() => service.AddGrade(newGrade));
        }
    }
}