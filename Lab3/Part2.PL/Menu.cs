using System;
using Part2.BLL;
using Part2.DAL;

namespace Part2.PL
{
    public class Menu
    {
        private EntityService _service = new EntityService();

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛІННЯ СТУДЕНТАМИ (ВАРІАНТ 12) ===");
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Показати всіх студентів");
                Console.WriteLine("3. ЗАВДАННЯ: Знайти іноземців-відмінників 1 курсу");
                Console.WriteLine("4. Зберегти дані у файл");
                Console.WriteLine("0. Вихід");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": AddStudentUI(); break;
                        case "2": ShowAllUI(); break;
                        case "3": ShowTaskUI(); break;
                        case "4":
                            _service.SaveChanges();
                            Console.WriteLine("Дані збережено!");
                            Console.ReadKey();
                            break;
                        case "0": return;
                        default: Console.WriteLine("Невірний вибір."); break;
                    }
                }
                catch (StudentServiceException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n[Помилка бізнес-логіки]: {ex.Message}");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Критична помилка]: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        private void AddStudentUI()
        {
            Console.WriteLine("\n--- Додавання студента ---");

            Console.Write("Прізвище: ");
            string surname = Console.ReadLine();

            Console.Write("Курс (1-6): ");
            int course = int.Parse(Console.ReadLine());

            Console.Write("Студентський квиток: ");
            string ticket = Console.ReadLine();

            Console.Write("Середній бал (0-100): ");
            double grade = double.Parse(Console.ReadLine());

            Console.Write("Країна: ");
            string country = Console.ReadLine();

            Console.Write("Номер закордонного паспорту: ");
            string passport = Console.ReadLine();

            Student newStudent = new Student(surname, course, ticket, grade, country, passport);
            _service.AddStudent(newStudent);

            Console.WriteLine("Студента додано успішно!");
            Console.ReadKey();
        }

        private void ShowAllUI()
        {
            Console.WriteLine("\n--- Всі студенти ---");
            var list = _service.GetAll();
            foreach (var s in list)
            {
                Console.WriteLine(s);
            }
            Console.ReadKey();
        }

        private void ShowTaskUI()
        {
            Console.WriteLine("\n--- Результат завдання (Іноземці, 1 курс, Відмінники) ---");
            var results = _service.GetForeignExcellentFirstYearStudents();

            Console.WriteLine($"Знайдено студентів: {results.Count}");
            foreach (var s in results)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(s);
                Console.ResetColor();
            }
            Console.ReadKey();
        }
    }
}