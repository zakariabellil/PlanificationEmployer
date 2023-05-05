using Microsoft.Win32;
using Newtonsoft.Json.Linq;
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
    /// Logique d'interaction pour InterfacePatron.xaml
    /// </summary>
    public partial class InterfacePatron : Window
    {
        List<rangeFeuilleDeTempsUI> rangeFeuilleDeTempsUIS = new List<rangeFeuilleDeTempsUI>();
        public InterfacePatron()
            {
            InitializeComponent();
            }

        private void Button_Click(object sender, RoutedEventArgs e)
            {        
            Menu menu = new Menu();
            this.Close();
            menu.Show();          
            }

        private void chargerUneFeuilleDeTemps_Click(object sender, RoutedEventArgs e)
            {
            if (string.IsNullOrEmpty(this.cheminFeuilleDeTempsTxt.Text))
                {
                this.cheminFeuilleDeTempsTxt.Focus();
                MessageBox.Show("Svp ajouter le chemin ");
                }
            else
                {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = this.cheminFeuilleDeTempsTxt.Text;
                openFileDialog.Filter = "Fichiers texte (*.txt)|*.txt|Tous les fichiers (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                bool? result = openFileDialog.ShowDialog();

                if (result == true)
                    {
                    mappeurJson mappeurJson = new mappeurJson();
                    string fileName = openFileDialog.FileName;
                    JObject feuilleDeTempsJObject = mappeurJson.getDataFromJsonFile(fileName);
                    feuilleDeTemps feuilleDeTemps = new feuilleDeTemps(feuilleDeTempsJObject);
                    Dictionary<int, rangeFeuilleDeTempsUI> listeRangeeDictionary = feuilleDeTemps.convertireFeuilleDeTempsEnListeDeRange();
                    rangeFeuilleDeTempsUIS.Clear();
                    foreach (KeyValuePair<int, rangeFeuilleDeTempsUI> kvp in listeRangeeDictionary)                       
                        rangeFeuilleDeTempsUIS.Add(kvp.Value);
                    listView.ItemsSource = rangeFeuilleDeTempsUIS;
                    }
                }
        }
    }
}
