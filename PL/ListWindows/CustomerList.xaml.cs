using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BlApi;
using PL.PO;
using PL.SingleWindows;


namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        IBL bl;

        public CustomerList(IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            List<Customer> customers = (from customer in bl.displayCustomerList() select Converter.CustomerPO(customer)).ToList();
            DataContext = customers;

            //droneDataGrid.ItemsSource = bl.displayDroneList();
            //statusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            //weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.Customer c = cell.DataContext as PO.Customer;
            new CustomerWindow(bl, Converter.SingleCustomerPO(bl.displayCustomer(c.CID))).ShowDialog();
        }

        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(bl).ShowDialog();
            //customerDataGrid.ItemsSource = bl.displayCustomerList();
            DataContext = (from customer in bl.displayCustomerList() select Converter.CustomerPO(customer)).ToList();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
