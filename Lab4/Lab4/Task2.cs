using System;
using System.Text;

namespace Task2_3_Variant12
{
    // --- Частина 2: Клас-аргумент події ---
    // Зберігає інформацію про те, що сталося (поточний об'єм та надлишок)
    public class PoolEventArgs : EventArgs
    {
        public double CurrentVolume { get; }
        public double MaxVolume { get; }
        public string Message { get; }

        public PoolEventArgs(double current, double max, string message)
        {
            CurrentVolume = current;
            MaxVolume = max;
            Message = message;
        }
    }

    // --- Частина 2: Компонент (Клас Басейн) ---
    public class Pool
    {
        // Властивості басейну
        public double Length { get; }
        public double Width { get; }
        public double Depth { get; }

        // Поточний об'єм води
        public double WaterVolume { get; private set; }

        // Максимальний об'єм (обчислюється автоматично)
        public double MaxVolume => Length * Width * Depth;

        // Подія: використовуємо стандартний узагальнений делегат EventHandler
        public event EventHandler<PoolEventArgs> PoolOverflow;

        public Pool(double length, double width, double depth)
        {
            Length = length;
            Width = width;
            Depth = depth;
            WaterVolume = 0;
        }

        // Метод доливання води
        public void AddWater(double amount)
        {
            Console.WriteLine($"Attempt to add {amount} m3 of water...");
            double projectedVolume = WaterVolume + amount;

            if (projectedVolume > MaxVolume)
            {
                // Вода переливається!
                WaterVolume = MaxVolume; // Басейн повний по вінця

                // Викликаємо подію (якщо є підписники)
                PoolOverflow?.Invoke(this, new PoolEventArgs(projectedVolume, MaxVolume, "Вода перелилася через край!"));
            }
            else
            {
                WaterVolume = projectedVolume;
                Console.WriteLine($"   Воду долито. Рівень: {WaterVolume}/{MaxVolume}");
            }
        }

        // Метод зливу води
        public void DrainWater(double amount)
        {
            Console.WriteLine($"Draining {amount} m3 of water...");
            WaterVolume -= amount;
            if (WaterVolume < 0) WaterVolume = 0;
            Console.WriteLine($"   Воду злито. Рівень: {WaterVolume}/{MaxVolume}");
        }
    }

    // --- Частина 3: Програма (Використання) ---
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // 1. Створюємо басейн (5м x 2м x 1.5м = 15 кубів максимум)
            Pool myPool = new Pool(5, 2, 1.5);
            Console.WriteLine($"Басейн створено. Макс. об'єм: {myPool.MaxVolume} куб.м.\n");

            // 2. ПІДПИСКА НА ПОДІЮ
            // Ми кажемо: коли станеться PoolOverflow, запусти метод OnPoolOverflow
            myPool.PoolOverflow += OnPoolOverflow;

            // 3. Тестуємо (Доливаємо воду)
            myPool.AddWater(10); // Все ок
            myPool.AddWater(4);  // Все ок (разом 14)

            Console.WriteLine("\n--- Спроба переповнити ---");
            myPool.AddWater(5);  // 14 + 5 = 19 (Це більше ніж 15 -> ПОДІЯ!)

            Console.ReadKey();
        }

        // Метод-обробник події
        // sender - це сам об'єкт басейну
        // e - це наші дані (об'єм, повідомлення)
        private static void OnPoolOverflow(object sender, PoolEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n>>> УВАГА! ПОДІЯ ПЕРЕПОВНЕННЯ <<<");
            Console.WriteLine($"Повідомлення: {e.Message}");
            Console.WriteLine($"Спроба заповнити до: {e.CurrentVolume} куб.м.");
            Console.WriteLine($"Максимальний об'єм басейну: {e.MaxVolume} куб.м.");

            // Можемо отримати доступ до властивостей самого басейну через sender
            if (sender is Pool p)
            {
                Console.WriteLine($"Глибина цього басейну: {p.Depth} м.");
            }
            Console.ResetColor();
        }
    }
}