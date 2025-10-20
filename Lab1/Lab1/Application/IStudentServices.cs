using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application
{
    public interface IStudentServices
    {
        int CountExcellentForeigners(Student[] students);
    }
}
