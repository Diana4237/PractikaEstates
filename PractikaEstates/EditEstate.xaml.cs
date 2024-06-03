using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
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
using System.Xml.Linq;

namespace PractikaEstates
{
    /// <summary>
    /// Логика взаимодействия для EditEstate.xaml
    /// </summary>
    public partial class EditEstate : Window
    {
        int IdEs;
        private EstateObj _currentClient = new EstateObj();
        public EditEstate()
        {
            InitializeComponent();
            using (EstateEntities context = new EstateEntities())
            {
                List<string> listNameDist = new List<string>();
                foreach (var item in EstateEntities.GetContext().District.ToList())
                {
                    listNameDist.Add(item.NameDist);
                }
                Dist.ItemsSource = listNameDist;
                List<string> listNameType = new List<string>();
                foreach (var item in EstateEntities.GetContext().TypeOfEstate.ToList())
                {
                    listNameType.Add(item.Name);
                }
                type.ItemsSource = listNameType;
            }
                Add.Visibility = Visibility.Visible;
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Estates agents = new Estates();
            agents.Show();
            this.Close();
        }
        public EditEstate(EstateObj entity)
        {
            InitializeComponent();
            using (EstateEntities context = new EstateEntities())
            {
                 city.Text= entity.City;
                street.Text = entity.Street;
                home.Text = entity.Number_home.ToString();
                flat.Text = entity.Number_flat.ToString();
                IdEs = entity.Id_estate;
                List<string> listNameDist = new List<string>();
                foreach (var item in EstateEntities.GetContext().District.ToList())
                {
                    listNameDist.Add(item.NameDist);
                }
                    Dist.ItemsSource = listNameDist;
                List<string> listNameType = new List<string>();
                foreach (var item in EstateEntities.GetContext().TypeOfEstate.ToList())
                {
                    listNameType.Add(item.Name);
                }
                type.ItemsSource = listNameType;
            }
            Edit.Visibility = Visibility.Visible;
            if (entity == null) return;
            _currentClient = entity;
            DataContext = _currentClient;
        }
        private void EditEs(object sender, RoutedEventArgs e)
        {

            using (EstateEntities context = new EstateEntities())
            {
                var entity = context.EstateObj.Find(IdEs);
                District Distr = (District)context.District.FirstOrDefault(c => c.NameDist == Dist.SelectedItem);
                entity.id_district = Distr.Id_district;
                TypeOfEstate TypeEs = (TypeOfEstate)context.TypeOfEstate.FirstOrDefault(c => c.Name == type.SelectedItem);
                entity.Id_type = TypeEs.Id_type;
                if(entity.Id_type != null) 
                { 
                context.SaveChanges();
                }
            }
           
            EstateEntities.GetContext().SaveChanges();
            new Estates().Show();
            Close();
        }
        private void AddEs(object sender, RoutedEventArgs e)
        {
            using (EstateEntities context = new EstateEntities())
            {
                District Distr = (District)context.District.FirstOrDefault(c => c.NameDist == Dist.SelectedItem);
                TypeOfEstate TypeEs = (TypeOfEstate)context.TypeOfEstate.FirstOrDefault(c => c.Name == type.SelectedItem);
                EstateObj entity = new EstateObj
                 {
                        City = city.Text,
                        Street = street.Text,
                        Number_home = Convert.ToInt32(home.Text),
                        Number_flat =Convert.ToInt32(flat.Text),
                        id_district = Distr.Id_district,
                        Id_type= TypeEs.Id_type
                };
                    context.EstateObj.Add(entity);
                    context.SaveChanges();
                
                
                Estates mainWindow = new Estates();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
