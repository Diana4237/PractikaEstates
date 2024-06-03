using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для Estates.xaml
    /// </summary>
    public partial class Estates : Window
    {
        private EstateObj _currentEstates = new EstateObj();
        private District _currentDistrict = new District();
        private TypeOfEstate _currentType = new TypeOfEstate();
        public Estates()
        {
            InitializeComponent();
            DataContext = _currentEstates;
            DataContext = _currentDistrict;
            DataContext = _currentType;
            EstateGrid.ItemsSource = EstateEntities.GetContext().EstateObj.ToList();
           
        }
        void StrEdit(object sender, RoutedEventArgs e)
        {
            // (EstateObj)
            //int itemIndex = TypeGrid.SelectedIndex;
            //var item = (EstateObj)EstateGrid.Items[itemIndex];
            var item = (EstateObj)EstateGrid.SelectedItem; 
            new EditEstate(item).Show();
            Close();

        }
        void AddEs(object sender, RoutedEventArgs e)
        {
            EditEstate es=new EditEstate();
            es.Show();
            this.Close();

        }
        void DelEs(object sender, RoutedEventArgs e)
        {
            var selectedItem = EstateGrid.SelectedItem;
            var estate = (EstateObj)selectedItem;
            using (EstateEntities context = new EstateEntities())
            {

                var entity = context.EstateObj.Find(estate.Id_estate);
                if (entity != null)
                {
                   // var demandForClient = context.Demand.FirstOrDefault(d => d.Id_client == entity.Id_estate);
                    var supplyForClient = context.Supplies.FirstOrDefault(d => d.Id_client == entity.Id_estate);
                    if ( supplyForClient == null)
                    {
                        context.EstateObj.Remove(entity);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить объект, он связан с предложением");
                    }
                }
            }
            Estates mainWindow = new Estates();
            mainWindow.Show();
    }
        void ClientClick(object sender, RoutedEventArgs e)
        {
            MainWindow clients = new MainWindow();
            clients.Show();
            this.Close();
        }
        void AgentClick(object sender, RoutedEventArgs e)
        {
            Agents agents = new Agents();
            agents.Show();
            this.Close();
        }
    }
}
