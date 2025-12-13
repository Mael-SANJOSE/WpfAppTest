using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfAppTest.Core.FunctionalServices.Interfaces;
using WpfAppTest.Core.FunctionalServices.Services;
using WpfAppTest.Data.Context;
using WpfAppTest.Data.Repositories.Interfaces.Common;
using WpfAppTest.Data.Repositories.Services.Common;
using WpfAppTest.UI.Services.Interfaces;
using WpfAppTest.UI.Services.Services;
using WpfAppTest.UI.ViewModels.ViewModels;
using WpfAppTest.UI.Views;

namespace WpfAppTest.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        //public App()
        //{
            
        //}

        private void ConfigureServices(IServiceCollection services)
        {
            #region AutoMapper
            //services.AddAutoMapper(typeof(MappingProfile));
            #endregion AutoMapper

            #region EntityFrameworkService
            services.AddDbContext<ContactsContext>(options => options.UseSqlServer(@"Server=ZEPHYR\SQLSERVERMSJ;Database=CONTACTS;Trusted_Connection=True;Encrypt=False;"));
            #endregion EntityFrameworkService

            // Enregistrement des services de navigation, messagerie et dialogue
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IMessengerService, MessengerService>();
            services.AddSingleton<IDialogService, DialogService>();

            // Enregistrement des repositories
            services.AddScoped<IContactRepository, ContactRepository>();

            // Enregistrement des services
            services.AddScoped<IContactService, ContactService>();

            // Enregistrement des ViewModels
            services.AddTransient<MainViewModel>();
            //services.AddTransient<DashboardViewModel>();
            services.AddTransient<ContactListViewModel>();
            services.AddTransient<ContactDetailViewModel>();

            services.AddTransient<ConfirmationViewModel>();

            // Enregistrement des Views
            services.AddTransient<MainWindow>();
            services.AddTransient<DialogView>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();

            //// Créer/Migrer la base de données au démarrage
            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<ContactDbContext>();
            //    await context.Database.EnsureCreatedAsync();
            //}

            MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }
}
