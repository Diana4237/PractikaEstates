using System;
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
using System.Windows.Shapes;

namespace PractikaEstates
{
    /// <summary>
    /// Логика взаимодействия для DealsWindow.xaml
    /// </summary>
    public partial class DealsWindow : Window
    {

        public DealsWindow()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            using (var context = new EstateEntities())
            {
                var query = from supplies in context.Supplies
                            from demand in supplies.Demand
                            select new
                            {
                                SupplyId = supplies.Id_supply,
                                DemandId = demand.Id_demand,
                                supplyName = supplies.Id_supply,
                                demandName = demand.Id_demand
                            };

                DealsGrid.ItemsSource = query.ToList();
            }
        }
        void DealsClick(object sender, RoutedEventArgs e)
        {
            Agents dealsWindow = new Agents();
            dealsWindow.Show();
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
        void ClientClick(object sender, RoutedEventArgs e)
        {
            MainWindow clients = new MainWindow();
            clients.Show();
            this.Close();
        }
        void EstateClick(object sender, RoutedEventArgs e)
        {
            Estates agents = new Estates();
            agents.Show();
            this.Close();
        }
        void AddClient(object sender, RoutedEventArgs e)
        {
            EditDeal editClient = new EditDeal();
            editClient.Show();
        }
        void Delete(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button?.DataContext;

            if (dataContext != null)
            {
                dynamic item = dataContext;
                int supplyId = item.SupplyId;
                int demandId = item.DemandId;

                using (var context = new EstateEntities())
                {
                    var supply = context.Supplies.Include("Demand").FirstOrDefault(u => u.Id_supply == supplyId);
                    if (supply != null)
                    {
                        var demand = supply.Demand.FirstOrDefault(c => c.Id_demand == demandId);
                        if (demand != null)
                        {
                            supply.Demand.Remove(demand);
                            context.SaveChanges();
                            LoadData();
                        }
                    }
                }
            }
        }
        void Upload(object sender, RoutedEventArgs e)
        {
            EditDeal editClient = new EditDeal();
            editClient.Show();
        }
    }
}
