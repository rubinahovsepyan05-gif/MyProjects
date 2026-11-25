using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace StructnClass;

public readonly struct Money
{
    public decimal Amount { get; }
    public string Currency { get; }
    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money operator +(Money x, Money y)
    {
        if (x.Currency == y.Currency)
            return new Money(x.Amount + y.Amount,x.Currency);
        else
        {
            throw new InvalidOperationException();
        }
    }

    public static Money operator -(Money x, Money y)
    {
        if (x.Currency == y.Currency)
            return new Money(x.Amount - y.Amount, x.Currency);
        else
        {
            throw new InvalidOperationException();
        }
    }
    
    public static Money operator *(Money x, decimal m)
    { 
        return new Money(x.Amount * m,x.Currency);
    }

    public static bool operator ==(Money x, Money y)
    {
        if (x.Currency == y.Currency && x.Amount == y.Amount)
            return true;
        return false;   
    }

    public static bool operator !=(Money x, Money y)
    {
        return !(x == y);
    }

    public static bool operator <(Money x, Money y)
    {
        if (x.Currency == y.Currency)
        {
            if (x.Amount < y.Amount)
                return true;
            return false;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public static bool operator >(Money x, Money y)
    {
        if (x.Currency == y.Currency)
        {
            if (x.Amount > y.Amount)
                return true;
            return false;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }
    
    public bool Equals(Money other)
    {
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override bool Equals(object obj)
    {
        if (obj is Money money)
             return Equals(money);
        else
            return false;
    }

    public override int GetHashCode()
    {
        int hash = HashCode.Combine(Amount, Currency);
        return hash;
    }

    public override string ToString()
    {
        return $"{Amount.ToString("F2", CultureInfo.InvariantCulture)} {Currency}";
    }

    public Money[] Allocate(int parts)
    {
        if (parts <= 0)
            throw new ArgumentOutOfRangeException(nameof(parts));
        Money[] money = new Money[parts];
        decimal amount = Math.Truncate(Amount * 100 / parts) / 100;
        
        decimal firstAmount;
        firstAmount = (Amount - amount * parts) + amount;
        money[0] = new Money(firstAmount, Currency);
        
        for (int i = 1; i < parts; i++)
        {
            money[i] = new Money(amount, Currency);
        }
        return money;
    }
}