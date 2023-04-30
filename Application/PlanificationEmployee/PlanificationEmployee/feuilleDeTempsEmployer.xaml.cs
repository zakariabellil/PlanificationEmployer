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
    /// Logique d'interaction pour feuilleDeTempsEmployer.xaml
    /// </summary>
    public partial class feuilleDeTempsEmployer : Window
    {
        List<rangeFeuilleDeTempsUI> rangeFeuilleDeTempsUIS = new List<rangeFeuilleDeTempsUI>();
        public feuilleDeTempsEmployer()
        {
            InitializeComponent();
            rangeFeuilleDeTempsUIS.Add(new rangeFeuilleDeTempsUI { CodeProjet = "", Lundi = "", Mardi = "" });
            dataGrid.ItemsSource = rangeFeuilleDeTempsUIS;
        }



        private void Sauvgarder_Click(object sender, RoutedEventArgs e)
        {
            List<rangeFeuilleDeTempsUI> test = dataGrid.ItemsSource as List<rangeFeuilleDeTempsUI>;
        }
    }
}
