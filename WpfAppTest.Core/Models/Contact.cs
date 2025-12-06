namespace WpfAppTest.Core.Models
{
    public class Contact : ModelBase
    {
        private string _firstname = "Prénom";
        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string _lastname = "Nom";
        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string FullName => $"{_firstname} {_lastname}";
    }
}
