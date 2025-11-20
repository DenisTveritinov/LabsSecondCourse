using Gradebook.BLL.Interfaces;
using Gradebook.BLL.Services;
using Gradebook.DAL.Repositories;

namespace Gradebook.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;


            IStudentRepository studentRepo = new StudentRepository();
            IGroupRepository groupRepo = new GroupRepository();
            ISubjectRepository subjectRepo = new SubjectRepository();
            IGradeRepository gradeRepo = new GradeRepository();


            StudentService studentService = new StudentService(studentRepo, groupRepo, gradeRepo);

            GroupService groupService = new GroupService(groupRepo, studentRepo);

            SubjectService subjectService = new SubjectService(subjectRepo, gradeRepo);

            GradeService gradeService = new GradeService(gradeRepo, studentRepo, subjectRepo);

            ServiceManager manager = new ServiceManager(studentService, groupService, subjectService, gradeService);

            ApplicationUI ui = new ApplicationUI(manager);

            ui.Run();
        }
    }
}
