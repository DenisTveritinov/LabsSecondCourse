using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gradebook.Core;

namespace Gradebook.BLL.Interfaces
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetAll();
        Subject GetById(int id);
        void Create(Subject subject);
        void Update(Subject subject);
        void Delete(int id);
    }
}
