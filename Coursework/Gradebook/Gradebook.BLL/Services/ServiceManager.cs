using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradebook.BLL.Services
{
    public class ServiceManager
    {
        public StudentService StudentService { get; }
        public GroupService GroupService { get; }
        public SubjectService SubjectService { get; }
        public GradeService GradeService { get; }

        public ServiceManager(StudentService studentService, GroupService groupService, SubjectService subjectService, GradeService gradeService)
        {
            StudentService = studentService;
            GroupService = groupService;
            SubjectService = subjectService;
            GradeService = gradeService;
        }
    }
}
