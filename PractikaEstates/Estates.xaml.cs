using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Xml;
using System.Xml.Linq;

namespace PractikaEstates
{
    /// <summary>
    /// Логика взаимодействия для Estates.xaml
    /// </summary>
    public partial class Estates : Window
    {
        public List<EstateObj> estateObjs = new List<EstateObj>();
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
            using (EstateEntities context = new EstateEntities())
            {
                List<string> listNameDist = new List<string>();
                foreach (var item in EstateEntities.GetContext().District.ToList())
                {
                    listNameDist.Add(item.NameDist);
                }
                ComboboxDistrict.ItemsSource = listNameDist;
                List<string> listNameType = new List<string>();
                foreach (var item in EstateEntities.GetContext().TypeOfEstate.ToList())
                {
                    listNameType.Add(item.Name);
                }
                ComboboxType.ItemsSource = listNameType;
                List<string> listNameCity = new List<string>();
                foreach (var item in EstateEntities.GetContext().EstateObj.ToList())
                {
                    listNameCity.Add(item.City);
                }
                ComboboxCity.ItemsSource = listNameCity.Distinct();
                List<string> listNameStreet = new List<string>();
                foreach (var item in EstateEntities.GetContext().EstateObj.ToList())
                {
                    listNameStreet.Add(item.Street);
                }
                ComboboxStreet.ItemsSource = listNameStreet.Distinct();
                List<string> listNameHome = new List<string>();
                foreach (var item in EstateEntities.GetContext().EstateObj.ToList())
                {
                    listNameHome.Add(item.Number_home.ToString());
                }
                ComboboxHome.ItemsSource = listNameHome.Distinct();
                List<string> listNameFlat = new List<string>();
                foreach (var item in EstateEntities.GetContext().EstateObj.ToList())
                {
                    listNameFlat.Add(item.Number_flat.ToString());
                }
                ComboboxFlat.ItemsSource = listNameFlat.Distinct();
            }
        }
        void Filtr()
        {
            var selectedStreet = (string)ComboboxStreet.SelectedItem;
            var selectedCity = (string)ComboboxCity.SelectedItem;
            var selectedhome = (string)ComboboxHome.SelectedItem;
            var selectedflat = (string)ComboboxFlat.SelectedItem;
            var selecteddist=(string)ComboboxDistrict.SelectedItem;
            var selectedtype= (string)ComboboxType.SelectedItem;
            int h=Convert.ToInt32(selectedhome);
            int f=Convert.ToInt32(selectedflat);
            using (EstateEntities context = new EstateEntities())
            {
                var query = context.EstateObj.AsQueryable();

                if (!string.IsNullOrEmpty(selectedStreet))
                {
                    query = query.Where(c => c.Street == selectedStreet);
                    Border border = new Border { BorderBrush = new SolidColorBrush(Color.FromRgb(0, 217, 136)), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(3),Width=double.NaN,MaxWidth=150,Height=40 };
                    Button button = new Button {Background=new ImageBrush(new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\PractikaEstates\\PractikaEstates\\out.png"))),Width=10,Height=10,BorderThickness=new Thickness(0)};
                    
                    TextBlock textBlock = new TextBlock { Text= selectedStreet };
                    StackPanel stackPanel = new StackPanel { Orientation= Orientation.Horizontal };
                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(button);
                    border.Child = stackPanel;
                    tags.Children.Add(border);
                }
                if (!string.IsNullOrEmpty(selectedCity))
                {
                    query = query.Where(c => c.City == selectedCity);
                    Border border = new Border { BorderBrush = new SolidColorBrush(Color.FromRgb(0, 217, 136)), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(3), Width = double.NaN, MaxWidth = 150, Height = 40 };
                    Button button = new Button { Background = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\PractikaEstates\\PractikaEstates\\out.png"))), Width = 10, Height = 10, BorderThickness = new Thickness(0) };
                    TextBlock textBlock = new TextBlock { Text = selectedCity };
                    StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(button);
                    border.Child = stackPanel;
                    tags.Children.Add(border);
                }
                if (!string.IsNullOrEmpty(selectedhome))
                {
                    query = query.Where(c => c.Number_home == h);
                    Border border = new Border { BorderBrush = new SolidColorBrush(Color.FromRgb(0, 217, 136)), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(3), Width = double.NaN, MaxWidth = 150, Height = 40 };
                    Button button = new Button { Background = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\PractikaEstates\\PractikaEstates\\out.png"))), Width = 10, Height = 10, BorderThickness = new Thickness(0) };
                    TextBlock textBlock = new TextBlock { Text = "д."+selectedhome };
                    StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(button);
                    border.Child = stackPanel;
                    tags.Children.Add(border);
                }
                if (!string.IsNullOrEmpty(selectedflat))
                {
                    query = query.Where(c => c.Number_flat == f);
                    Border border = new Border { BorderBrush = new SolidColorBrush(Color.FromRgb(0, 217, 136)), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(3), Width = double.NaN, MaxWidth = 150, Height = 40 };
                    Button button = new Button { Background = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\PractikaEstates\\PractikaEstates\\out.png"))), Width = 10, Height = 10, BorderThickness = new Thickness(0) };
                    TextBlock textBlock = new TextBlock { Text = "кв." + selectedflat };
                    StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(button);
                    border.Child = stackPanel;
                    tags.Children.Add(border);
                }
                if (!string.IsNullOrEmpty(selecteddist))
                {
                    District Distr = (District)context.District.FirstOrDefault(c => c.NameDist == selecteddist);
                    query = query.Where(c => c.id_district == Distr.Id_district);
                    Border border = new Border { BorderBrush = new SolidColorBrush(Color.FromRgb(0, 217, 136)), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(3), Width = double.NaN, MaxWidth = 150, Height = 40 };
                    Button button = new Button { Background = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\PractikaEstates\\PractikaEstates\\out.png"))), Width = 10, Height = 10, BorderThickness = new Thickness(0) };
                    TextBlock textBlock = new TextBlock { Text = Distr.NameDist };
                    StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(button);
                    border.Child = stackPanel;
                    tags.Children.Add(border);
                }
                if (!string.IsNullOrEmpty(selecteddist))
                {
                    TypeOfEstate TypeEs = (TypeOfEstate)context.TypeOfEstate.FirstOrDefault(c => c.Name == selectedtype);
                    query = query.Where(c => c.Id_type == TypeEs.Id_type);
                    Border border = new Border { BorderBrush = new SolidColorBrush(Color.FromRgb(0, 217, 136)), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(3), Width = double.NaN, MaxWidth = 150, Height = 40 };
                    Button button = new Button { Background = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\Пользователь\\Desktop\\OOP\\PractikaEstates\\PractikaEstates\\out.png"))), Width = 10, Height = 10, BorderThickness = new Thickness(0) };
                    TextBlock textBlock = new TextBlock { Text = TypeEs.Name };
                    StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(button);
                    border.Child = stackPanel;
                    tags.Children.Add(border);
                }
                EstateGrid.ItemsSource = query.ToList();

            }
        }
        void clearStack(object sender, RoutedEventArgs e)
        {
            tags.Children.Clear();
            ComboboxFlat.SelectedItem = null;
            ComboboxCity.SelectedItem = null;
            ComboboxDistrict.SelectedItem = null;
            ComboboxHome.SelectedItem = null;
            ComboboxType.SelectedItem = null;
            ComboboxStreet.SelectedItem = null;
            EstateGrid.ItemsSource = EstateEntities.GetContext().EstateObj.ToList();
            //Button button = (Button)sender;

            //Border border = (Border)button.Parent;

            //// Удаляем Border из его родительского элемента
            //border.Parent.Children.Remove(border);
        }
        void SelectCity(object sender, SelectionChangedEventArgs e)
        {
            tags.Children.Clear();
            Filtr();
           
        }
        void SelectStreet(object sender, SelectionChangedEventArgs e)
        {
            tags.Children.Clear();
            Filtr();
        }
        void SelectHome(object sender, SelectionChangedEventArgs e)
        {
            tags.Children.Clear();
            Filtr();
        }
        void SelectFlat(object sender, SelectionChangedEventArgs e)
        {
            tags.Children.Clear();
            Filtr();
        }
        void SelectType(object sender, SelectionChangedEventArgs e)
        {
            tags.Children.Clear();
            Filtr();
        }
        void SelectDist(object sender, SelectionChangedEventArgs e)
        {
            tags.Children.Clear();
            Filtr();
        }
        void DealsClick(object sender, RoutedEventArgs e)
        {
            DealsWindow dealsWindow = new DealsWindow();
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
        void StrEdit(object sender, RoutedEventArgs e)
        {
            // (EstateObj)
            //int itemIndex = TypeGrid.SelectedIndex;
            //var item = (EstateObj)EstateGrid.Items[itemIndex];
            var item = (EstateObj)EstateGrid.SelectedItem; 
            new EditEstate(item).Show();
            Close();

        }
        void AddEs(object sender, RoutedEventArgs e)
        {
            EditEstate es=new EditEstate();
            es.Show();
            this.Close();

        }
        void DelEs(object sender, RoutedEventArgs e)
        {
            var selectedItem = EstateGrid.SelectedItem;
            var estate = (EstateObj)selectedItem;
            using (EstateEntities context = new EstateEntities())
            {

                var entity = context.EstateObj.Find(estate.Id_estate);
                if (entity != null)
                {
                   // var demandForClient = context.Demand.FirstOrDefault(d => d.Id_client == entity.Id_estate);
                    var supplyForClient = context.Supplies.FirstOrDefault(d => d.Id_client == entity.Id_estate);
                    if ( supplyForClient == null)
                    {
                        context.EstateObj.Remove(entity);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить объект, он связан с предложением");
                    }
                }
            }
            Estates mainWindow = new Estates();
            mainWindow.Show();
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
