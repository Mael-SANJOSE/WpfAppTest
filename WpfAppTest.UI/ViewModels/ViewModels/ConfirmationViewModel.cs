using System.Windows.Input;
using WpfAppTest.UI.Services.Interfaces;

namespace WpfAppTest.UI.ViewModels.ViewModels
{
    public class ConfirmationViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        public ICommand OkCommand { get; }

        public ConfirmationViewModel(string title, IDialogService dialogService)
        {
            _title = title;
            _dialogService = dialogService;

            OkCommand = new RelayCommand(CloseWindow);
        }

        //public Action<bool?> CloseAction { get; set; }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private void CloseWindow()
        {
            _dialogService.CloseDialog(this, true);
        }
    }
}
