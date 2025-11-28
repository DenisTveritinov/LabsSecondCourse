using System;

namespace Part2.DAL
{
    [Serializable]
    public class Student
    {
        public string Surname { get; set; }
        public int Course { get; set; } 
        public string StudentTicket { get; set; } 
        public double AverageGrade { get; set; }
        public string Country { get; set; }
        public string PassportNumber { get; set; }

        public Student() { }

        public Student(string surname, int course, string ticket, double grade, string country, string passport)
        {
            Surname = surname;
            Course = course;
            StudentTicket = ticket;
            AverageGrade = grade;
            Country = country;
            PassportNumber = passport;
        }

        public override string ToString()
        {
            return $"{Surname} | Курс: {Course} | Бал: {AverageGrade} | Країна: {Country}";
        }
    }
}