using Microsoft.Win32;
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
    /// Logique d'interaction pour InterfaceEmployes.xaml
    /// </summary>
    public partial class InterfaceEmployes : Window
<<<<<<< HEAD
        {
=======
    {
        List<rangeFeuilleDeTempsUI> rangeFeuilleDeTempsUIS = new List<rangeFeuilleDeTempsUI>();
>>>>>>> 3cb56cecd29a00c5c0a5c77b80e2d1575db5e001
        public InterfaceEmployes()
        {
            InitializeComponent();
            rangeFeuilleDeTempsUIS.Add(new rangeFeuilleDeTempsUI { CodeProjet = "", Lundi = "", Mardi = "" });
            dataGrid.ItemsSource = rangeFeuilleDeTempsUIS;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            this.Close();
            menu.Show();
        }

<<<<<<< HEAD
=======
        private void Sauvgarder_Click(object sender, RoutedEventArgs e)
        {
            List<rangeFeuilleDeTempsUI> test = dataGrid.ItemsSource as List<rangeFeuilleDeTempsUI>;
>>>>>>> 3cb56cecd29a00c5c0a5c77b80e2d1575db5e001
        }
    }
