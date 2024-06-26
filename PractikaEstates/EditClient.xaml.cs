﻿using System;
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
using System.Windows.Threading;

namespace PractikaEstates
{
    /// <summary>
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        int IdCl;
        private Client _currentClient = new Client();
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
            if (entity == null) return;
            _currentClient = entity;
            DataContext = _currentClient;
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow agents = new MainWindow();
            agents.Show();
            this.Close();
        }
        private void EditCl(object sender, RoutedEventArgs e)
        {
            //using (EstateEntities context = new EstateEntities())
            //{
            //    var entity = context.Client.Find(IdCl);
            //    entity.FirstName = first.Text;
            //    entity.LastName = last.Text;
            //    entity.Patronymic = patronym.Text;
            //    entity.Telephone = phone.Text;
            //    entity.Mail = Mail.Text;
            //    context.SaveChanges();


            //}
            EstateEntities.GetContext().SaveChanges();
            new MainWindow().Show();
            Close();
            //EstateEntities.GetContext().SaveChanges();
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(2); // Задержка в 2 секунды
            //timer.Tick += (sender1, e1) =>
            //{
            //    // Открыть окно здесь
              
            //    MainWindow mainWindow = new MainWindow();
            //    mainWindow.Show();
             

            //    // Остановить таймер после открытия окна
            //    timer.Stop();
            //};

            //timer.Start();

            ////MainWindow mainWindow = new MainWindow();
            ////mainWindow.Show();
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
                this.Close();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}
