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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using System.Collections;
using System.Collections.ObjectModel;
using PL.PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<PO.Drone> drones;
        public static ObservableCollection<PO.Parcel> parcels;
        public static ObservableCollection<PO.Customer> customers;
        public static ObservableCollection<PO.BaseStation> stations;

        public MainWindow()
        {
            InitializeComponent();
            InitializeCollections();
            
           
        }

        static public void InitializeCollections()
        {
            drones = new ObservableCollection<PO.Drone>((from bl in App.bl.displayDroneList() select Converter.DronePO(bl)));
            parcels = new ObservableCollection<Parcel>((from bl in App.bl.displayParcelList() select Converter.ParcelPO(App.bl.displayParcel(bl.id))));
            customers = new ObservableCollection<Customer>((from bl in App.bl.displayCustomerList() select Converter.CustomerPO(App.bl.displayCustomer(bl.id))));
            stations = new ObservableCollection<BaseStation>((from bl in App.bl.displayStationList() select Converter.StationPO(App.bl.displayStation(bl.id))));
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
        private void showDronesButton_click(object sender, RoutedEventArgs e)
        {
            new droneList(App.bl,drones).Show();
        }

        private void showParcelsButton_click(object sender, RoutedEventArgs e)
        {
            new ParcelList(App.bl,parcels).Show();
        }

        private void showCustomersButton_click(object sender, RoutedEventArgs e)
        {
            new CustomerList(App.bl,customers).Show();
        }

        private void showStationButton_click(object sender, RoutedEventArgs e)
        {
            new BaseStationList(App.bl,stations).Show();
        }
    }
}
