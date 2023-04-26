namespace SnackMachine.Tests;

using Xunit;
using SnackMachine.Logic;
using FluentAssertions;

public class MoneyTest
{
    [Fact]
    public void SumProducesCorrectResult()
    {
        var money1 = new Money(1, 2, 3, 4, 5, 6);
        var money2 = new Money(1, 2, 3, 4, 7, 6);

        var actual = money1 + money2;

        actual.OneCentCount.Should().Be(2);
        actual.TenCentCount.Should().Be(4);
        actual.QuarterCount.Should().Be(6);
        actual.OneDollarCount.Should().Be(8);
        actual.FiveDollarCount.Should().Be(12);
        actual.TwentyDollarCount.Should().Be(12);
    }

    [Fact]
    public void TwoMoneyObjectsWithSameValuesAreEqual()
    {
        var money1 = new Money(1, 2, 3, 4, 5, 6);
        var money2 = new Money(1, 2, 3, 4, 5, 6);

        money1.Should().Be(money2);
        money1.GetHashCode().Should().Be(money2.GetHashCode());
    }

    [Fact]
    public void TwoMoneyObjectsWithDifferentValuesAreNotEqual()
    {
        var dollar = new Money(0, 0, 0, 1, 0, 0);
        var hundredCents = new Money(100, 0, 0, 0, 0, 0);

        dollar.Should().NotBe(hundredCents);
        dollar.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
    }

    [Theory]
    [InlineData(-1, 0, 0, 0, 0, 0)]
    [InlineData(0, -2, 0, 0, 0, 0)]
    [InlineData(0, 0, -3, 0, 0, 0)]
    [InlineData(0, 0, 0, -4, 0, 0)]
    [InlineData(0, 0, 0, 0, -5, 0)]
    [InlineData(0, 0, 0, 0, 0, -6)]
    public void ShouldNotBeAbleToCreateMoneyWithNegativeValues(
        int oneCentCount,
        int tenCentCount,
        int quarterCount,
        int oneDollarCount,
        int fiveDollarCount,
        int twentyDollarCount)
    {
        var action = () => new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

        action.Should().Throw<InvalidOperationException>();
    }
}
