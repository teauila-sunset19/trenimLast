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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace trenimLast
{
    /// <summary>
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        int IdUsers;
        public LogInPage()
        {
            InitializeComponent();
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text;
            string password = passwordBox.Text;
            if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(password)) {
                MessageBox.Show("Введите логин и пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrEmpty(login)) {
                MessageBox.Show("Введите логин", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (correctLogIn(login, password))
            {
                Manager.MainFrame.Navigate(new ProductsPage(IdUsers));
                loginBox.Clear();
                passwordBox.Clear();
            }
            else
            {
                MessageBox.Show("Логин или пароль не верны", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private bool correctLogIn (string login, string password)
        {
            try
            {
                var users = trenimLastEntities.GetContext().Users.FirstOrDefault(u => u.login == login && u.passwrod == password);
                IdUsers = users.id;
                return users != null;
            }
            catch
            {
                return false;
            }
        }

        private void GeustBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProductsPage(0));
            loginBox.Clear();
            passwordBox.Clear();
        }
    }
}
