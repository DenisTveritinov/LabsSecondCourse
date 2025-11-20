using Xunit;
using Moq;
using Gradebook.BLL.Services;
using Gradebook.BLL.Interfaces;
using Gradebook.BLL.Exeptions;
using Gradebook.Core;
using System.Linq;

namespace Gradebook.Tests
{
    public class SubjectServiceTests
    {


        [Fact]
        public void DeleteSubject_GradesExist_ThrowsValidationException()
        {
            const int subjectId = 20;
            var mockSubjectRepo = new Mock<ISubjectRepository>();
            var mockGradeRepo = new Mock<IGradeRepository>();

            mockGradeRepo
                .Setup(repo => repo.GetAll())
                .Returns(new List<Grade> { new Grade { SubjectId = subjectId } });

            var service = new SubjectService(mockSubjectRepo.Object, mockGradeRepo.Object);

            Assert.Throws<ValidationException>(() => service.DeleteSubject(subjectId));
        }


        [Fact]
        public void CreateSubject_UniqueName_CallsRepositoryCreate()
        {
            var mockSubjectRepo = new Mock<ISubjectRepository>();
            var newSubject = new Subject { Name = "Новий Предмет" };

            mockSubjectRepo.Setup(repo => repo.GetAll()).Returns(Enumerable.Empty<Subject>());

            var service = new SubjectService(mockSubjectRepo.Object, new Mock<IGradeRepository>().Object);

            service.CreateSubject(newSubject);

            mockSubjectRepo.Verify(repo => repo.Create(It.IsAny<Subject>()), Times.Once());
        }
    }
}