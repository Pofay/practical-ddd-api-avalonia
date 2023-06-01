using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackMachine.Logic
{
    public record Money
    {
        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);

        public const decimal ONE_CENT_AMOUNT = 0.01m;
        public const decimal QUARTER_CENT_AMOUNT = 0.25m;
        public const decimal TEN_CENT_AMOUNT = 0.10m;
        public const decimal ONE_DOLLAR_AMOUNT = 1;
        public const decimal FIVE_DOLLAR_AMOUNT = 5;
        public const decimal TWENTY_DOLLAR_AMOUNT = 20;

        public int OneCentCount { get; private set; }
        public int QuarterCount { get; private set; }
        public int OneDollarCount { get; private set; }
        public int FiveDollarCount { get; private set; }
        public int TwentyDollarCount { get; private set; }
        public int TenCentCount { get; private set; }
        public decimal Amount =>
            (OneCentCount * 0.01m) +
            (TenCentCount * 0.10m) +
            (QuarterCount * 0.25m) +
            OneDollarCount +
            (FiveDollarCount * 5) +
            (TwentyDollarCount * 20);

        private Money()
        {
        }

        public Money(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount) : this()
        {
            if (oneCentCount < 0 || tenCentCount < 0 || quarterCount < 0 || oneDollarCount < 0 || fiveDollarCount < 0 || twentyDollarCount < 0)
            {
                throw new InvalidOperationException();
            }

            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new Money(
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount
            );

            return sum;
        }

        public static Money operator -(Money money1, Money money2)
        {
            Money sum = new Money(
                money1.OneCentCount - money2.OneCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.QuarterCount - money2.QuarterCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount,
                money1.TwentyDollarCount - money2.TwentyDollarCount
            );

            return sum;
        }

        public static Money operator *(Money money1, int multiplier)
        {
            Money sum = new Money(
                money1.OneCentCount * multiplier,
                money1.TenCentCount * multiplier,
                money1.QuarterCount * multiplier,
                money1.OneDollarCount * multiplier,
                money1.FiveDollarCount * multiplier,
                money1.TwentyDollarCount * multiplier);
            return sum;
        }

        public override string ToString()
        {
            if (Amount < 1)
            {
                return "¢" + (Amount * 100).ToString("0");
            }
            return "$" + Amount.ToString("0.00");
        }

        public Money AllocateCore(decimal amount)
        {
            int twentyDollarCount = GetNoteOrCoinCount(amount, TWENTY_DOLLAR_AMOUNT, TwentyDollarCount);
            amount = amount - (twentyDollarCount * 20);

            int fiveDollarCount = GetNoteOrCoinCount(amount, FIVE_DOLLAR_AMOUNT, FiveDollarCount);
            amount = amount - (fiveDollarCount * 5);

            int oneDollarCount = GetNoteOrCoinCount(amount, ONE_DOLLAR_AMOUNT, OneDollarCount);
            amount = amount - oneDollarCount;

            int quarterCount = GetNoteOrCoinCount(amount, QUARTER_CENT_AMOUNT, QuarterCount);
            amount = amount - (quarterCount * 0.25m);

            int tenCentCount = GetNoteOrCoinCount(amount, TEN_CENT_AMOUNT, TenCentCount);
            amount = amount - (tenCentCount * 0.10m);

            int oneCentCount = GetNoteOrCoinCount(amount, ONE_CENT_AMOUNT, OneDollarCount);

            return new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);
        }

        private int GetNoteOrCoinCount(decimal amount, decimal rawMoneyAmount, int noteOrCoinCount)
        {
            int noteOrCoinCountFromAmount = (int)(amount / rawMoneyAmount);
            return Math.Min(noteOrCoinCountFromAmount, noteOrCoinCount);
        }

        public bool CanAllocate(decimal amount)
        {
            var money = AllocateCore(amount);
            return money.Amount == amount;
        }

        public Money Allocate(decimal amount)
        {
            if (!CanAllocate(amount))
            {
                throw new InvalidOperationException();
            }
            return AllocateCore(amount);
        }
    }
}
