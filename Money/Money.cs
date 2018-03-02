using System;

namespace MoneyBank
{
    public class Money : IExpression
    {
        public int Amount { get; }
        public string Currency { get; }

        public static Money Dollar(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money Franc(int amount)
        {
            return new Money(amount, "CHF");
        }

        public Money(int amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }

        public IExpression Times(int multiplier)
        {
            return new Money(Amount * multiplier, Currency);
        }

        public Money Reduce(Bank bank, string to)
        {
            int rate = bank.Rate(Currency, to); 
            return new Money(Amount / rate, to);
        }

        public override bool Equals(object other)
        {
            Money money = (Money)other;
            return Amount == money.Amount && Currency.Equals(money.Currency, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return string.Concat(Amount, " ", Currency);
        }
    }
}
