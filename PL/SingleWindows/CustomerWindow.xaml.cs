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

namespace PL.SingleWindows
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BlApi.IBL bl;
        public CustomerWindow(BlApi.IBL bl, BO.customer customer)//action grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Hidden;
            FCus.ItemsSource = customer.fromCus;
            TCus.ItemsSource = customer.toCus;

        }
        public CustomerWindow(BlApi.IBL bl)//add grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Visible;
        }
        private void closeA_click(object sender, RoutedEventArgs e)
        {

        }
        private void updateA_click(object sender, RoutedEventArgs e)
        {

        }
        private void deleteA_click(object sender, RoutedEventArgs e)
        {

        }

        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}