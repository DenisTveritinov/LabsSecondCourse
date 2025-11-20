using Gradebook.BLL.Services;
using Gradebook.Core;
using Gradebook.BLL.Exeptions;

namespace Gradebook.PL
{
    public class ApplicationUI
    {
        private readonly ServiceManager _manager;
        private bool _isRunning = true;

        public ApplicationUI(ServiceManager manager)
        {
            _manager = manager;
        }

        public void Run()
        {
            while (_isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();
                ProcessChoice(choice);
                if (_isRunning)
                {
                    Console.WriteLine("\nНатисніть Enter для продовження...");
                    Console.ReadLine();
                }
            }
        }

        private void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ЕЛЕКТРОННИЙ ЖУРНАЛ УСПІШНОСТІ ===");
            Console.WriteLine("1. Керування Студентами та Групами (CRUD)");
            Console.WriteLine("2. Керування Предметами та Оцінками");
            Console.WriteLine("3. Аналіз та Звітність (Пошук, Середній бал)");
            Console.WriteLine("0. Вихід");
            Console.WriteLine("Ваш вибір: ");
        }

        private void ProcessChoice(string choice)
        {
            try
            {
                switch (choice)
                {
                    case "1": StudentGroupManagementMenu(); break;
                    case "2": SubjectGradeManagementMenu(); break;
                    case "3": AnalysisAndReportingMenu(); break;
                    case "0":
                        _isRunning = false;
                        Console.WriteLine("Програма завершена.");
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
            catch (ValidationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[Помилка Валідації BLL]: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[Критична Помилка]: {ex.Message}");
                Console.ResetColor();
            }
        }


        private void StudentGroupManagementMenu()
        {
            Console.Clear();
            Console.WriteLine("--- КЕРУВАННЯ СТУДЕНТАМИ ТА ГРУПАМИ ---");
            Console.WriteLine("1. Додати Студента/Групу");
            Console.WriteLine("2. Видалити Студента/Групу");
            Console.WriteLine("3. Змінити дані Студента");
            Console.WriteLine("4. Переглянути список Студентів");
            Console.WriteLine("5. Переглянути список Груп");
            Console.WriteLine("Вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddStudentOrGroupMenu(); break;
                case "2": DeleteStudentOrGroupMenu(); break;
                case "3": UpdateStudentData(); break;
                case "4": DisplayAllStudents(); break;
                case "5": DisplayAllGroups(); break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }

        private void AddStudentOrGroupMenu()
        {
            Console.Write("\nДодати (S-Студент, G-Група): ");
            string type = Console.ReadLine()?.ToUpper();

            switch (type)
            {
                case "S": AddStudent(); break;
                case "G": AddGroup(); break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }

        private void AddStudent()
        {
            Console.Write("Ім'я: ");
            string firstName = Console.ReadLine();
            Console.Write("Прізвище: ");
            string lastName = Console.ReadLine();
            Console.Write("ID Групи: ");

            if (!int.TryParse(Console.ReadLine(), out int groupId))
            {
                throw new ValidationException("Невірний формат ID групи.");
            }

            var newStudent = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                GroupId = groupId
            };

            _manager.StudentService.CreateStudent(newStudent);
            Console.WriteLine("Студент успішно доданий.");
        }

        private void AddGroup()
        {
            Console.Write("Назва групи (напр. ІПЗ-21): ");
            string name = Console.ReadLine();
            var newGroup = new Group { Name = name };
            _manager.GroupService.CreateGroup(newGroup);
            Console.WriteLine($"Група '{name}' успішно додана.");
        }

        private void DeleteStudentOrGroupMenu()
        {
            Console.Write("\nВидалити (S-Студента, G-Групу): ");
            string type = Console.ReadLine()?.ToUpper();

            Console.Write("Введіть ID для видалення: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                throw new ValidationException("Невірний ID.");
            }

            switch (type)
            {
                case "S":
                    _manager.StudentService.DeleteStudent(id);
                    Console.WriteLine($"Студент ID {id} видалений.");
                    break;
                case "G":
                    _manager.GroupService.DeleteGroup(id);
                    Console.WriteLine($"Група ID {id} видалена.");
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }

        private void UpdateStudentData()
        {
            Console.Write("Введіть ID студента, якого потрібно змінити: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                throw new ValidationException("Невірний ID.");
            }

            var student = _manager.StudentService.GetStudentById(id);
            if (student == null)
            {
                throw new ValidationException($"Студент ID {id} не знайдений.");
            }

            Console.WriteLine($"Поточні дані: {student.FirstName} {student.LastName}, Група: {student.GroupId}");

            Console.Write("Нове ім'я (залиште пустим, щоб не змінювати): ");
            string newFirstName = Console.ReadLine();
            Console.Write("Нове прізвище (залиште пустим, щоб не змінювати): ");
            string newLastName = Console.ReadLine();
            Console.Write("Новий ID Групи (0, щоб не змінювати): ");

            if (!int.TryParse(Console.ReadLine(), out int newGroupId))
            {
                newGroupId = 0;
            }

            if (string.IsNullOrWhiteSpace(newFirstName))
            {
            }
            else
            {
                student.FirstName = newFirstName;
            }

            if (string.IsNullOrWhiteSpace(newLastName))
            {
            }
            else
            {
                student.LastName = newLastName;
            }

            if (newGroupId != 0)
            {
                student.GroupId = newGroupId;
            }

            _manager.StudentService.UpdateStudent(student);
            Console.WriteLine($"Дані студента ID {id} оновлено.");
        }

        private void DisplayAllStudents()
        {
            var students = _manager.StudentService.GetAllStudents();
            DisplayStudentsList(students);
        }

        private void DisplayAllGroups()
        {
            var groups = _manager.GroupService.GetAllGroups();
            Console.WriteLine("\nID | Назва");
            Console.WriteLine("---------------");
            foreach (var g in groups)
            {
                Console.WriteLine($"{g.Id} | {g.Name}");
            }
        }

        private void SubjectGradeManagementMenu()
        {
            Console.Clear();
            Console.WriteLine("--- ПРЕДМЕТИ ТА ОЦІНКИ ---");
            Console.WriteLine("1. Додати Предмет");
            Console.WriteLine("2. Видалити Предмет");
            Console.WriteLine("3. Виставити/Змінити оцінку");
            Console.WriteLine("Вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddSubject(); break;
                case "2": DeleteSubject(); break;
                case "3": AddGradeMenu(); break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }

        private void AddSubject()
        {
            Console.Write("Назва Предмета: ");
            string name = Console.ReadLine();
            var newSubject = new Subject { Name = name };
            _manager.SubjectService.CreateSubject(newSubject);
            Console.WriteLine($"Предмет '{name}' успішно доданий.");
        }

        private void DeleteSubject()
        {
            Console.Write("Введіть ID предмета для видалення: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                throw new ValidationException("Невірний ID.");
            }
            _manager.SubjectService.DeleteSubject(id);
            Console.WriteLine($"Предмет ID {id} видалений (з перевіркою!).");
        }

        private void AddGradeMenu()
        {
            Console.Write("ID Студента: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId)) 
            { 
                throw new ValidationException("Невірний ID студента."); 
            }
            Console.Write("ID Предмета: ");
            if (!int.TryParse(Console.ReadLine(), out int subjectId)) 
            { 
                throw new ValidationException("Невірний ID предмета."); 
            }
            Console.Write("Оцінка (0-100): ");
            if (!int.TryParse(Console.ReadLine(), out int mark)) 
            { 
                throw new ValidationException("Невірний формат оцінки."); 
            }

            var newGrade = new Grade
            {
                StudentId = studentId,
                SubjectId = subjectId,
                Mark = mark,
                Date = DateTime.Now
            };

            _manager.GradeService.AddGrade(newGrade);
            Console.WriteLine($"Оцінка {mark} успішно виставлена.");
        }

        private void AnalysisAndReportingMenu()
        {
            Console.Clear();
            Console.WriteLine("--- АНАЛІЗ ТА ЗВІТНІСТЬ ---");
            Console.WriteLine("1. Пошук студента за ПІБ");
            Console.WriteLine("2. Пошук студентів по Групі");
            Console.WriteLine("3. Детальний перегляд успішності");
            Console.WriteLine("4. Аналіз середнього балу");
            Console.Write("Вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": SearchStudentsByName(); break;
                case "2": SearchStudentsByGroup(); break;
                case "3": ViewDetailedPerformance(); break;
                case "4": AnalyzeAverageMarks(); break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }

        private void SearchStudentsByName()
        {
            Console.Write("\nВведіть ім'я або частину прізвища для пошуку: ");
            string query = Console.ReadLine();
            var students = _manager.StudentService.SearchStudents(query);
            DisplayStudentsList(students);
        }

        private void SearchStudentsByGroup()
        {
            Console.Write("Введіть ID Групи для пошуку студентів: ");
            if (!int.TryParse(Console.ReadLine(), out int groupId))
            {
                throw new ValidationException("Невірний ID.");
            }

            var students = _manager.StudentService.GetStudentsByGroup(groupId);
            DisplayStudentsList(students);
        }

        private void ViewDetailedPerformance()
        {
            Console.Write("ID Студента для детального перегляду: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                throw new ValidationException("Невірний ID.");
            }

            var student = _manager.StudentService.GetStudentById(studentId);
            if (student == null)
            {
                throw new ValidationException($"Студент ID {studentId} не знайдений.");
            }

            var grades = _manager.GradeService
                .GetGradesForStudent(studentId)
                .ToList();

            var subjects = _manager.SubjectService
                .GetAllSubjects()
                .ToDictionary(s => s.Id, s => s.Name);

            Console.WriteLine($"\n--- УСПІШНІСТЬ СТУДЕНТА: {student.FirstName} {student.LastName} ---");

            if (!grades.Any())
            {
                Console.WriteLine("Оцінки відсутні.");
                return;
            }

            Console.WriteLine("Дата\t\t| Предмет\t\t| Оцінка");
            Console.WriteLine("-------------------------------------------------");
            foreach (var g in grades)
            {
                string subjectName = subjects.TryGetValue(g.SubjectId, out var name)
                    ? name
                    : $"Предмет ID {g.SubjectId} (Невідомий)";
                Console.WriteLine($"{g.Date.ToShortDateString()}\t| {subjectName}\t| {g.Mark}");
            }
            Console.WriteLine($"\nЗагальний середній бал: {_manager.GradeService.GetAverageMarkForStudent(studentId):F2}");
        }

        private void AnalyzeAverageMarks()
        {
            Console.Write("Показати студентів з середнім балом ВИЩЕ або НИЖЧЕ (H/L): ");
            string type = Console.ReadLine()?.ToUpper();

            Console.Write("Введіть пороговий середній бал (напр., 70): ");

            if (!double.TryParse(Console.ReadLine(), out double threshold)) 
            { 
                throw new ValidationException("Невірний формат балу."); 
            }

            var allStudents = _manager.StudentService.GetAllStudents();
            var results = new List<(Student Student, double Avg)>();

            foreach (var student in allStudents)
            {
                double avg = _manager.GradeService.GetAverageMarkForStudent(student.Id);

                if (type == "H" && avg >= threshold)
                {
                    results.Add((student, avg));
                }
                else if (type == "L" && avg < threshold)
                {
                    results.Add((student, avg));
                }
            }

            Console.WriteLine("\nСтуденти, що відповідають критеріям:");
            Console.WriteLine("------------------------------------------");
            if (!results.Any())
            {
                Console.WriteLine("Студенти не знайдені.");
            }
            else
            {
                foreach (var r in results.OrderByDescending(r => r.Avg))
                {
                    Console.WriteLine($"{r.Student.FirstName} {r.Student.LastName} | Середній бал: {r.Avg:F2}");
                }
            }
        }

        private void DisplayStudentsList(IEnumerable<Student> students)
        {
            if (!students.Any())
            {
                Console.WriteLine("Список студентів порожній або студенти не знайдені.");
                return;
            }

            Console.WriteLine("\nID | Ім'я | Група");
            Console.WriteLine("------------------------------");
            foreach (var s in students)
            {
                var group = _manager.GroupService.GetGroupById(s.GroupId);
                string groupName = group != null ? group.Name : $"ID: {s.GroupId} (Не знайдено)";
                Console.WriteLine($"{s.Id} | {s.FirstName} {s.LastName} | {groupName}");
            }
        }
    }
}