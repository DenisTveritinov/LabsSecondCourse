using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Student : Person, ICanStudy
    {
        public string studentID { get; set; }
        public int course {  get; set; }
        public double averageGrade { get; set; }
        public string country { get; set; }
        public string passportNumber {  get; set; }

        public Student(string FirstName, string LastName, string StudentId,
                       int Course = 1, double AverageGrade = 0,
                       string Country = "Україна", string PassportNumber = "") : base(FirstName, LastName)
        {
            studentID = StudentId;
            course = Course;
            averageGrade = AverageGrade;
            country = Country;
            passportNumber = PassportNumber;
        }

        public void study()
        {
            course++;
        }

        public override string getInfo()
        {
            return $"Student {firstName} {lastName}. ID: {studentID}, Course: {course}, AvgGrade: {averageGrade}, Country: {country}, Passport: {passportNumber}";
        }



    }
}
