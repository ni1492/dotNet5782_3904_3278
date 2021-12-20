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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBL bl;

        public MainWindow()
        {
            bl =BlFactory.GetBl();
            InitializeComponent();
        }

        private void showDronesButton_click(object sender, RoutedEventArgs e)
        {
            new droneList(bl).Show();
        }

        private void AdminSignIn(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserSignIn(object sender, RoutedEventArgs e)
        {

        }

        private void SignUp(object sender, RoutedEventArgs e)
        {

        }
    }
}
