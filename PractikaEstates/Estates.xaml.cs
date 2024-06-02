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
            List<int> districts=new List<int>();
            List<string> nameDist = new List<string>();
            District district1;
            List<int> types = new List<int>();
            List<string> nameTypes = new List<string>();
            TypeOfEstate type1;
            //string district2="";
            using (EstateEntities context = new EstateEntities())
            {
                foreach (var estateObj in EstateEntities.GetContext().EstateObj.ToList())
                {
                    if (context.District.FirstOrDefault(d => d.Id_district == estateObj.id_district) != null)
                        districts.Add((int)estateObj.id_district);
                    else districts.Add(0);
                    if (context.TypeOfEstate.FirstOrDefault(d => d.Id_type == estateObj.Id_type) != null)
                        types.Add((int)estateObj.Id_type);
                    else types.Add(0);
                }
                foreach (var district in districts)
                {
                    if (context.District.Where(d => d.Id_district == district) != null)
                    {
                        if (district == 0)
                        {
                            nameDist.Add("Нет");
                        }
                        else
                        {
                            district1 = context.District.Where(c => c.Id_district == district).FirstOrDefault();
                            nameDist.Add(district1.NameDist);
                        }
                    }
                }
                foreach (var type in types)
                {
                    if (context.TypeOfEstate.Where(d => d.Id_type == type) != null)
                    {
                        if (type == 0)
                        {
                            nameTypes.Add("Нет");
                        }
                        else
                        {
                            type1 = context.TypeOfEstate.Where(c => c.Id_type == type).FirstOrDefault();
                            nameTypes.Add(type1.Name);
                        }
                    }
                }
            }
            DistrictGrid.ItemsSource = nameDist;
            TypeGrid.ItemsSource = nameTypes;
        }
        void StrEdit(object sender, RoutedEventArgs e)
        {
            var item = (EstateObj)EstateGrid.SelectedItem; new EditEstate(item).Show();
            Close();

        }
        private void EstateGrid_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
            {
                ScrollViewer scrollViewer = GetScrollViewer(EstateGrid);
                ScrollViewer scrollViewer2 = GetScrollViewer(DistrictGrid);
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
                    scrollViewer2.ScrollToVerticalOffset(e.VerticalOffset);
                }
            }
        }
        private ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer scrollViewer)
            {
                return scrollViewer;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                var result = GetScrollViewer(child);
                if (result != null)
                    return result;
            }
            return null;
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
