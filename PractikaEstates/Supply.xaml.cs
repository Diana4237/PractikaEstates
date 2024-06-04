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
    /// Логика взаимодействия для Supply.xaml
    /// </summary>
    public partial class Supply : Window
    {
        private Supplies _currentStudent = new Supplies();
        public Supply()
        {
            InitializeComponent();
            DataContext = _currentStudent;
            SupplyGrid.ItemsSource = EstateEntities.GetContext().Supplies.ToList();
        }
        void StrEdit(object sender, RoutedEventArgs e)
        {

            var item = (Supplies)SupplyGrid.SelectedItem; new EditSupply(item).Show();
            Close();

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
        void EstateClick(object sender, RoutedEventArgs e)
        {
            Estates agents = new Estates();
            agents.Show();
            this.Close();
        }
        void SuppliesClick(object sender, RoutedEventArgs e)
        {
            Supply agent = new Supply();
            agent.Show();
            this.Close();
        }

        void DemandClick(object sender, RoutedEventArgs e)
        {
            Demands agent = new Demands();
            agent.Show();
            this.Close();
        }
        void AddSupply(object sender, RoutedEventArgs e)
        {
            EditSupply editClient = new EditSupply();
            editClient.Show();
        }
        void DelSupply(object sender, RoutedEventArgs e)
        {
            //var selectedItem = SupplyGrid.SelectedItem;
            //var supply = (Supplies)selectedItem;
            //using (EstateEntities context = new EstateEntities())
            //{

            //    var entity = context.Supplies.Find(supply.Id_supply);
            //    if (entity != null)
            //    {
            //        var deals = context.Deals.FirstOrDefault(d => d.Id_supply == entity.Id_supply);

            //        if (deals == null)
            //        {
            //            context.Supplies.Remove(entity);
            //            context.SaveChanges();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Невозможно удалить агента, он связан с потребностью или предложением");
            //        }
            //    }
            //}
            //Agents mainWindow = new Agents();
            //mainWindow.Show();
        }

    }
}
