using System.Windows;
using WpfAppTest.UI.Services.Interfaces;
using WpfAppTest.UI.ViewModels;
using WpfAppTest.UI.ViewModels.ViewModels;
using WpfAppTest.UI.Views;

namespace WpfAppTest.UI.Services.Services
{
    public class DialogService : IDialogService
    {
        // Dictionnaire pour associer ViewModels et Vues
        private readonly Dictionary<Type, Type> _viewModelViewMapping = new();

        // Dictionnaire pour tracer les fenêtres ouvertes
        private readonly Dictionary<object, Window> _openWindows = new();

        public DialogService()
        {
            // Enregistrer les associations ViewModel -> View
            RegisterDialog<DialogViewModel, DialogView>();
            //RegisterDialog<CustomConfirmViewModel, CustomConfirmDialog>();
            //RegisterDialog<ProgressDialogViewModel, ProgressDialog>();
        }

        // Enregistrer une association ViewModel/View
        private void RegisterDialog<TViewModel, TView>()
            where TViewModel : ViewModelBase
            where TView : Window
        {
            _viewModelViewMapping[typeof(TViewModel)] = typeof(TView);
        }

        //public void ShowMessage(string message, string title)
        //{
        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : ViewModelBase
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var viewModelType = viewModel.GetType();

            if (!_viewModelViewMapping.TryGetValue(viewModelType, out var viewType))
            {
                throw new InvalidOperationException(
                    $"Aucune vue enregistrée pour le ViewModel {viewModelType.Name}");
            }

            Window window = (Window)Activator.CreateInstance(viewType)!;            
            window.DataContext = viewModel;
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            // Enregistrer la fenêtre ouverte
            _openWindows[viewModel] = window;

            // Nettoyer lors de la fermeture
            window.Closed += (s, e) => _openWindows.Remove(viewModel);

            return window.ShowDialog();
        }

        // Fermer un dialogue depuis le ViewModel
        public void CloseDialog<TViewModel>(TViewModel viewModel, bool? dialogResult = null) where TViewModel : ViewModelBase
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (_openWindows.TryGetValue(viewModel, out Window? window))
            {
                if (dialogResult.HasValue)
                {
                    window.DialogResult = dialogResult;
                }
                else
                {
                    window.Close();
                }
            }
        }

        //MessageBox.Show(message, title);
    }
}

