using Car_App.Data;
using Car_App.Helpers;
using Car_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Car_App.Views
{
    public partial class AddCarWindow : Window
    {
        public AddCarWindow(DataContext dataContext)
        {
            InitializeComponent();
            this.dataContext = dataContext;
        }
        private readonly DataContext dataContext;
        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            string message = CarDataValidator.InputDataValidator(UserInput());
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK);
                return;
            }
            try
            {
                dataContext.Car.Add(UserInput());
                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                File.WriteAllTextAsync("Error.txt", ex.ToString());
                MessageBox.Show("Cannot save changes to database.", "Error", MessageBoxButton.OK);
            }
            if (string.IsNullOrEmpty(message)) MessageBox.Show("Car added succesfuly", "", MessageBoxButton.OK); BackToMenu(null, null);
        }
        private Car UserInput() => new Car()
        {
            CarId = Guid.NewGuid(),
            RegistrationNumber = RegistrationNumTextBox.Text,
            VIN = VinTextBox.Text,
            Brand = BrandTextBox.Text,
            Model = ModelTextBox.Text
        };
        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(dataContext);
            main.Show();
            Close();
        }
    }
}
