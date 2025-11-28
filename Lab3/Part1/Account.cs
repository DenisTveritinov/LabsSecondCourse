using System;
using System.Runtime.Serialization;

namespace Part1
{
    [Serializable]
    public class Account : ISerializable
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        
        public Account() { }

        public Account(int id, decimal balance)
        {
            Id = id;
            Balance = balance;
        }

        public void Deposit(decimal sum) => Balance += sum;
        public void Withdraw(decimal sum) => Balance -= sum;

        public void Transfer(Account destination, decimal sum)
        {
            this.Withdraw(sum);
            destination.Deposit(sum);
        }

        public decimal Recalculation(decimal rate) => Balance * rate;

        public void GetInfo()
        {
            Console.WriteLine($"ID: {Id}, Balance: {Balance:C}");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MyKey_ID", Id);
            info.AddValue("MyKey_Balance", Balance);
            info.AddValue("SaveDate", DateTime.Now);
        }

        protected Account(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetInt32("MyKey_ID");
            Balance = info.GetDecimal("MyKey_Balance");
        }
    }
}