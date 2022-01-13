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
using PL.PO;


namespace PL.SingleWindows
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BlApi.IBL bl;
        public CustomerWindow(BlApi.IBL bl, PO.CustomerSingle customer)//action grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Hidden;
            ToCustomerDataGrid.ItemsSource = customer.ToC;
            FromCustomerDataGrid.DataContext = customer.FromC;

            viewID.Text = customer.CusID.ToString();
            ShowLat.Text = customer.CLatitude.ToString();
            ShowLong.Text = customer.CLongitude.ToString();
            NAME.Text = customer.CusName.ToString();
            PHONE.Text = customer.CPhone.ToString();
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
            this.Close();
        }
        private void updateA_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkName(NAME.Text)&&checkPhone(PHONE.Text))
                {
                    int id;
                    Int32.TryParse(viewID.Text, out id);
                    bl.updateCustomer(id,NAME.Text,PHONE.Text);
                    MessageBox.Show("updated");
                    this.Close();
                    return;
                }
                else
                    MessageBox.Show("incorrect input - customer update failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkId(ID.Text) && checkName(NAME.Text) && checkNUM(LONGITUDE.Text) && checkNUM(LATITUDE.Text) && checkPhone(PHONE.Text))
                {
                    int n;
                    Int32.TryParse(ID.Text, out n);
                    BO.customer c = new BO.customer
                    {
                        id = n,
                        name = NAME.Text,
                        phone=PHONE.Text,
                        fromCus=null,
                        toCus=null,
                        location = new()
                        {
                            Longitude = Double.Parse(LONGITUDE.Text),
                            Latitude = Double.Parse(LATITUDE.Text)
                        }
                        
                    };
                    bl.addCustomer(c);
                    MessageBox.Show("Added");
                    this.Close();
                    return;
                }
                else
                    MessageBox.Show("incorrect input - add customer failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private bool checkName(string text)
        {
            if ((text != null) && (text != ""))
                return true;
            return false;
        }
        private void NAMETextChanged(object sender, RoutedEventArgs e)
        {
            if (checkName(NAME.Text))
            {
                NAME.BorderBrush = Brushes.GreenYellow;
                NAME.Background = Brushes.White;
            }
            else
            {
                NAME.BorderBrush = Brushes.DarkRed;
                NAME.Background = Brushes.Red;
            }
        }
        private bool checkPhone(string text)
        {
            try
            {
                if (text == null)
                    return false;
                if (!int.TryParse(text, out int num))
                    return false;
                if (num >= 100000000)
                    return true;

                return false;
            }
            catch (Exception)
            {
                return true;
            }

        }
        private void PHONETextChanged(object sender, RoutedEventArgs e)
        {
            if (checkPhone(PHONE.Text))
            {
                PHONE.BorderBrush = Brushes.GreenYellow;
                PHONE.Background = Brushes.White;
            }
            else
            {
                PHONE.BorderBrush = Brushes.DarkRed;
                PHONE.Background = Brushes.Red;
            }
        }
        private bool checkId(string text)
        {
            int id;
            try
            {
                if (text == null)
                    return false;
                if (!int.TryParse(text, out id))
                    return false;
                if (id <= 0)
                    return false;
                if (bl.displayCustomer(id).id != 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        private void IDTextChanged(object sender, RoutedEventArgs e)
        {
            if (checkId(ID.Text))
            {
                ID.BorderBrush = Brushes.GreenYellow;
                ID.Background = Brushes.White;
            }
            else
            {
                ID.BorderBrush = Brushes.DarkRed;
                ID.Background = Brushes.Red;
            }
        }
        private bool checkNUM(string text)
        {
            try
            {
                if (text == null)
                    return false;
                if (!Double.TryParse(text, out double num))
                    return false;
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        private void LATITUDETextChanged(object sender, RoutedEventArgs e)
        {
            if (checkNUM(LATITUDE.Text))
            {
                LATITUDE.BorderBrush = Brushes.GreenYellow;
                LATITUDE.Background = Brushes.White;
            }
            else
            {
                LATITUDE.BorderBrush = Brushes.DarkRed;
                LATITUDE.Background = Brushes.Red;
            }
        }
        private void LONGITUDETextChanged(object sender, RoutedEventArgs e)
        {
            if (checkNUM(LONGITUDE.Text))
            {
                LONGITUDE.BorderBrush = Brushes.GreenYellow;
                LONGITUDE.Background = Brushes.White;
            }
            else
            {
                LONGITUDE.BorderBrush = Brushes.DarkRed;
                LONGITUDE.Background = Brushes.Red;
            }
        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.ParcelAtCustomer p = cell.DataContext as PO.ParcelAtCustomer;
            new ParcelWindow(bl, Converter.SingleParcelPO(bl.displayParcel(p.PCID))).ShowDialog();
            ToCustomerDataGrid.ItemsSource = bl.displayCustomer(Int32.Parse(viewID.Text)).toCus;
            FromCustomerDataGrid.DataContext = bl.displayCustomer(Int32.Parse(viewID.Text)).fromCus;
        }
    }
   
}