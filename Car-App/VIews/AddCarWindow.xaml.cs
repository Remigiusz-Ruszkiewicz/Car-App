using Car_App.Data;
using Car_App.Helpers;
using Car_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Car_App.Views
{
    public partial class AddCarWindow : Window
    {
        private User _currentUser;
        public AddCarWindow(DataContext dataContext,User user)
        {
            _currentUser = user;
            InitializeComponent();
            this.dataContext = dataContext;
            FillComboBox();
        }
        private readonly DataContext dataContext;
        private void FillComboBox() {
            //List<Client> clients = dataContext.Clients;
            foreach (var item in dataContext.Clients)
            {
                clientDropDown.Items.Add(item.Name+"  "+ item.Surname);
            }
        }
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
                dataContext.Cars.Add(UserInput());
                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                File.WriteAllTextAsync("Error.txt", ex.ToString());
                MessageBox.Show("Cannot save changes to database.", "Error", MessageBoxButton.OK);
            }
            if (string.IsNullOrEmpty(message)) MessageBox.Show("Car added succesfuly", "", MessageBoxButton.OK); BackToMenu(null, null);
        }
        private Car UserInput() => new()
        {
            CarId = Guid.NewGuid(),
            RegistrationNumber = RegistrationNumTextBox.Text,
            VIN = VinTextBox.Text,
            Brand = BrandTextBox.Text,
            Model = ModelTextBox.Text,
            ClientId  = Guid.NewGuid()
        };
        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            MainWindow main = new(dataContext,_currentUser);
            main.Show();
            Close();
        }
    }
}
