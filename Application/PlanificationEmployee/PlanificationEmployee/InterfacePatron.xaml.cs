﻿using System;
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
    }
}
