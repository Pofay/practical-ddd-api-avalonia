namespace SnackMachine.Tests;

using Xunit;
using SnackMachine.Logic;

public class MoneyTest
{
    [Fact]
    public void SumProducesCorrectResult()
    {
        var money1 = new Money(1, 2, 3, 4, 5, 6);
        var money2 = new Money(1, 2, 3, 4, 7, 6);

        var actual = money1 + money2;

        Assert.Equal(2, actual.OneCentCount);
    }
}
