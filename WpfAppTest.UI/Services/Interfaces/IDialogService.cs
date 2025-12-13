using WpfAppTest.UI.ViewModels;

namespace WpfAppTest.UI.Services.Interfaces
{
    public interface IDialogService
    {
        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : ViewModelBase;

        void CloseDialog<TViewModel>(TViewModel viewModel, bool? dialogResult = null) where TViewModel : ViewModelBase;
    }
}
