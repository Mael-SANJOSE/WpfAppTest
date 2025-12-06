using Microsoft.Extensions.DependencyInjection;
using WpfAppTest.UI.Services.Interfaces;
using WpfAppTest.UI.ViewModels;
using WpfAppTest.UI.ViewModels.Interfaces;

namespace WpfAppTest.UI.Services.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                CurrentViewChanged?.Invoke();
            }
        }

        public event Action CurrentViewChanged;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
            CurrentView = viewModel;
        }

        public void NavigateTo<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

            // Passer le paramètre si le ViewModel implémente INavigable
            if (viewModel is INavigable navigable)
            {
                navigable.OnNavigatedTo(parameter);
            }

            CurrentView = viewModel;
        }
    }
}
