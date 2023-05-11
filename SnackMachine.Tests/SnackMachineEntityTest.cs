using Xunit;
using SnackMachine.Logic;
using FluentAssertions;


namespace SnackMachine.Tests
{
    public class SnackMachineEntityTest
    {

        [Fact]
        public void ShouldNotContainAnyMoneyInsideOrMoneyInTransactionOnCreation()
        {
            var sut = new SnackMachineEntity(Guid.NewGuid());

            sut.MoneyInside.Should().Be(Money.None);
            sut.MoneyInTransaction.Should().Be(Money.None);
        }

        [Fact]
        public void ShouldEmptyMoneyInTransactionWhenInvokingReturnMoney()
        {
            var sut = new SnackMachineEntity(Guid.NewGuid());
            sut.InsertMoney(Money.Dollar);

            sut.ReturnMoney();

            sut.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void ShouldInsertMoneyToMoneyInTransaction()
        {
            var sut = new SnackMachineEntity(Guid.NewGuid());

            sut.InsertMoney(Money.Cent);
            sut.InsertMoney(Money.Dollar);

            sut.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [Fact]
        public void ShouldNotBeAbleToInsertMoreThanOneCoinOrNoteAtATime()
        {
            var sut = new SnackMachineEntity(Guid.NewGuid());
            var twoCent = Money.Cent + Money.Cent;

            var action = () => sut.InsertMoney(twoCent);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ShouldTradeInsertedMoneyForSnack()
        {
            var sut = new SnackMachineEntity(Guid.NewGuid());
            sut.LoadSnacks(1, new SnackPile(new Snack("Snack"), 10, 1m));
            sut.InsertMoney(Money.Dollar);

            sut.BuySnack(1);

            sut.MoneyInside.Amount.Should().Be(1m);
            sut.MoneyInTransaction.Amount.Should().Be(0m);
            sut.GetSnackPile(1).Quantity.Should().Be(9);
        }

        [Fact(Skip = "Working on Buy Snack change")]

        public void ShouldNotEmptyMoneyInsideWhenBuyingASnackAndReturningMoney()
        {
            var sut = new SnackMachineEntity(Guid.NewGuid());
            sut.InsertMoney(Money.Dollar);
            sut.InsertMoney(Money.Dollar);

            /*sut.BuySnack();
*/
            sut.ReturnMoney();

            sut.MoneyInside.Amount.Should().Be(2m);
        }

        [Fact]
        public void SanityCheckForEquality()
        {
            // Default
            var sut = new SnackMachineEntity(Guid.NewGuid());
            var sut2 = new SnackMachineEntity(Guid.NewGuid());
            sut.Should().NotBe(sut2);

            // Same Id
            var id = Guid.NewGuid();
            sut = new SnackMachineEntity(id);
            sut2 = new SnackMachineEntity(id);
            sut.Should().Be(sut2);

            // Reference Equality
            sut = sut2;
            sut.Should().Be(sut2);
        }
    }
}
