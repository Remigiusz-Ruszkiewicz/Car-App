using Car_App.Data;
using Car_App.Models;
using System;
using System.Collections.Generic;
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

namespace Car_App.VIews
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        readonly DataContext _dataContext;
        public LoginWindow(DataContext context)
        {
            _dataContext = context;
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (_dataContext.Users.Where(x => x.Login == loginTextBox.Text).FirstOrDefault() == null)
            {
                _dataContext.Users.Add(
                new User
                {
                    Login = loginTextBox.Text,
                    Password = EncryptionHelper.Encrypt(passwordBox.Password),
                    RoleId = Guid.Parse("f63a2990-440a-4dc5-b5fb-c3ca93ad6e39")
                }
            );
                _dataContext.SaveChanges(); ;
            }
            else { 
            MessageBox.Show("User Already Exist. Try another Login.");
            }
        }
        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            User user = _dataContext.Users.Where(x => x.Login == loginTextBox.Text).FirstOrDefault();
            if (user != null && EncryptionHelper.Decrypt(user.Password) == passwordBox.Password)
            {
                new MainWindow(_dataContext).Show();
                Close();
                
            }
        }
    }
}
