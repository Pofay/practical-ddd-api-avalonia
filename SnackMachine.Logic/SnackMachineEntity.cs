namespace SnackMachine.Logic;

public class SnackMachineEntity : AggregateRoot
{
    private static readonly Money[] ACCEPTABLE_COINS_AND_NOTES = { Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar };
    public Money MoneyInside { get; private set; }
    public Money MoneyInTransaction { get; private set; }
    public IList<Slot> Slots { get; private set; }

    private SnackMachineEntity() { }

    public SnackMachineEntity(Guid id)
    {
        this.Id = id;
        this.MoneyInside = Money.None;
        this.MoneyInTransaction = Money.None;
        this.Slots = new List<Slot>
        {
            new Slot(this, 1, null, 0, 0m),
            new Slot(this, 2, null, 0, 0m),
            new Slot(this, 3, null, 0, 0m),
        };
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

    public void BuySnack(int position)
    {
        var slot = Slots.Single(x => x.Position == position);
        slot.Quantity--;
        MoneyInside += MoneyInTransaction;
        MoneyInTransaction = Money.None;
    }

    public void LoadSnacks(int position, Snack snack, int quantity, decimal price)
    {
        var slot = Slots.Single(x => x.Position == position);
        slot.Snack = snack;
        slot.Quantity = quantity;
        slot.Price = price;
    }
}
