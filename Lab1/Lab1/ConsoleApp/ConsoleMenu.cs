using System;
using Application;
using DataAccess;
using Domain.Entities;

namespace ConsoleApp
{
    public class ConsoleMenu
    {
        private readonly IFileServices _fileService;
        private readonly IStudentServices _studentService;
        private readonly string _filePath = "students.txt";

        public ConsoleMenu()
        {
            _fileService = new FileServices();
            _studentService = new StudentServices();
        }

        public void start()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n==== МЕНЮ ====");
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Показати усіх студентів");
                Console.WriteLine("3. Порахувати іноземних відмінників 1 курсу");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ShowAllStudents();
                        break;
                    case "3":
                        CountExcellentForeigners();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        private void AddStudent()
        {
            Console.Write("Ім’я: ");
            string firstName = Console.ReadLine();

            Console.Write("Прізвище: ");
            string lastName = Console.ReadLine();

            Console.Write("Студентський квиток: ");
            string studentId = Console.ReadLine();

            Console.Write("Курс: ");
            int course = int.Parse(Console.ReadLine());

            Console.Write("Середній бал: ");
            double avgGrade = double.Parse(Console.ReadLine());

            Console.Write("Країна: ");
            string country = Console.ReadLine();

            Console.Write("Номер паспорту: ");
            string passport = Console.ReadLine();

            var student = new Student(firstName, lastName, studentId, course, avgGrade, country, passport);
            _fileService.saveStudent(student, _filePath);

            Console.WriteLine("Студента додано у файл!");
        }

        private void ShowAllStudents()
        {
            var students = _fileService.readStudents(_filePath);

            if (students.Length == 0)
            {
                Console.WriteLine("Файл порожній або не існує.");
                return;
            }

            Console.WriteLine("\n==== Список студентів ====");
            foreach (var s in students)
            {
                Console.WriteLine($"{s.firstName} {s.lastName} | Курс: {s.course} | Бал: {s.averageGrade} | Країна: {s.country}");
            }
        }

        private void CountExcellentForeigners()
        {
            var students = _fileService.readStudents(_filePath);
            int count = _studentService.CountExcellentForeigners(students);

            Console.WriteLine($"Кількість іноземних відмінників 1 курсу: {count}");
        }
    }
}
    
