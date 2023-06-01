namespace SnackMachine.Logic;

public class SnackMachineEntity : AggregateRoot
{
    private static readonly Money[] ACCEPTABLE_COINS_AND_NOTES = { Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar };
    public Money MoneyInside { get; private set; }
    public decimal MoneyInTransaction { get; private set; }
    internal IList<Slot> Slots { get; set; }

    private SnackMachineEntity() { }

    public SnackMachineEntity(Guid id)
    {
        this.Id = id;
        this.MoneyInside = Money.None;
        this.MoneyInTransaction = 0;
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
        {
            throw new InvalidOperationException();
        }

        MoneyInTransaction += money.Amount;
        MoneyInside += money;
    }

    public void LoadMoney(Money money)
    {
        MoneyInside += money;
    }

    public void ReturnMoney()
    {
        var moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
        MoneyInside -= moneyToReturn;
        MoneyInTransaction = 0;
    }

    public string CanBuySnack(int position)
    {
        var snackPile = GetSnackPile(position);

        if (snackPile.Quantity == 0)
        {
            return "The snack pile is empty";
        }
        if (MoneyInTransaction < snackPile.Price)
        {
            return "Not enough money";
        }
        if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
        {
            return "Not enough change";
        }

        return string.Empty;
    }

    public void BuySnack(int position)
    {
        if (CanBuySnack(position) != string.Empty)
        {
            throw new InvalidOperationException();
        }
        var slot = GetSlot(position);
        var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
        slot.SnackPile = slot.SnackPile.SubtractOne();
        MoneyInside -= change;
        MoneyInTransaction = 0;
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

    public IReadOnlyList<SnackPile> GetAllSnackPiles()
    {
        return Slots.OrderBy(x => x.Position).Select(x => x.SnackPile).ToList();
    }
}
