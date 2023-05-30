using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SnackMachine.Logic;
using System;

namespace SnackMachine.UI.ViewModels
{
    public class SnackPileViewModel
    {
        private readonly SnackPile _snackPile;

        public string Price => _snackPile.Price.ToString("C2");
        public int Amount => _snackPile.Quantity;
        public int ImageWidth => GetImageWidth(_snackPile.Snack);
        public Bitmap Image
        {
            get
            {
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                return new Bitmap(assets.Open(new Uri("avares://SnackMachine.UI/Assets/Images/" + _snackPile.Snack.Name + ".png")));
            }
        }

        public SnackPileViewModel(SnackPile snackPile)
        {
            _snackPile = snackPile;
        }

        private int GetImageWidth(Snack snack)
        {
            if (snack == Snack.Chocolate)
                return 120;

            if (snack == Snack.Soda)
                return 70;

            if (snack == Snack.Gum)
                return 70;

            throw new ArgumentException();
        }
    }
}
