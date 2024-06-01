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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PractikaEstates
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int threshold = 3;
        private Client _currentStudent = new Client();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _currentStudent;
            Clients.ItemsSource=EstateEntities.GetContext().Client.ToList();  
            SearchText.TextChanged += TextboxChange;
        }
      
        void TextboxChange(object sender, TextChangedEventArgs e)
        {
            if(SearchText.Text != null && SearchText.Text != "") { 
            string inputName = SearchText.Text;
            List<string> matches = FuzzySearch(inputName);
            List<Client> clients = new List<Client>();
            foreach(string name in matches)
            { 
                using (EstateEntities context = new EstateEntities())
                {
                clients = context.Client.Where(c => c.FirstName == name).ToList();
                }
            }
            Clients.ItemsSource = clients;
            }
            else 
            {
                SearchText.Text = null;
            }
            
        }
        void got(object sender, RoutedEventArgs e)
        {
            if(SearchText.Text != null)
            {
            }
        }
        void lost(object sender, RoutedEventArgs e)
        {
            if (SearchText.Text == null || SearchText.Text =="")
            {
                Clients.ItemsSource = EstateEntities.GetContext().Client.ToList();
            }
        }

        private List<string> FuzzySearch(string inputName)
        {
            List<string> results = new List<string>();
            List<string> FIOClients = new List<string>();
            using (EstateEntities context = new EstateEntities())
            { 
                foreach(Client cl in context.Client)
                {
                    FIOClients.Add(cl.FirstName);
                }
            }

            foreach (string name in FIOClients)
            {
                int distance = LevenshteinDistance(name, inputName);
                if (distance <= threshold)
                {
                    results.Add(name);
                }
            }
            return results;
        }
        private int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }
        void StrEdit(object sender, RoutedEventArgs e)
        {
            //var selectedItem = Clients.SelectedItem;
            //var client = (Client)selectedItem;
            //using (EstateEntities context = new EstateEntities())
            //{
            //    var entity = context.Client.Find(client.Id_client);
            //    EditClient editClient = new EditClient(entity);
            //    editClient.Show();
            //    this.Close();

            //}
            var item = (Client)Clients.SelectedItem; new EditClient(item).Show();
            Close();

        }
        void AddClient(object sender, RoutedEventArgs e)
        {
            EditClient editClient = new EditClient();
            editClient.Show();
        }

        void DelClient(object sender, RoutedEventArgs e)
        {
            var selectedItem = Clients.SelectedItem;
            var client = (Client)selectedItem;
            using (EstateEntities context = new EstateEntities())
            {

                var entity = context.Client.Find(client.Id_client);
                if (entity != null)
                {
                    var demandForClient = context.Demand.FirstOrDefault(d => d.Id_client == entity.Id_client);
                    var supplyForClient = context.Supplies.FirstOrDefault(d => d.Id_client == entity.Id_client);
                    if (demandForClient == null || supplyForClient == null)
                    {
                        context.Client.Remove(entity);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить клиента, он связан с потребностью или предложением");
                    }
                }
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            }

        }
}
