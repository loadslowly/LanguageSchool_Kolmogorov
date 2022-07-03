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
using WpfApp1.Classes;
using WpfApp1.DataBase;

namespace WpfApp1.UI
{
    /// <summary>
    /// Логика взаимодействия для PageAuthorization.xaml
    /// </summary>
    public partial class PageAuthorization : Page
    {
        public PageAuthorization()
        {
            InitializeComponent();
        }

        private void UserConfirm_Click(object sender, RoutedEventArgs e)
        {
            var data = LanguageSchoolEntities.GetContext();

            var selectedUsers = from client in data.Client
                                where UserLogin.Text == client.Login
                                where UserPasswordBox.Password == client.Password
                                select client;

            if (selectedUsers == null || !selectedUsers.Any())
            {
                MessageBox.Show("Неверный логин или пароль");
            }
            else if(UserLogin.Text == "admin" && UserPasswordBox.Password == "admin")
            {
                MessageBox.Show("Добро пожаловать, admin");
                Manager.MainFrame.Navigate(new PageService(false));
            }
            else
            {
                MessageBox.Show($"Добро пожаловать, {UserLogin.Text}");
                Manager.MainFrame.Navigate(new PageService(true));
            }

        }

        private void UserCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (UserCheckBox.IsChecked.Value)
            {
                UserPasswordBox.Visibility = Visibility.Hidden;
                UserPassword.Text = UserPasswordBox.Password;
                UserPassword.Visibility = Visibility.Visible;
            }
            else
            {
                UserPassword.Visibility = Visibility.Hidden;
                UserPasswordBox.Password = UserPassword.Text;
                UserPasswordBox.Visibility = Visibility.Visible;
            }
        }
    }
}
