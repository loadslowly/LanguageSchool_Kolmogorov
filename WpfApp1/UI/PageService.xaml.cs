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
using WpfApp1.UC;

namespace WpfApp1.UI
{
    /// <summary>
    /// Логика взаимодействия для PageService.xaml
    /// </summary>
    public partial class PageService : Page
    {
        private bool isUser;

        private LanguageSchoolEntities data = LanguageSchoolEntities.GetContext();

        public PageService(bool isUser)
        {
            InitializeComponent();

            this.isUser = isUser;

            if (isUser)
            {
                BtnAdd.Visibility = Visibility.Hidden;
            }

            foreach (Service service in data.Service)
            {
                UCList.Items.Add(new ControlService(service, isUser));
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var searchService = from service in data.Service
                                where service.Title.StartsWith(TextSearch.Text)
                                select service;

            UCList.Items.Clear();
            foreach (Service service in searchService)
            {
                UCList.Items.Add(new ControlService(service, isUser));
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TextSearch.Text = "";
            UCList.Items.Clear();
            foreach (Service service in data.Service)
            {
                UCList.Items.Add(new ControlService(service, isUser));
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAddEditService(null,isUser));
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if(CheckBox.IsChecked.Value)
            {
                var searchService = from service in data.Service
                                    orderby service.Cost
                                    select service;
                UCList.Items.Clear();
                foreach (Service service in searchService)
                {
                    UCList.Items.Add(new ControlService(service, isUser));
                }
            }
            else
            {
                UCList.Items.Clear();
                foreach (Service service in data.Service)
                {
                    UCList.Items.Add(new ControlService(service, isUser));
                }
            }
        }
    }
}
