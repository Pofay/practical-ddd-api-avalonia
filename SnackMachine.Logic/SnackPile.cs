using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackMachine.Logic
{
    public record SnackPile
    {
        public Snack Snack { get; }
        public int Quantity { get; }
        public decimal Price { get; }

        private SnackPile() { }

        public SnackPile(Snack snack, int quantity, decimal price) : this()
        {
            if (quantity < 0)
                throw new InvalidOperationException();
            if(price < 0)
                throw new InvalidOperationException();
            if (price % 0.01m > 0)
                throw new InvalidOperationException();
            Snack = snack;
            Quantity = quantity;
            Price = price;
        }

        public SnackPile SubtractOne()
        {
            return new SnackPile(Snack, Quantity - 1, Price);
        }
    }
}
