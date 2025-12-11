namespace WpfAppTest.Core.Models
{
    public class Contact : ModelBase
    {        
        public int Id { get; set; }

        private string _firstname = "Veuillez saisir un prénom";
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

        private string _lastname = "Veuillez saisir un nom";
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
