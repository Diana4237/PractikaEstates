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
    /// Логика взаимодействия для EditDeal.xaml
    /// </summary>
    public partial class EditDeal : Window
    {
        public EditDeal()
        {
            InitializeComponent();
          
            Add.Visibility = Visibility.Visible;
            using (var context = new EstateEntities())
            {
                var query = from supplies in context.Supplies
                            from demand in supplies.Demand
                            select new
                            {
                                SupplyId = supplies.Id_supply,
                                DemandId = demand.Id_demand
                            };

                var allSuppliesIds = context.Supplies.Select(x => x.Id_supply).ToList();
                var allDemandIds = context.Demand.Select(x => x.Id_demand).ToList();
                var suppliesIdsNotInQuery = allSuppliesIds.Except(query.Select(x => x.SupplyId)).ToList();
                var demandIdsNotInQuery = allDemandIds.Except(query.Select(x => x.DemandId)).ToList();
                Sup.ItemsSource = suppliesIdsNotInQuery;
                Dem.ItemsSource = demandIdsNotInQuery;
            }
        }
       void LoadData() { }
        private void EditSup(object sender, RoutedEventArgs e)
        {
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            DealsWindow agents = new DealsWindow();
            agents.Show();
            this.Close();
        }
        private void AddSup(object sender, RoutedEventArgs e)
        {
            int supplyId =(int)Sup.SelectedItem;
            int demandId = (int)Dem.SelectedItem;
            using (var context = new EstateEntities())
            {
                var supply = context.Supplies.Include("Demand").FirstOrDefault(u => u.Id_supply == supplyId);
                var demand = context.Demand.Include("Supplies").FirstOrDefault(d => d.Id_demand == demandId);
                if (supply != null )
                {
                    supply.Demand.Add(demand);
                    context.SaveChanges();
                  DealsWindow mainWindow = new DealsWindow();
                   mainWindow.Show();
                    this.Close();
                }
            }
        }
    }
}
