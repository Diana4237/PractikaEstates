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
    /// Логика взаимодействия для EditDemand.xaml
    /// </summary>
    public partial class EditDemand : Window
    {
        int IdDem;
        private Demand _currentSupp = new Demand();
        public EditDemand()
        {
            InitializeComponent();
            Add.Visibility = Visibility.Visible;
            List<string> listClient = new List<string>();
            foreach (var item in EstateEntities.GetContext().Client.ToList())
            {
                listClient.Add(item.Id_client.ToString());
            }

            List<string> listAgent = new List<string>();
            foreach (var item in EstateEntities.GetContext().Agent.ToList())
            {
                listAgent.Add(item.Id_agent.ToString());
            }

            List<string> listEstate = new List<string>();
            foreach (var item in EstateEntities.GetContext().TypeOfEstate.ToList())
            {
                listEstate.Add(item.Id_type.ToString());
            }

            List<string> listland = new List<string>();
            foreach (var item in EstateEntities.GetContext().DemandOfLand.ToList())
            {
                listland.Add(item.Id_demandLand.ToString());
            }

            List<string> listflat = new List<string>();
            foreach (var item in EstateEntities.GetContext().DemandOfFlat.ToList())
            {
                listflat.Add(item.Id_demandFlat.ToString());
            }

            List<string> listhouse = new List<string>();
            foreach (var item in EstateEntities.GetContext().DemandOfHouse.ToList())
            {
                listhouse.Add(item.Id_demandHouse.ToString());
            }

            Client.ItemsSource = listClient;
            Agent1.ItemsSource = listAgent;
            Estate1.ItemsSource = listEstate;
            land.ItemsSource = listland;
            house.ItemsSource = listhouse;
            flat.ItemsSource = listflat;


            DataContext = _currentSupp;
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Demands agents = new Demands();
            agents.Show();
            this.Close();
        }
        private void EditDem(object sender, RoutedEventArgs e)
        {
            using (EstateEntities context = new EstateEntities())
            {
                var entity = context.Demand.Find(IdDem);
                entity.MinPrice = int.Parse(PriceMin.Text);
                entity.MaxPrice = int.Parse(PriceMax.Text);
                entity.Id_client = (string)Client.SelectedItem != null ? int.Parse((string)Client.SelectedItem) : (int?)null;
                entity.Id_agent = (string)Agent1.SelectedItem != null ? int.Parse((string)Agent1.SelectedItem) : (int?)null;
                entity.Id_type = (string)Estate1.SelectedItem != null ? int.Parse((string)Estate1.SelectedItem) : (int?)null;
                entity.Adress = adress.Text;
                entity.Id_demand_flat = (string)flat.SelectedItem != null ? int.Parse((string)flat.SelectedItem) : (int?)null;
                entity.Id_demand_land = (string)land.SelectedItem != null ? int.Parse((string)land.SelectedItem) : (int?)null;
                entity.Id_demand_house = (string)house.SelectedItem != null ? int.Parse((string)house.SelectedItem) : (int?)null;

                context.SaveChanges();

            }

            EstateEntities.GetContext().SaveChanges();
            new Supply().Show();
            Close();
        }
        public EditDemand(Demand entity)
        {
            InitializeComponent();

            List<string> listClient = new List<string>();
            foreach (var item in EstateEntities.GetContext().Client.ToList())
            {
                listClient.Add(item.Id_client.ToString());
            }

            List<string> listAgent = new List<string>();
            foreach (var item in EstateEntities.GetContext().Agent.ToList())
            {
                listAgent.Add(item.Id_agent.ToString());
            }

            List<string> listEstate = new List<string>();
            foreach (var item in EstateEntities.GetContext().EstateObj.ToList())
            {
                listEstate.Add(item.Id_estate.ToString());
            }

            List<string> listland = new List<string>();
            foreach (var item in EstateEntities.GetContext().DemandOfLand.ToList())
            {
                listland.Add(item.Id_demandLand.ToString());
            }

            List<string> listflat = new List<string>();
            foreach (var item in EstateEntities.GetContext().DemandOfFlat.ToList())
            {
                listflat.Add(item.Id_demandFlat.ToString());
            }

            List<string> listhouse = new List<string>();
            foreach (var item in EstateEntities.GetContext().DemandOfHouse.ToList())
            {
                listhouse.Add(item.Id_demandHouse.ToString());
            }

            using (EstateEntities context = new EstateEntities())
            {
                IdDem = entity.Id_demand;
                PriceMin.Text = entity.MinPrice.ToString();
                PriceMax.Text = entity.MaxPrice.ToString();
                Client.ItemsSource = listClient;
                Agent1.ItemsSource = listAgent;
                Estate1.ItemsSource = listEstate;
                adress.Text = entity.Adress;
                land.ItemsSource = listland;
                flat.ItemsSource = listflat;
                house.ItemsSource = listhouse;
            }
            Edit.Visibility = Visibility.Visible;
            if (entity == null) return;
            _currentSupp = entity;
            DataContext = _currentSupp;
        }




        private void AddDem(object sender, RoutedEventArgs e)
        {
            using (EstateEntities context = new EstateEntities())
            {
                int integer;

                string selectedLand = (string)land.SelectedItem;
                int? idDemandLand = selectedLand != null ? int.Parse(selectedLand) : (int?)null;

                string selectedFlat = (string)flat.SelectedItem;
                int? idDemandFlat = selectedFlat != null ? int.Parse(selectedFlat) : (int?)null;

                string selectedHouse = (string)house.SelectedItem;
                int? idDemandHouse = selectedHouse != null ? int.Parse(selectedHouse) : (int?)null;


                if (Client.SelectedItem != null && Agent1.SelectedItem != null && Estate1.SelectedItem != null && PriceMin.Text != null && PriceMax != null && adress.Text != null && (house.SelectedItem != null || flat.SelectedItem != null))
                {
                    Demand entity = new Demand
                    {
                        MaxPrice = int.Parse(PriceMax.Text),
                        MinPrice = int.Parse(PriceMin.Text),
                        Id_client = int.Parse((string)Client.SelectedItem),
                        Id_agent = int.Parse((string)Agent1.SelectedItem),
                        Id_type = int.Parse((string)Estate1.SelectedItem),
                        Adress = adress.Text,
                        Id_demand_land = idDemandLand,
                        Id_demand_flat = idDemandFlat,
                        Id_demand_house = idDemandHouse,
                    };
                    context.Demand.Add(entity);
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Поля не заполнены");
                }
                this.Close();
                Supply mainWindow = new Supply();
                mainWindow.Show();
            }
        }
    }
}
