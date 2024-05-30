﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace PractikaEstates
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Clients.ItemsSource=EstateEntities.GetContext().Client.ToList();
        }
        void StrEdit(object sender, RoutedEventArgs e)
        {
            var selectedItem = Clients.SelectedItem;
            var client = (Client)selectedItem;
            using (EstateEntities context = new EstateEntities())
            {
                var entity = context.Client.Find(client.Id_client);

                EditClient editClient = new EditClient(entity);
                editClient.Show();

            }
            
        }
        void AddClient(object sender, RoutedEventArgs e)
        {
            EditClient editClient = new EditClient();
            editClient.Show();
        }
    }
}
