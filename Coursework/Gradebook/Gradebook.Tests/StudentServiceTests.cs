using Xunit;
using Moq;
using Gradebook.BLL.Services;
using Gradebook.BLL.Interfaces;
using Gradebook.BLL.Exeptions;
using Gradebook.Core;
using System.Collections.Generic;
using System.Linq;

namespace Gradebook.Tests
{
    public class StudentServiceTests
    {
        private readonly Group _existingGroup = new Group { Id = 100, Name = "ІПЗ-25" };
        private readonly Student _existingStudent = new Student { Id = 5, FirstName = "Old", LastName = "Test", GroupId = 100 };

        [Fact]
        public void CreateStudent_ValidDataAndGroupExists_CallsRepositoryCreate()
        {
            var mockStudentRepo = new Mock<IStudentRepository>();
            var mockGroupRepo = new Mock<IGroupRepository>();
            var mockGradeRepo = new Mock<IGradeRepository>();

            mockGroupRepo.Setup(repo => repo.GetById(100)).Returns(_existingGroup);

            mockStudentRepo.Setup(repo => repo.GetAll()).Returns(Enumerable.Empty<Student>());

            var service = new StudentService(mockStudentRepo.Object, mockGroupRepo.Object, mockGradeRepo.Object);

            service.CreateStudent(_existingStudent);

            mockStudentRepo.Verify(repo => repo.Create(It.IsAny<Student>()), Times.Once());
        }


        [Fact]
        public void CreateStudent_GroupDoesNotExist_ThrowsValidationException()
        {
            var mockGroupRepo = new Mock<IGroupRepository>();

            mockGroupRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Group)null);

            var service = new StudentService(new Mock<IStudentRepository>().Object, mockGroupRepo.Object, new Mock<IGradeRepository>().Object);

            var invalidStudent = new Student { FirstName = "Invalid", GroupId = 999 };

            Assert.Throws<ValidationException>(() => service.CreateStudent(invalidStudent));
        }


        [Fact]
        public void CreateStudent_DuplicateExists_ThrowsValidationException()
        {
            var mockStudentRepo = new Mock<IStudentRepository>();
            var mockGroupRepo = new Mock<IGroupRepository>();

            mockStudentRepo.Setup(repo => repo.GetAll()).Returns(new List<Student> { _existingStudent });

            mockGroupRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(_existingGroup);

            var service = new StudentService(mockStudentRepo.Object, mockGroupRepo.Object, new Mock<IGradeRepository>().Object);

            Assert.Throws<ValidationException>(() => service.CreateStudent(_existingStudent));
        }


        [Fact]
        public void DeleteStudent_StudentExists_TriggersCascadingDelete()
        {
            const int studentId = 5;
            var mockStudentRepo = new Mock<IStudentRepository>();
            var mockGradeRepo = new Mock<IGradeRepository>();

            mockStudentRepo.Setup(repo => repo.GetById(studentId)).Returns(_existingStudent);

            var service = new StudentService(mockStudentRepo.Object, null, mockGradeRepo.Object);

            service.DeleteStudent(studentId);

            mockGradeRepo.Verify(repo => repo.DeleteByStudentId(studentId), Times.Once());

            mockStudentRepo.Verify(repo => repo.Delete(studentId), Times.Once());
        }


        [Fact]
        public void DeleteStudent_StudentDoesNotExist_ThrowsValidationException()
        {
            var mockStudentRepo = new Mock<IStudentRepository>();

            mockStudentRepo.Setup(repo => repo.GetById(999)).Returns((Student)null);

            var service = new StudentService(mockStudentRepo.Object, null, new Mock<IGradeRepository>().Object);

            Assert.Throws<ValidationException>(() => service.DeleteStudent(999));
        }


        [Fact]
        public void UpdateStudent_ValidData_CallsRepositoryUpdate()
        {
            var mockStudentRepo = new Mock<IStudentRepository>();
            var mockGroupRepo = new Mock<IGroupRepository>();

            mockGroupRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(_existingGroup);

            var service = new StudentService(mockStudentRepo.Object, mockGroupRepo.Object, new Mock<IGradeRepository>().Object);

            service.UpdateStudent(_existingStudent);

            mockStudentRepo.Verify(repo => repo.Update(It.IsAny<Student>()), Times.Once());
        }


        [Fact]
        public void UpdateStudent_GroupDoesNotExist_ThrowsValidationException()
        {
            var mockStudentRepo = new Mock<IStudentRepository>();
            var mockGroupRepo = new Mock<IGroupRepository>();

            mockGroupRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Group)null);

            var service = new StudentService(mockStudentRepo.Object, mockGroupRepo.Object, new Mock<IGradeRepository>().Object);

            Assert.Throws<ValidationException>(() => service.UpdateStudent(_existingStudent));
        }
    }
}