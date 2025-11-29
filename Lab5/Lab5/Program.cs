using System;
using System.Collections.Generic;
using StudentProject.BLL;
using StudentProject.DAL;
using StudentProject.Models;

namespace StudentProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var students = new List<Student>
            {
                new Student { Surname = "Smith", Course = 1, AverageGrade = 95, Country = "USA", YearsInUkraine = 2 },
                new Student { Surname = "Ivanov", Course = 1, AverageGrade = 92, Country = "Ukraine", YearsInUkraine = 18 },
                new Student { Surname = "Kim", Course = 1, AverageGrade = 80, Country = "Korea", YearsInUkraine = 6 },
                new Student { Surname = "Muller", Course = 2, AverageGrade = 98, Country = "Germany", YearsInUkraine = 3 },
                new Student { Surname = "Garcia", Course = 1, AverageGrade = 99, Country = "Spain", YearsInUkraine = 5 }
            };

            FileRepository repository = new FileRepository();
            repository.SaveStudents(students);
            var loadedStudents = repository.LoadStudents();

            StudentService service = new StudentService();
            var filteredStudents = service.GetExcellentForeignFreshmen(loadedStudents);

            Console.WriteLine("--- Іноземні студенти-відмінники 1-го курсу ---");
            foreach (var s in filteredStudents)
            {
                bool citizenship = service.CheckCitizenshipEligibility(s);
                Console.WriteLine($"Студент: {s.Surname}, Країна: {s.Country}, Бал: {s.AverageGrade}. " +
                                  $"Громадянство можливо: {(citizenship ? "Так" : "Ні")}");
            }

            Console.ReadKey();
        }
    }
}