

using System.Windows.Input;
using System.Windows.Navigation;
using WpfAppTest.Core.FunctionalServices.Interfaces;
using WpfAppTest.Core.Models;
using WpfAppTest.UI.Services.Interfaces;

namespace WpfAppTest.UI.ViewModels.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //private readonly IContactService _contactService;

        //public MainViewModel(IContactService contactService)
        //{
        //    _contactService = contactService;

        //    LoadContact();
        //}

        //private async void LoadContact()
        //{
        //    Contact = await _contactService.GetFirstContactAsync();
        //}

        //private Contact _contact;

        //public Contact Contact
        //{
        //    get => _contact;
        //    set
        //    {
        //        _contact = value;
        //        OnPropertyChanged();
        //    }
        //}

        private readonly INavigationService _navigationService;
        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        //public ICommand NavigateToDashboardCommand { get; }
        //public ICommand NavigateToContactListCommand { get; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            // S'abonner aux changements de vue
            _navigationService.CurrentViewChanged += OnCurrentViewChanged;

            //NavigateToDashboardCommand = new RelayCommand(() => _navigationService.NavigateTo<DashboardViewModel>());
            //NavigateToContactListCommand = new RelayCommand(() => _navigationService.NavigateTo<ContactListViewModel>());

            // Navigation initiale
            _navigationService.NavigateTo<ContactListViewModel>();
        }

        private void OnCurrentViewChanged()
        {
            CurrentView = _navigationService.CurrentView;
        }
    }
}
