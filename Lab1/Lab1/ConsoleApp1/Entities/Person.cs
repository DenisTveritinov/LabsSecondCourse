using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Person
    {
        public string firstName {  get; set; }
        public string lastName { get; set; }

        protected Person(string FirstName, string LastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }

        public abstract string getInfo();
    }
}
