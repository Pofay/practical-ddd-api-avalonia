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

    [Theory]
    [InlineData(0, 0, 0, 0, 0, 0, 0)]
    [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
    [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
    [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
    [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
    [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
    [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
    [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
    [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
    public void ShouldBeAbleToGetCorrectAmount(int oneCentCount,
        int tenCentCount,
        int quarterCount,
        int oneDollarCount,
        int fiveDollarCount,
        int twentyDollarCount,
        double expectedAmount)
    {
        var money = new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);
        var expected = (decimal)expectedAmount;

        var actual = money.Amount;

        actual.Should().Be(expected);
    }

    [Fact]
    public void SubtractionProducesCorrectResult()
    {
        var money1 = new Money(10, 10, 10, 10, 10, 10);
        var money2 = new Money(1, 2, 3, 4, 5, 6);

        var actual = money1 - money2;

        actual.OneCentCount.Should().Be(9);
        actual.TenCentCount.Should().Be(8);
        actual.QuarterCount.Should().Be(7);
        actual.OneDollarCount.Should().Be(6);
        actual.FiveDollarCount.Should().Be(5);
        actual.TwentyDollarCount.Should().Be(4);
    }

    [Fact]
    public void CannotSubtractMoreThanExists()
    {
        var money1 = new Money(0, 1, 0, 0, 0, 0);
        var money2 = new Money(1, 0, 0, 0, 0, 0);

        var action = () => money1 - money2;

        action.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(1, 0, 0, 0, 0, 0, "¢1")]
    [InlineData(0, 0, 0, 1, 0, 0, "$1.00")]
    [InlineData(1, 0, 0, 1, 0, 0, "$1.01")]
    [InlineData(0, 0, 2, 1, 0, 0, "$1.50")]
    public void ShouldReturnProperToStringRepresentation(
        int oneCentCount,
        int tenCentCount,
        int quarterCount,
        int oneDollarCount,
        int fiveDollarCount,
        int twentyDollarCount,
        string expectedString)
    {
        var money = new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);
        var actual = money.ToString();
        actual.Should().Be(expectedString);
    }
}
