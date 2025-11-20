using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Gradebook.Core;
using System.Threading.Tasks;

namespace Gradebook.BLL.Interfaces
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetAll();
        Group GetById(int id);
        void Create(Group group);
        void Update(Group group);
        void Delete(int id);
    }
}
