using System.IO;
using System.Windows;
using Car_App.Data;
using Car_App.Views;
using Car_App.Models;
using Microsoft.Data.Sqlite;
using System.Linq;
using System;
using Car_App.VIews;

namespace Car_App
{
    public partial class MainWindow : Window
    {
        readonly DataContext context;
        private User _currentUser;
        Car ActiveCar = new();
        public MainWindow(DataContext dataContext,User user)
        {
            _currentUser = user;
            context = dataContext;
            InitializeComponent();
            GetCars();
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            AddCarWindow addcarwindow = new(context,_currentUser);
            addcarwindow.Show();
        }
        public void GetCars()
        {

            CarDataGrid.ItemsSource = context.Cars.ToList();
            if (_currentUser.RoleId == Guid.Parse("f63a2990-440a-4dc5-b5fb-c3ca93ad6e39"))
            {
                addButton.Visibility = Visibility.Collapsed;
                refreshButton.Visibility = Visibility.Collapsed;
                CarDataGrid.Columns.RemoveAt(5);
                CarDataGrid.Columns.RemoveAt(5);
            }
        }

        private void UpdateCar(object s, RoutedEventArgs e)
        {
            EditStackPanel.Visibility = Visibility.Visible;
            context.Update(ActiveCar);
            context.SaveChanges();
            GetCars();
            ClearUpdateCarGrid();
            EditStackPanel.Visibility = Visibility.Hidden;
        }
        private void ClearUpdateCarGrid()
        {
            EditStackPanel.DataContext = null;
            EditStackPanel.IsEnabled = false;
        }
        private void SelectCarToEdit(object s, RoutedEventArgs e)
        {
            EditStackPanel.Visibility = Visibility.Visible;
            ActiveCar = (s as FrameworkElement).DataContext as Car;
            EditStackPanel.DataContext = ActiveCar;
            EditStackPanel.IsEnabled = true;
        }

        private void DeleteCar(object s, RoutedEventArgs e)
        {
            var productToDelete = (s as FrameworkElement).DataContext as Car;
            context.Cars.Remove(productToDelete);
            context.SaveChanges();
            GetCars();
        }

        private void AddCarWindow(object sender, RoutedEventArgs e)
        {
            AddCarWindow addCarWindow = new(context,_currentUser);
            addCarWindow.Show();
            Close();
        }

        private void RefreshGridButton(object sender, RoutedEventArgs e)
        {
            GetCars();
            EditStackPanel.Visibility = Visibility.Hidden;
        }
        private void PrintGridButton(object sender, RoutedEventArgs e)
        {
            Print a = new(context);
            a.StartPrint();
        }

        private void LogoutButton(object sender, RoutedEventArgs e)
        {
            new LoginWindow(context).Show();
            Close();
        }
    }
}
