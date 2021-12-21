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

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        IBL bl;
        public CustomerList(IBL bl, ObservableCollection<PO.Customer> customers)
        {
            this.bl = bl;
            InitializeComponent();
            customerDataGrid.DataContext = customers;
            //droneDataGrid.ItemsSource = bl.displayDroneList();
            //statusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            //weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }
    }
}
