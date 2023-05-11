namespace SnackMachine.Logic;

public class SnackMachineEntity : AggregateRoot
{
    private static readonly Money[] ACCEPTABLE_COINS_AND_NOTES = { Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar };
    public Money MoneyInside { get; private set; }
    public Money MoneyInTransaction { get; private set; }
    protected IList<Slot> Slots { get; set; }

    private SnackMachineEntity() { }

    public SnackMachineEntity(Guid id)
    {
        this.Id = id;
        this.MoneyInside = Money.None;
        this.MoneyInTransaction = Money.None;
        this.Slots = new List<Slot>
        {
            new Slot(this, 1),
            new Slot(this, 2),
            new Slot(this, 3)
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
        var slot = GetSlot(position);
        slot.SnackPile = slot.SnackPile.SubtractOne();
        MoneyInside += MoneyInTransaction;
        MoneyInTransaction = Money.None;
    }

    public void LoadSnacks(int position, SnackPile snackPile)
    {
        var slot = GetSlot(position);
        slot.SnackPile = snackPile;
    }

    private Slot GetSlot(int position)
    {
        return Slots.Single(x => x.Position == position);
    }

    public SnackPile GetSnackPile(int position)
    {
        return GetSlot(position).SnackPile;
    }
}
