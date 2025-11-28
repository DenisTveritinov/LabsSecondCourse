using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;                             
using System.Xml.Serialization;                     

namespace Part1
{
    class Program
    {
        static void Main(string[] args)
        {

            Account[] accounts = new Account[]
            {
                new Account(1, 1000m),
                new Account(2, 2500m),
                new Account(3, 500m)
            };

            Console.WriteLine("=== ПОЧАТКОВІ ДАНІ ===");
            foreach (var a in accounts) a.GetInfo();
            Console.WriteLine();

            
            TestBinary(accounts);


            TestXml(accounts);

            TestJson(accounts);

            TestCollection();

            Console.WriteLine("\nГотово! Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }

#pragma warning disable SYSLIB0011 
        static void TestBinary(Account[] data)
        {
            Console.WriteLine("-> Тестуємо Binary (Custom ISerializable)...");
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("data.bin", FileMode.Create))
            {
                formatter.Serialize(fs, data);
            }

            using (FileStream fs = new FileStream("data.bin", FileMode.Open))
            {
                Account[] loaded = (Account[])formatter.Deserialize(fs);
                Console.WriteLine($"   Відновлено {loaded.Length} об'єктів. Баланс першого: {loaded[0].Balance}");
            }
        }
#pragma warning restore SYSLIB0011

        static void TestXml(Account[] data)
        {
            Console.WriteLine("-> Тестуємо XML...");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Account[]));

            using (FileStream fs = new FileStream("data.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, data);
            }

            using (FileStream fs = new FileStream("data.xml", FileMode.Open))
            {
                Account[] loaded = (Account[])xmlSerializer.Deserialize(fs);
                Console.WriteLine($"   Відновлено {loaded.Length} об'єктів. Баланс першого: {loaded[0].Balance}");
            }
        }

        static void TestJson(Account[] data)
        {
            Console.WriteLine("-> Тестуємо JSON...");

            string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("data.json", jsonString);

            string readString = File.ReadAllText("data.json");
            Account[] loaded = JsonSerializer.Deserialize<Account[]>(readString);

            Console.WriteLine($"   Відновлено {loaded.Length} об'єктів. Баланс першого: {loaded[0].Balance}");
        }

        static void TestCollection()
        {
            Console.WriteLine("-> Тестуємо колекцію List<Account>...");
            List<Account> list = new List<Account>
            {
                new Account(100, 50m),
                new Account(200, 70m)
            };

            string json = JsonSerializer.Serialize(list);

            List<Account> loadedList = JsonSerializer.Deserialize<List<Account>>(json);

            Console.WriteLine($"   Список відновлено. Елементів: {loadedList.Count}");
        }
    }
}