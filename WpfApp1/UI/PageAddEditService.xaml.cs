using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Classes;
using WpfApp1.DataBase;

namespace WpfApp1.UI
{
    /// <summary>
    /// Логика взаимодействия для PageAddEditService.xaml
    /// </summary>
    public partial class PageAddEditService : Page
    {
        private Service service = new Service();

        private bool isUser;

        public PageAddEditService(Service service, bool isUser)
        {
            InitializeComponent();

            if (service != null)
            {
               this.service = service;
            }
            DataContext = this.service;
            this.isUser = isUser;
        }

        private void BtnPathImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                service.MainImagePath = openFileDialog.FileName;
                image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            var data = LanguageSchoolEntities.GetContext();

            if (Title.Text.Length == 0)
                errors.AppendLine("Нужно заполнить навзание услуги!");
            if (Cost.Text.Length == 0)
                errors.AppendLine("Нужно заполнить стоимость услуги!");
            if (DurationInSeconds.Text.Length == 0)
                errors.AppendLine("Нужно заполнить длительность услуги!");
            if (service.Discount < 0)
                errors.AppendLine("Скидка не может быть отрицательной!");
            if (service.Discount > 1)
                errors.AppendLine("Скидка не может быть больше 100%!");



            foreach(Service services in data.Service)
            {
                if (services.Title == Title.Text && services.Title.Length == Title.Text.Length)
                {
                    errors.AppendLine("Такое название услуги уже сущетсвует!");
                }
            }
                

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (service.ID == 0)
            {
                LanguageSchoolEntities.GetContext().Service.Add(service);
            }

            try
            {
                LanguageSchoolEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешное добавление услуги!","Успешно",MessageBoxButton.OK,MessageBoxImage.Information);
                Manager.MainFrame.Navigate(new PageService(isUser));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                LanguageSchoolEntities.GetContext().Service.Remove(service);
            }
        }

    }
}
