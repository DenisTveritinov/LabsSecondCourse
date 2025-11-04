public class Account : IComparable<Account>
{
    public string id { get; init; }
    public decimal balance { get; private set; }

    public Account(string id, decimal balance)
    {
        if(String.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Id can't be empty", nameof(id));
        }

        if(balance < 0)
        {
            throw new ArgumentException("Balance can't be negative", nameof(balance));
        }

        this.id = id;
        this.balance = balance; 
    }

    public void deposit(decimal amount)
    {
        if(amount < 0)
        {
            throw new ArgumentException("Deposit can't be negative", nameof(amount));
        }
        else
        {
            this.balance = this.balance + amount;
        }
    }

    public bool withdraw(decimal amount)
    {
        if(amount <= 0)
        {
            return false;
        }
        else if(this.balance >= amount)
        {
            this.balance -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool transfer (Account destination, decimal amount)
    {
        if(destination == null)
        {
            return false;
        }

        bool withdrawSuccess = withdraw(amount);

        if (withdrawSuccess)
        {
            destination.deposit(amount);

            return true;
        }
        else
        {
            return false;
        }
    }

    public void convertBalance(decimal exchangeRate)
    {
        if(exchangeRate < 0)
        {
            throw new ArgumentException("Exchange rate can't be negative");
        }
        else
        {
            this.balance = this.balance * exchangeRate;
        }
    }

    public override string ToString()
    {
        return $"[Account] Id: {this.id}, Balance: {this.balance:C}";
    }

    public int CompareTo(Account other)
    {
        if (other == null)
        {
            return 1; 
        }

        return this.id.CompareTo(other.id);
    }
}