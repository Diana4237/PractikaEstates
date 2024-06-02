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
    /// Логика взаимодействия для EditEstate.xaml
    /// </summary>
    public partial class EditEstate : Window
    {
        public EditEstate()
        {
            InitializeComponent();
        }
        public EditEstate(EstateObj estate)
        {
            InitializeComponent();
        }
        private void EditAg(object sender, RoutedEventArgs e)
        {
        }
        private void AddAg(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
