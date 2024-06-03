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
    /// Логика взаимодействия для Agents.xaml
    /// </summary>
    public partial class Agents : Window
    {
        private int threshold = 3;
        private Agent _currentStudent = new Agent();
        public Agents()
        {
            InitializeComponent();
            DataContext = _currentStudent;
            AgentGrid.ItemsSource = EstateEntities.GetContext().Agent.ToList();
            SearchText.TextChanged += TextboxChange;
        }
        void TextboxChange(object sender, TextChangedEventArgs e)
        {
            if (SearchText.Text != null && SearchText.Text != "")
            {
                string inputName = SearchText.Text;
                List<string> matches = FuzzySearch(inputName);
                List<Agent> clients = new List<Agent>();
                foreach (string name in matches)
                {
                    using (EstateEntities context = new EstateEntities())
                    {
                        clients = context.Agent.Where(c => c.FirstName == name).ToList();
                    }
                }
                AgentGrid.ItemsSource = clients;
            }
            else
            {
                SearchText.Text = null;
            }
        }
        void got(object sender, RoutedEventArgs e)
        {
            if (SearchText.Text != null)
            {
            }
        }
        void lost(object sender, RoutedEventArgs e)
        {
            if (SearchText.Text == null || SearchText.Text == "")
            {
                AgentGrid.ItemsSource = EstateEntities.GetContext().Agent.ToList();
            }
            else
            {
                MessageBox.Show("Очистите поле ввода");
            }
        }
        private List<string> FuzzySearch(string inputName)
        {
            List<string> results = new List<string>();
            List<string> FIOClients = new List<string>();
            using (EstateEntities context = new EstateEntities())
            {
                foreach (Agent cl in context.Agent)
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
            var item = (Agent)AgentGrid.SelectedItem; new EditAgent(item).Show();
            Close();

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
            EditAgent editClient = new EditAgent();
            editClient.Show();
        }
        void DelClient(object sender, RoutedEventArgs e)
        {
            var selectedItem = AgentGrid.SelectedItem;
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

