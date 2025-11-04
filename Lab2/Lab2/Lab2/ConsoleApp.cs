using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class ConsoleApp
    {
        
        static void Main()
        {
            
            Account acc1 = new Account("S-101", 300m);
            Account acc2 = new Account("F-101", 3050m);
            Account acc3 = new Account("A-101", 3600m);
            Account acc4 = new Account("g-101", 600m);

            
            DemonstrateArray(acc1, acc2, acc3, acc4);
            DemonstrateArrayList(acc1, acc2, acc3, acc4);
            DemonstrateListT(acc1, acc2, acc3, acc4);
        }

        // 2. Функція для Масиву
        private static void DemonstrateArray(Account acc1, Account acc2, Account acc3, Account acc4)
        {
            Console.WriteLine("--- Демонстрація Масиву (Array) ---");

            Account[] accArr = new Account[4];

            // 1. Додавання в масив
            accArr[0] = acc1;
            accArr[1] = acc2;
            accArr[2] = acc3;
            accArr[3] = acc4;

            // 2. Прохід по масиву
            Console.WriteLine("Прохід по масиву:");
            foreach (Account acc in accArr)
            {
                Console.WriteLine(acc);
            }

            // 3. Пошук (знайти "F-101")
            Account foundAccArr = null;
            foreach (Account acc in accArr)
            {
                if (acc.id == "F-101")
                {
                    foundAccArr = acc;
                    break;
                }
            }
            Console.WriteLine($"\nЗнайдено: {foundAccArr}");

            // 4. Оновлення 
            if (foundAccArr != null)
            {
                foundAccArr.deposit(100m);
                Console.WriteLine($"Оновлений рахунок: {foundAccArr}");
            }

            // 5. Видалення (видалити "A-101")
            for (int i = 0; i < accArr.Length; i++)
            {
                if (accArr[i] != null && accArr[i].id == "A-101")
                {
                    accArr[i] = null; 
                    break;
                }
            }
            Console.WriteLine("\nМасив після видалення 'A-101':");
            foreach (Account acc in accArr)
            {
                if (acc == null)
                {
                    Console.WriteLine("[пуста комірка]");
                }
                else
                {
                    Console.WriteLine(acc);
                }
            }
            Console.WriteLine("\n");
        }

        // 3. Функція для ArrayList
        private static void DemonstrateArrayList(Account acc1, Account acc2, Account acc3, Account acc4)
        {
            Console.WriteLine("--- Демонстрація ArrayList ---");

            ArrayList accArrayList = new ArrayList();

            // 1. Додавання
            accArrayList.Add(acc1);
            accArrayList.Add(acc2);
            accArrayList.Add(acc3);
            accArrayList.Add(acc4);

            // 2. Прохід
            Console.WriteLine("Прохід по ArrayList:");
            foreach (object obj in accArrayList)
            {
                if (obj is Account)
                {
                    Account acc = (Account)obj;
                    Console.WriteLine(acc);
                }
            }

            // 3. Пошук (знайти "g-101")
            Account foundAccArrayList = null;
            foreach (object obj in accArrayList)
            {
                if (obj is Account acc)
                {
                    if (acc.id == "g-101")
                    {
                        foundAccArrayList = acc;
                        break;
                    }
                }
            }
            Console.WriteLine($"\nЗнайдено: {foundAccArrayList}");

            // 4. Оновлення
            if (foundAccArrayList != null)
            {
                foundAccArrayList.deposit(50m);
                Console.WriteLine($"Оновлений рахунок: {foundAccArrayList}");
            }

            // 5. Видалення (видалити "S-101")
            Account toRemoveArrayList = null;
            foreach (object obj in accArrayList)
            {
                if (obj is Account acc && acc.id == "S-101")
                {
                    toRemoveArrayList = acc;
                    break;
                }
            }
            if (toRemoveArrayList != null)
            {
                accArrayList.Remove(toRemoveArrayList);
                Console.WriteLine("\nArrayList після видалення 'S-101':");
                foreach (object obj in accArrayList)
                {
                    Console.WriteLine(obj);
                }
            }
            Console.WriteLine("\n");
        }

        // 4. Функція для List<T>
        private static void DemonstrateListT(Account acc1, Account acc2, Account acc3, Account acc4)
        {
            Console.WriteLine("--- Демонстрація List<Account> ---");

            List<Account> accList = new List<Account>();

            // 1. Додавання
            accList.Add(acc1);
            accList.Add(acc2);
            accList.Add(acc3);
            accList.Add(acc4);

            // 2. Прохід
            Console.WriteLine("Прохід по List<Account>:");
            foreach (Account acc in accList)
            {
                Console.WriteLine(acc);
            }

            // 3. Пошук (знайти "A-101")
            Account foundAccList = null;
            foreach (Account acc in accList)
            {
                if (acc.id == "A-101")
                {
                    foundAccList = acc;
                    break;
                }
            }
            Console.WriteLine($"\nЗнайдено: {foundAccList}");

            // 4. Оновлення (додати 200)
            if (foundAccList != null)
            {
                foundAccList.deposit(200m);
                Console.WriteLine($"Оновлений рахунок: {foundAccList}");
            }

            // 5. Видалення (видалити "F-101")
            Account toRemoveList = null;
            foreach (Account acc in accList)
            {
                if (acc.id == "F-101")
                {
                    toRemoveList = acc;
                    break;
                }
            }
            if (toRemoveList != null)
            {
                accList.Remove(toRemoveList);
                Console.WriteLine("\nList після видалення 'F-101':");
                foreach (Account acc in accList)
                {
                    Console.WriteLine(acc);
                }
            }
            Console.WriteLine("\n");
        }
    }
}