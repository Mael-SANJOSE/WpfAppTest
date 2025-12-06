using WpfAppTest.UI.ViewModels;

namespace WpfAppTest.UI.Services.Interfaces
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
        void NavigateTo<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        event Action CurrentViewChanged;
    }
}
