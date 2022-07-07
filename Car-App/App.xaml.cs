using Car_App.Data;
using Car_App.VIews;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Car_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            services.AddDbContext<DataContext>(options => options.UseSqlite("Data Source=CompanyCars.db"));
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoginWindow>();
            serviceProvider = services.BuildServiceProvider();
        }

        private void OnStartup(object s, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<LoginWindow>();
            mainWindow.Show();
        }
    }
}