using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PractikaEstates
{
    /// <summary>
    /// Логика взаимодействия для EditAgent.xaml
    /// </summary>
    public partial class EditAgent : Window
    {
        int IdCl;
        private Agent _currentClient = new Agent();
        public EditAgent(Agent entity)
        {
            InitializeComponent();
            using (EstateEntities context = new EstateEntities())
            {
                IdCl = entity.Id_agent;
                first.Text = entity.FirstName;
                last.Text = entity.LastName;
                patronym.Text = entity.Patronymic;
                share.Text = Convert.ToString(entity.DealShare);
            }
            Edit.Visibility = Visibility.Visible;
            if (entity == null) return;
            _currentClient = entity;
            DataContext = _currentClient;
        }
        public EditAgent()
        {
            InitializeComponent();
            Add.Visibility = Visibility.Visible;
        }
        private void Back(object sender, RoutedEventArgs e) 
        { 
            Agents agents = new Agents();
            agents.Show();
            this.Close();
        }

        private void EditAg(object sender, RoutedEventArgs e)
        {
            EstateEntities.GetContext().SaveChanges();
            new Agents().Show();
            Close();
        }
        private void AddAg(object sender, RoutedEventArgs e)
        {
            using (EstateEntities context = new EstateEntities())
            {
                List<Agent> ag = EstateEntities.GetContext().Agent.ToList();
                List<int> idAgents= new List<int>();
                foreach (Agent agent in ag)
                {
                    idAgents.Add(agent.Id_agent);
                }
                int randomId;
                do
                {
                    randomId = new Random().Next();
                } while (idAgents.Contains(randomId));
                int integer;
                if (first.Text != null && last.Text != null && patronym.Text!=null && int.TryParse(share.Text,out integer))
                {
                    Agent entity = new Agent
                    {
                        Id_agent=randomId,
                        FirstName = first.Text,
                        LastName = last.Text,
                        Patronymic = patronym.Text,
                        DealShare = Convert.ToInt32(share.Text)
                    };
                    context.Agent.Add(entity);
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("У клиента не указаны контактные данные");
                }
                this.Close();
                Agents mainWindow = new Agents();
                mainWindow.Show();
            }
        }
    }
}
