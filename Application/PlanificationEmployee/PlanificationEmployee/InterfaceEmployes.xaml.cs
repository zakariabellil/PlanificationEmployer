using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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

    {
        List<rangeFeuilleDeTempsUI> rangeFeuilleDeTempsUIS = new List<rangeFeuilleDeTempsUI>();
        public InterfaceEmployes()
        {
            InitializeComponent();
            rangeFeuilleDeTempsUIS.Add(new rangeFeuilleDeTempsUI { CodeProjet = "", Lundi = "", Mardi = "" });
            dataGrid.ItemsSource = rangeFeuilleDeTempsUIS;
        }



        private void Sauvgarder_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrEmpty(this.DateTxt.Text))
            {
                this.DateTxt.Focus();
                MessageBox.Show("Svp ajouter la date");

            }
            else if (string.IsNullOrEmpty(this.IDEmpTxt.Text))
            {
                this.DateTxt.Focus();
                MessageBox.Show("Svp ajouter le IDEmp");
            }
            else if (string.IsNullOrEmpty(this.PathTxt.Text))
            {
                this.PathTxt.Focus();
                MessageBox.Show("Svp ajouter le chemin du dossier ou vous voulez sauvgarder");
            }
            else if (string.IsNullOrEmpty(this.NomFichierTxt.Text))
            {
                this.NomFichierTxt.Focus();
                MessageBox.Show("Svp ajouter le nom du ficher a sauvgarder");
            }
            else
            {
                bool isErreursFeuilleDeTemps = false;
                System.Globalization.Calendar cal = CultureInfo.CurrentCulture.Calendar;
                int NumSemainFeuilleDeTemps = cal.GetWeekOfYear(DateTime.Parse(this.DateTxt.Text), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                int IDEmp = int.Parse(this.IDEmpTxt.Text);
                List<rangeFeuilleDeTempsUI> rangeFeuille = dataGrid.ItemsSource as List<rangeFeuilleDeTempsUI>;
                feuilleDeTemps feuilleDeTemps = new feuilleDeTemps(rangeFeuille, IDEmp, NumSemainFeuilleDeTemps);
                validateurFeuilleDeTemps validateurFeuilleDeTemps = new validateurFeuilleDeTemps();
                Dictionary<string, string> erreursFeuilleDeTemps = validateurFeuilleDeTemps.validerFeuilleDeTemps(feuilleDeTemps);
                foreach (KeyValuePair<string, string> kvp in erreursFeuilleDeTemps)
                {
                    if (kvp.Value != "Aucun probleme")
                    {
                        MessageBox.Show(kvp.Key + " " + " " + kvp.Value);
                        isErreursFeuilleDeTemps = true;
                        break;
                    }
                }
                if (!isErreursFeuilleDeTemps)
                {
                    feuilleDeTemps.enregistrerMaFeuilleDeTempeEnFormatJson(this.PathTxt.Text,
                                                                           this.NomFichierTxt.Text);
                    MessageBox.Show("Feuille de temps sauvgardée!");
                }
            }

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
