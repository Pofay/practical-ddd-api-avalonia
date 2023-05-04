namespace SnackMachine.Logic;

public sealed class SnackMachineEntity : Entity
{
    private static readonly Money[] ACCEPTABLE_COINS_AND_NOTES = { Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar };
    public Money MoneyInside { get; private set; } = Money.None;
    public Money MoneyInTransaction { get; private set; } = Money.None;

    public SnackMachineEntity(Guid id)
    {
        this.Id = id;
    }

    public void InsertMoney(Money money)
    {
        if (!ACCEPTABLE_COINS_AND_NOTES.Contains(money))
            throw new InvalidOperationException();

        MoneyInTransaction += money;
    }

    public void ReturnMoney()
    {
        MoneyInTransaction = Money.None;
    }

    public void BuySnack()
    {
        MoneyInside += MoneyInTransaction;
        MoneyInTransaction = Money.None;
    }
}
