using MoneyBank;
using NUnit.Framework;

namespace MoneyTest
{
    [TestFixture]
    public class MoneyTest
    {
        private IExpression fiveBucks;
        private IExpression tenFrancs;
        private Bank bank;

        [SetUp]
        public void SetUp()
        {
            fiveBucks = Money.Dollar(5);
            tenFrancs = Money.Franc(10);
            bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
        }

        [Test]
        public void TestMultiplication()
        {
            Assert.AreEqual(Money.Dollar(10), fiveBucks.Times(2));
            Assert.AreEqual(Money.Dollar(15), fiveBucks.Times(3));
        }

        [Test]
        public void TestEquality()
        {
            Assert.True(Money.Dollar(5).Equals(Money.Dollar(5)));
            Assert.False(Money.Dollar(5).Equals(Money.Dollar(6)));
            Assert.False(Money.Dollar(5).Equals(Money.Franc(5)));
        }

        [Test]
        public void TestCurrency()
        {
            Assert.AreEqual("USD", Money.Dollar(1).Currency);
            Assert.AreEqual("CHF", Money.Franc(1).Currency);
        }

        [Test]
        public void TestSimpleAddition()
        {
            IExpression sum = fiveBucks.Plus(fiveBucks);
            Money reduced = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(10), reduced);
        }

        [Test]
        public void TestReduceSum()
        {
            IExpression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(7), result);
        }

        [Test]
        public void TestReduceMoney()
        {
            Money result = bank.Reduce(Money.Dollar(1), "USD");
            Assert.AreEqual(Money.Dollar(1), result);
        }

        [Test]
        public void TestReduceMoneyDifferentCurrency()
        {
            Money result = bank.Reduce(Money.Franc(2), "USD");
            Assert.AreEqual(Money.Dollar(1), result);
        }

        [Test]
        public void TestIdentityRate()
        {
            Assert.AreEqual(1, new Bank().Rate("USD", "USD"));
        }

        [Test]
        public void TestMixedAddition()
        {
            Money result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");
            Assert.AreEqual(Money.Dollar(10), result);
        }

        [Test]
        public void TestSumPlusMoney()
        {
            IExpression sum = new Sum(fiveBucks, tenFrancs).Plus(fiveBucks);
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(15), result);
        }

        [Test]
        public void TestSumTimes()
        {
            IExpression sum = new Sum(fiveBucks, tenFrancs).Times(2);
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(20), result);
        }
    }
}
