using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfAppTest.Core.FunctionalServices.Interfaces;
using WpfAppTest.Core.Models;
using WpfAppTest.UI.Enums;
using WpfAppTest.UI.Messages;
using WpfAppTest.UI.Services.Interfaces;
using WpfAppTest.UI.Services.Services;

namespace WpfAppTest.UI.ViewModels.ViewModels
{
    public class ContactListViewModel : ViewModelBase
    {
        private readonly IContactService _contactService;
        private readonly INavigationService _navigationService;
        private readonly IMessengerService _messenger;
        private readonly IDialogService _dialogService;
        private Contact _selectedContact;

        public ObservableCollection<Contact> Contacts { get; set; }

        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddContactCommand { get; }
        public ICommand EditContactCommand { get; }
        public ICommand DeleteContactCommand { get; }
        public ICommand OpenMessageBoxCommand { get; }

        public ContactListViewModel(
            IContactService contactService,
            INavigationService navigationService,
            IMessengerService messenger,
            IDialogService dialogService)
        {
            _contactService = contactService;
            _navigationService = navigationService;
            _messenger = messenger;
            _dialogService = dialogService;

            Contacts = new ObservableCollection<Contact>();

            AddContactCommand = new RelayCommand(AddContact);
            EditContactCommand = new RelayCommand<Contact>(EditContact);
            DeleteContactCommand = new RelayCommand<Contact>(DeleteContact);
            OpenMessageBoxCommand = new RelayCommand(OpenMessageBox);

            // S'abonner aux messages
            _messenger.Register<ContactSavedMessage>(this, OnContactSaved);

            LoadContacts();
        }

        private async void LoadContacts()
        {
            List<Contact> contacts = await _contactService.GetAllContactsAsync();
            Contacts.Clear();
            foreach (Contact contact in contacts)
            {
                Contacts.Add(contact);
            }
        }

        private void AddContact()
        {
            _navigationService.NavigateTo<ContactDetailViewModel>(ScreenMode.Creation);
        }

        private void EditContact(Contact contact)
        {
            if (contact != null)
            {
                _navigationService.NavigateTo<ContactDetailViewModel>(ScreenMode.Modification, contact);
            }
        }

        private async void DeleteContact(Contact contact)
        {
            if (contact != null)
            {
                var result = MessageBox.Show(
                    $"Voulez-vous vraiment supprimer {contact.FullName} ?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await _contactService.Delete(contact);
                    Contacts.Remove(contact);
                }
            }
        }

        private void OpenMessageBox()
        {
            ConfirmationViewModel dialogViewModel = new ConfirmationViewModel("Test titre", _dialogService);
            _dialogService.ShowDialog(dialogViewModel);
        }

        private void OnContactSaved(ContactSavedMessage message)
        {
            // Recharger la liste après sauvegarde
            LoadContacts();
        }
    }
}
