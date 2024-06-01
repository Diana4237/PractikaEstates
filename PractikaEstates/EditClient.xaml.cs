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

namespace PractikaEstates
{
    /// <summary>
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        int IdCl;
        public EditClient()
        {
            InitializeComponent();
            Add.Visibility = Visibility.Visible;
        }
        public EditClient(Client entity)
        {
            InitializeComponent();
            using (EstateEntities context = new EstateEntities())
            {
                IdCl = entity.Id_client;
                first.Text = entity.FirstName;
                last.Text = entity.LastName ;
                patronym.Text = entity.Patronymic;
                phone.Text = entity.Telephone;
                Mail.Text =entity.Mail;
            }
            Edit.Visibility = Visibility.Visible;
        }
       private void EditCl(object sender, RoutedEventArgs e)
        {
            using (EstateEntities context = new EstateEntities())
            {
                var entity = context.Client.Find(IdCl);
                entity.FirstName = first.Text;
                entity.LastName = last.Text;
                entity.Patronymic = patronym.Text;
                entity.Telephone = phone.Text;
                entity.Mail = Mail.Text;
                context.SaveChanges();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
        private void AddCl(object sender, RoutedEventArgs e)
        {
            using (EstateEntities context = new EstateEntities())
            {
                if (phone.Text!=null || Mail.Text!=null) { 
                Client entity = new Client
                {
                    FirstName = first.Text,
                    LastName = last.Text,
                    Patronymic = patronym.Text,
                    Telephone = phone.Text,
                    Mail = Mail.Text
                };
                context.Client.Add(entity);
                context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("У клиента не указаны контактные данные");
                }
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}
