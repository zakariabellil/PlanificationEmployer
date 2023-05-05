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

namespace PlanificationEmployee
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnEmploye_Click(object sender, RoutedEventArgs e)
        {
            InterfaceEmployes interfaceEmployes = new InterfaceEmployes();
            interfaceEmployes.Show();
            this.Close();
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            if (txtPasword.Text == "admin")
            {
                InterfacePatron interfacePatron = new InterfacePatron();
                interfacePatron.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("mot de passe incorecte");
            }
        }
    }
}
