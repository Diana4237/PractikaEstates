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
    /// Логика взаимодействия для EditSupply.xaml
    /// </summary>
    public partial class EditSupply : Window
    {
        int IdSup;
        private Supplies _currentSupp = new Supplies();
        public EditSupply()
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
            foreach (var item in EstateEntities.GetContext().EstateObj.ToList())
            {
                listEstate.Add(item.Id_estate.ToString());
            }



            Client1.ItemsSource = listClient;
            Agent1.ItemsSource = listAgent;
            Estate1.ItemsSource = listEstate;


            DataContext = _currentSupp;
        }

        public EditSupply(Supplies entity)
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

            using (EstateEntities context = new EstateEntities())
            {
                IdSup = entity.Id_supply;
                Price1.Text = entity.Price.ToString();

                Client1.ItemsSource = listClient;
                Agent1.ItemsSource = listAgent;
                Estate1.ItemsSource = listEstate;
            }
            Edit.Visibility = Visibility.Visible;
            if (entity == null) return;
            _currentSupp = entity;
            DataContext = _currentSupp;
        }


        private void Back(object sender, RoutedEventArgs e)
        {
           Supply agents = new Supply();
            agents.Show();
            this.Close();
        }
        private void EditSup(object sender, RoutedEventArgs e)
        {
            using (EstateEntities context = new EstateEntities())
            {
                var entity = context.Supplies.Find(IdSup);
                entity.Price = int.Parse(Price1.Text);
                entity.Id_client = (string)Client1.SelectedItem != null ? int.Parse((string)Client1.SelectedItem) : (int?)null;
                entity.Id_agent = (string)Agent1.SelectedItem != null ? int.Parse((string)Agent1.SelectedItem) : (int?)null;
                entity.Id_estate = (string)Estate1.SelectedItem != null ? int.Parse((string)Estate1.SelectedItem) : (int?)null;


                context.SaveChanges();

            }

            EstateEntities.GetContext().SaveChanges();
            new Supply().Show();
            Close();
        }
        private void AddSup(object sender, RoutedEventArgs e)
        {
            using (EstateEntities context = new EstateEntities())
            {
                int integer;
                if (Client1.SelectedItem != null && Agent1.SelectedItem != null && Estate1.SelectedItem != null && Price1.Text != null)
                {
                    Supplies entity = new Supplies
                    {
                        Price = int.Parse(Price1.Text),
                        Id_client = int.Parse((string)Client1.SelectedItem),
                        Id_agent = int.Parse((string)Agent1.SelectedItem),
                        Id_estate = int.Parse((string)Estate1.SelectedItem),
                    };
                    context.Supplies.Add(entity);
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
