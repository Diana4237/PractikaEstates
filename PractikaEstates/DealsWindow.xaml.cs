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
    /// Логика взаимодействия для DealsWindow.xaml
    /// </summary>
    public partial class DealsWindow : Window
    {

        public DealsWindow()
        {
            InitializeComponent();
            using (var context = new EstateEntities())
            {
                var query = from supplies in context.Supplies
                            from demand in supplies.Demand
                            select new
                            {
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
        void DelClient(object sender, RoutedEventArgs e)
        {
            var selectedItem = DealsGrid.SelectedItem;
            using (var context = new EstateEntities())
            {
                var query = from supplies in context.Supplies
                            from demand in supplies.Demand
                            select new
                            {
                                supplyName = supplies.Id_supply,
                                demandName = demand.Id_demand
                            };
            }
            
            var client = (Agent)selectedItem;
            using (EstateEntities context = new EstateEntities())
            {

                var entity = context.Agent.Find(client.Id_agent);
                if (entity != null)
                {
                    var demandForClient = context.Demand.FirstOrDefault(d => d.Id_agent == entity.Id_agent);
                    var supplyForClient = context.Supplies.FirstOrDefault(d => d.Id_agent == entity.Id_agent);
                    if (demandForClient == null || supplyForClient == null)
                    {
                        context.Agent.Remove(entity);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить агента, он связан с потребностью или предложением");
                    }
                }
            }
            Agents mainWindow = new Agents();
            mainWindow.Show();
        }
    }
}
