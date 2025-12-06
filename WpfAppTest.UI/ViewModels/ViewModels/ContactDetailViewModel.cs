using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;
using System.Windows.Input;
using WpfAppTest.Core.FunctionalServices.Interfaces;
using WpfAppTest.Core.Models;
using WpfAppTest.UI.Messages;
using WpfAppTest.UI.Services.Interfaces;
using WpfAppTest.UI.ViewModels.Interfaces;

namespace WpfAppTest.UI.ViewModels.ViewModels
{
    public class ContactDetailViewModel : ViewModelBase, INavigable
    {
        private readonly IContactService _contactService;
        private readonly INavigationService _navigationService;
        private readonly IMessenger _messenger;

        public ICommand SaveCommand { get; }

        public ContactDetailViewModel(
            IContactService contactService,
            INavigationService navigationService,
            IMessenger messenger)
        {
            _contactService = contactService;
            _navigationService = navigationService;
            _messenger = messenger;

            SaveCommand = new RelayCommand(Save);
        }

        private async void Save()
        {
            if (Contact.Firstname.IsNullOrEmpty())
                await _contactService.CreateAsync(Contact);
            else
                await _contactService.Update(Contact);

            // Envoyer un message pour notifier les autres vues
            _messenger.Send(new ContactSavedMessage(Contact));

            NavigateBack();
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Contact contact)
            {
                // Mode édition
                Contact = contact;
                Title = $"Modifier {contact.FullName}";
            }
            else
            {
                // Mode création
                Contact = new Contact();
                Title = "Nouveau contact";
            }
        }

        private void NavigateBack()
        {
            _navigationService.NavigateTo<ContactListViewModel>();
        }

        private Contact _contact;

        public Contact Contact
        {
            get => _contact;
            set
            {
                _contact = value;
                OnPropertyChanged();
            }
        }

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }
}
