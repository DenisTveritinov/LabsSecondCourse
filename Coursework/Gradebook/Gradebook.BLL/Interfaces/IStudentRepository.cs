using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gradebook.Core;

namespace Gradebook.BLL.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student GetById(int id);
        void Create(Student student);
        void Update(Student student);
        void Delete(int id);
    }
}
