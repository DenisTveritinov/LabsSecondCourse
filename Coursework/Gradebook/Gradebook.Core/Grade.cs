using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradebook.Core
{
    public class Grade
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public int Mark { get; set; }

        public DateTime Date { get; set; }
    }
}
