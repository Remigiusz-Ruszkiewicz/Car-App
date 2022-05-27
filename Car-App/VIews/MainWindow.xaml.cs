using System.IO;
using System.Windows;
using Car_App.Data;
using Car_App.Views;
using Car_App.Models;
using Microsoft.Data.Sqlite;
using System.Linq;


namespace Car_App
{
    public partial class MainWindow : Window
    {
        private readonly DataContext dataContext;
        Car ActiveCar = new Car();
        public MainWindow(DataContext dataContext)
        {
            this.dataContext = dataContext;
            InitializeComponent();
            GetCars();
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            AddCarWindow addcarwindow = new AddCarWindow(dataContext);
            addcarwindow.Show();
        }
        public void GetCars()
        {
            CarDataGrid.ItemsSource = dataContext.Car.ToList();
        }

        private void UpdateCar(object s, RoutedEventArgs e)
        {
            EditStackPanel.Visibility = Visibility.Visible;
            dataContext.Update(ActiveCar);
            dataContext.SaveChanges();
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
            dataContext.Car.Remove(productToDelete);
            dataContext.SaveChanges();
            GetCars();
        }

        private void AddCarWindow(object sender, RoutedEventArgs e)
        {
            AddCarWindow addCarWindow = new AddCarWindow(dataContext);
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
            Print a = new Print(dataContext);
            a.StartPrint();
            a = null;
        }

    }
}
