using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackMachine.Logic
{
    public class Slot : Entity
    {
        public Snack Snack { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public SnackMachineEntity snackMachine { get; private set; }
        public int Position { get; private set; }

        private Slot() { }

        public Slot(SnackMachineEntity snackMachine, int position, Snack snack, int quantity, decimal price)
        {
            this.snackMachine = snackMachine;
            Position = position;
            Snack = snack;
            Quantity = quantity;
            Price = price;
        }
    }
}
