using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using SharedKernel;
using SnackMachine.Logic;

namespace SnackMachine.UI.ViewModels
{
    public class SnackMachineViewModel : ViewModelBase
    {
        private SnackMachineEntity _snackMachine;
        private SnackMachineRepository _repository;
        private string _message = "";
        public string Caption => "Snack Machine";
        public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
        public Money MoneyInside => _snackMachine.MoneyInside;

        public IReadOnlyList<SnackPileViewModel> Piles
        {
            get
            {
                return _snackMachine.GetAllSnackPiles()
                       .Select(x => new SnackPileViewModel(x))
                       .ToList();
            }
        }

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

        public SnackMachineViewModel(SnackMachineEntity snackMachine, SnackMachineRepository repository)
        {
            _snackMachine = snackMachine;
            _repository = repository;

            InsertCentCommand = new RelayCommand(() => InsertMoney(Money.Cent));
            InsertTenCentCommand = new RelayCommand(() => InsertMoney(Money.TenCent));
            InsertQuarterCommand = new RelayCommand(() => InsertMoney(Money.Quarter));
            InsertDollarCommand = new RelayCommand(() => InsertMoney(Money.Dollar));
            InsertFiveDollarCommand = new RelayCommand(() => InsertMoney(Money.FiveDollar));
            InsertTwentyDollarCommand = new RelayCommand(() => InsertMoney(Money.TwentyDollar));
            ReturnMoneyCommand = new RelayCommand(() => ReturnMoney());
            BuySnackCommand = new RelayCommand<string>(BuySnack);
        }

        private void BuySnack(string snackPilePosition)
        {
            int position = int.Parse(snackPilePosition);

            var error = _snackMachine.CanBuySnack(position);
            if (error != string.Empty)
            {
                NotifyClient(error);
                return;
            }
            _snackMachine.BuySnack(position);
            _repository.Save(_snackMachine);
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
            Message = message;
            OnPropertyChanged(nameof(MoneyInTransaction));
            OnPropertyChanged(nameof(MoneyInside));
            OnPropertyChanged(nameof(Piles));
        }
    }
}
