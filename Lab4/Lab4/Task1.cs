using System;
using System.Text;

namespace Task1_Variant12
{
    // 1. Оголошуємо делегат (підпис методу)
    public delegate void StringAnalyzer(string input);

    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування консолі для коректного відображення кирилиці
            Console.OutputEncoding = Encoding.UTF8;

            string text = "Басейн №12: Temp=24C, Water=90%";
            Console.WriteLine($"Вхідний рядок: \"{text}\"\n");

            // --- СПОСІБ 1: Анонімний метод (Вимога на "Добре") ---
            // Використовуємо ключове слово delegate
            StringAnalyzer analyzeWithAnonymous = delegate (string str)
            {
                int digits = 0, letters = 0, others = 0;
                foreach (char c in str)
                {
                    if (char.IsDigit(c)) digits++;
                    else if (char.IsLetter(c)) letters++;
                    else others++;
                }
                Console.WriteLine($"[Анонімний метод] Цифр: {digits}, Літер: {letters}, Інших: {others}");
            };

            // Виклик
            analyzeWithAnonymous(text);

            Console.WriteLine(new string('-', 30));

            // --- СПОСІБ 2: Лямбда-вираз (Вимога на "Добре") ---
            // Використовуємо оператор =>
            StringAnalyzer analyzeWithLambda = (str) =>
            {
                int digits = 0, letters = 0, others = 0;
                foreach (char c in str)
                {
                    if (char.IsDigit(c)) digits++;
                    else if (char.IsLetter(c)) letters++;
                    else others++;
                }
                Console.WriteLine($"[Лямбда-вираз]    Цифр: {digits}, Літер: {letters}, Інших: {others}");
            };

            // Виклик
            analyzeWithLambda(text);

            Console.ReadKey();
        }
    }
}