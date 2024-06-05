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
            bool flag = false;
            var selectedItem = SupplyGrid.SelectedItem;
            var suppls = (Supplies)selectedItem;

            using (var context = new EstateEntities())
            {
                var query = from supplies in context.Supplies
                            from demand in supplies.Demand
                            select new
                            {
                                supplyName = supplies.Id_supply,
                                demandName = demand.Id_demand
                            };
                foreach (var result in query)
                {
                    if (result.supplyName != suppls.Id_supply)
                    {
                        flag = true;
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить предложение, он связан со сделкой");
                    }
                }

            }
            using (EstateEntities context = new EstateEntities())
            {
                if (flag)
                {
                    var entity = context.Supplies.Find(suppls.Id_supply);
                    context.Supplies.Remove(entity);
                    context.SaveChanges();
                }
            }
            Supply mainWindow = new Supply();
            mainWindow.Show();
        }

    }
}
