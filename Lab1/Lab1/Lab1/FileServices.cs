using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace DataAccess
{
    public class FileServices : IFileServices
    {
        public void saveStudent(Student student, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"Student {student.firstName}{student.lastName}");
                sw.WriteLine("{");
                sw.WriteLine($"  \"FirstName\": \"{student.firstName}\",");
                sw.WriteLine($"  \"LastName\": \"{student.lastName}\",");
                sw.WriteLine($"  \"StudentId\": \"{student.studentID}\",");
                sw.WriteLine($"  \"Course\": \"{student.course}\",");
                sw.WriteLine($"  \"AverageGrade\": \"{student.averageGrade}\",");
                sw.WriteLine($"  \"Country\": \"{student.country}\",");
                sw.WriteLine($"  \"PassportNumber\": \"{student.passportNumber}\"");
                sw.WriteLine("};");
            }

        }

        public Student[] readStudents (string path)
        {
            if (!File.Exists(path))
                return new Student[0];

            var lines = File.ReadAllLines(path);
            var students = new System.Collections.Generic.List<Student>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("Student "))
                {
                    string FirstName = "";
                    string LastName = "";
                    string StudentId = "";
                    int Course = 1;
                    double AverageGrade = 0;
                    string Country = "Україна";
                    string PassportNumber = "";

                    
                    i++;
                    while (!lines[i].Trim().Equals("};"))
                    {
                        var line = lines[i].Trim();

                        var match = Regex.Match(line, "\"(\\w+)\":\\s*\"([^\"]+)\"");
                        if (match.Success)
                        {
                            string key = match.Groups[1].Value;
                            string value = match.Groups[2].Value;

                            switch (key)
                            {
                                case "FirstName": FirstName = value; break;
                                case "LastName": LastName = value; break;
                                case "StudentId": StudentId = value; break;
                                case "Course": Course = int.Parse(value); break;
                                case "AverageGrade": AverageGrade = double.Parse(value); break;
                                case "Country": Country = value; break;
                                case "PassportNumber": PassportNumber = value; break;
                            }
                        }

                        i++;
                    }

                    students.Add(new Student(FirstName, LastName, StudentId, Course, AverageGrade, Country, PassportNumber));
                }
            }

            return students.ToArray();
        }
    }
}
