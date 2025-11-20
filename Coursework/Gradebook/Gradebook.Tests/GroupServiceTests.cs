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
    public class GroupServiceTests
    {
        private readonly Group _existingGroup = new Group { Id = 10, Name = "ІПЗ-25" };


        [Fact]
        public void DeleteGroup_StudentsExistInGroup_ThrowsValidationException()
        {
            const int groupId = 10;
            var mockGroupRepo = new Mock<IGroupRepository>();
            var mockStudentRepo = new Mock<IStudentRepository>();

            mockStudentRepo
                .Setup(repo => repo.GetAll())
                .Returns(new List<Student> { new Student { GroupId = groupId, Id = 1 } });

            var service = new GroupService(mockGroupRepo.Object, mockStudentRepo.Object);

            Assert.Throws<ValidationException>(() => service.DeleteGroup(groupId));
        }

        [Fact]
        public void CreateGroup_DuplicateNameExists_ThrowsValidationException()
        {
            var mockGroupRepo = new Mock<IGroupRepository>();
            var newGroup = new Group { Name = "ІПЗ-25" };

            mockGroupRepo
                .Setup(repo => repo.GetAll())
                .Returns(new List<Group> { _existingGroup });

            var service = new GroupService(mockGroupRepo.Object, new Mock<IStudentRepository>().Object);

            Assert.Throws<ValidationException>(() => service.CreateGroup(newGroup));
        }


        [Fact]
        public void DeleteGroup_NoStudentsExist_CallsRepositoryDelete()
        {
            const int groupId = 10;
            var mockGroupRepo = new Mock<IGroupRepository>();
            var mockStudentRepo = new Mock<IStudentRepository>();

            mockStudentRepo
                .Setup(repo => repo.GetAll())
                .Returns(Enumerable.Empty<Student>());

            var service = new GroupService(mockGroupRepo.Object, mockStudentRepo.Object);

            service.DeleteGroup(groupId);

            mockGroupRepo.Verify(repo => repo.Delete(groupId), Times.Once());
        }
    }
}