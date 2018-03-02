using System;
using System.Collections.Generic;

namespace MoneyBank
{
    public class Bank
    {
        private Dictionary<Tuple<string, string>, int> rates;

        public Bank()
        {
            rates = new Dictionary<Tuple<string, string>, int>();
        }

        public void AddRate(string from, string to, int rate)
        {
            rates.Add(new Tuple<string, string>(from, to), rate);
        }

        public Money Reduce(IExpression source, string to)
        {
            return source.Reduce(this, to);
        }

        public int Rate(string from, string to)
        {
            if (from.Equals(to, StringComparison.Ordinal))
            {
                return 1;
            }
            return rates[new Tuple<string, string>(from, to)];
        }
    }
}
