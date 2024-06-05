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
    /// Логика взаимодействия для Demands.xaml
    /// </summary>
    public partial class Demands : Window
    {

        private Demand _currentStudent = new Demand();
        public Demands()
        {
            InitializeComponent();
            DataContext = _currentStudent;
            DemandGrid.ItemsSource = EstateEntities.GetContext().Demand.ToList();
        }
        void StrEdit(object sender, RoutedEventArgs e)
        {

            var item = (Demand)DemandGrid.SelectedItem; new EditDemand(item).Show();
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
        void AddDemand(object sender, RoutedEventArgs e)
        {
            EditDemand editDem = new EditDemand();
            editDem.Show();
        }
        void DelDemand(object sender, RoutedEventArgs e)
        {
            bool flag=false;
            var selectedItem = DemandGrid.SelectedItem;
            var demands = (Demand)selectedItem;

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
                    if(result.demandName != demands.Id_demand)
                    {
                       flag= true; 
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить потребность, он связан со сделкой");
                    }
                }

            }
            using (EstateEntities context = new EstateEntities())
            {
                if(flag)
                { 
                var entity = context.Demand.Find(demands.Id_demand);
                context.Demand.Remove(entity);
                context.SaveChanges();
                }
            }
                Demands mainWindow = new Demands();
            mainWindow.Show();
        }

    }
}

