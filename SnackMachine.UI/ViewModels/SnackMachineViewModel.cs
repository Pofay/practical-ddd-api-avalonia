using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using SnackMachine.Logic;

namespace SnackMachine.UI.ViewModels
{
    public class SnackMachineViewModel : ViewModelBase
    {
        private SnackMachineEntity _snackMachine;

        private string _message = "";
        public string Caption => "Snack Machine";
        public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
        public Money MoneyInside => _snackMachine.MoneyInside + _snackMachine.MoneyInTransaction;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ICommand InsertCentCommand { get; private set; }
        public ICommand InsertTenCentCommand { get; private set; }
        public ICommand InsertQuarterCommand { get; private set; }
        public ICommand InsertDollarCommand { get; private set; }
        public ICommand InsertFiveDollarCommand { get; private set; }
        public ICommand InsertTwentyDollarCommand { get; private set; }
        public ICommand ReturnMoneyCommand { get; private set; }
        public ICommand BuySnackCommand { get; private set; }

        public SnackMachineViewModel(SnackMachineEntity snackMachine)
        {
            _snackMachine = snackMachine;

            InsertCentCommand = new RelayCommand(() => InsertMoney(Money.Cent));
            InsertTenCentCommand = new RelayCommand(() => InsertMoney(Money.TenCent));
            InsertQuarterCommand = new RelayCommand(() => InsertMoney(Money.Quarter));
            InsertDollarCommand = new RelayCommand(() => InsertMoney(Money.Dollar));
            InsertFiveDollarCommand = new RelayCommand(() => InsertMoney(Money.FiveDollar));
            InsertTwentyDollarCommand = new RelayCommand(() => InsertMoney(Money.TwentyDollar));
            ReturnMoneyCommand = new RelayCommand(() => ReturnMoney());
            BuySnackCommand = new RelayCommand(() => BuySnack());
        }

        private void BuySnack()
        {
            _snackMachine.BuySnack();
            NotifyClient("You bought a snack");

        }

        private void ReturnMoney()
        {
            _snackMachine.ReturnMoney();
            NotifyClient("Money was returned");
        }

        private void InsertMoney(Money coinOrNote)
        {
            _snackMachine.InsertMoney(coinOrNote);
            NotifyClient(string.Format("You have inserted {0}", coinOrNote));
        }

        private void NotifyClient(string message)
        {
            OnPropertyChanged(nameof(MoneyInTransaction));
            OnPropertyChanged(nameof(MoneyInside));
            Message = message;
        }
    }
}
