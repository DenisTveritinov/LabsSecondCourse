using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace DataAccess
{
    public interface IFileServices
    {
            void saveStudent(Student student, string path);
            Student[] readStudents(string path);

    }
}
