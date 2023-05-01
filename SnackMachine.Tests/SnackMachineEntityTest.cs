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
            var sut = new SnackMachineEntity();

            sut.MoneyInside.Should().Be(Money.None);
            sut.MoneyInTransaction.Should().Be(Money.None);
        }

        [Fact]
        public void ShouldEmptyMoneyInTransactionWhenInvokingReturnMoney()
        {
            var sut = new SnackMachineEntity();
            sut.InsertMoney(Money.Dollar);

            sut.ReturnMoney();

            sut.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void ShouldInsertMoneyToMoneyInTransaction()
        {
            var sut = new SnackMachineEntity();

            sut.InsertMoney(Money.Cent);
            sut.InsertMoney(Money.Dollar);

            sut.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [Fact]
        public void ShouldNotBeAbleToInsertMoreThanOneCoinOrNoteAtATime()
        {
            var sut = new SnackMachineEntity();
            var twoCent = Money.Cent + Money.Cent;

            var action = () => sut.InsertMoney(twoCent);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ShouldPutMoneyInTranstionToMoneyInsideOnBuySnack()
        {
            var sut = new SnackMachineEntity();
            sut.InsertMoney(Money.Dollar);
            sut.InsertMoney(Money.Dollar);

            sut.BuySnack();

            sut.MoneyInside.Amount.Should().Be(2m);
            sut.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void ShouldNotEmptyMoneyInsideWhenBuyingASnackAndReturningMoney()
        {
            var sut = new SnackMachineEntity();
            sut.InsertMoney(Money.Dollar);
            sut.InsertMoney(Money.Dollar);

            sut.BuySnack();
            sut.ReturnMoney();

            sut.MoneyInside.Amount.Should().Be(2m);
        }
    }
}
