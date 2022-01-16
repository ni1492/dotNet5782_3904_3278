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
        #region window initialization 
        IBL bl;

        public CustomerList(IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            List<Customer> customers = (from customer in bl.displayCustomerList() select Converter.CustomerPO(customer)).ToList();
            DataContext = customers;

        }
        #endregion

        #region clicks
        /// <summary>
        ///open single customer window 
        /// </summary>
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.Customer c = cell.DataContext as PO.Customer;
            new CustomerWindow(bl, Converter.SingleCustomerPO(bl.displayCustomer(c.CID))).ShowDialog();
            DataContext = (from customer in bl.displayCustomerList() select Converter.CustomerPO(customer)).ToList();

        }
        /// <summary>
        ///open adding customer window
        /// </summary>
        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(bl).ShowDialog();
            DataContext = (from customer in bl.displayCustomerList() select Converter.CustomerPO(customer)).ToList();
        }
        /// <summary>
        ///close the window
        /// </summary>
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        ///refreshing the page
        /// </summary>
        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            DataContext = (from customer in bl.displayCustomerList() select Converter.CustomerPO(customer)).ToList();
        }
        #endregion
    }
}
