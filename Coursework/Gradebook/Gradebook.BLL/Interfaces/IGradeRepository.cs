using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gradebook.Core;

namespace Gradebook.BLL.Interfaces
{
    public interface IGradeRepository
    {
        IEnumerable<Grade> GetAll();
        Grade GetById(int id);
        IEnumerable<Grade> GetGradesByStudent(int studentId);
        void Create(Grade grade);
        void Update(Grade grade);
        void Delete(int id);
        void DeleteByStudentId(int studentId);
    }
}
