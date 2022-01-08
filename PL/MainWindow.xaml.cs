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
            new droneList(App.bl).Show();
        }

        private void showParcelsButton_click(object sender, RoutedEventArgs e)
        {
            new ParcelList(App.bl).Show();
        }

        private void showCustomersButton_click(object sender, RoutedEventArgs e)
        {
            new CustomerList(App.bl).Show();
        }

        private void showStationButton_click(object sender, RoutedEventArgs e)
        {
            new BaseStationList(App.bl).Show();
        }

        private void signIn_Click(object sender, RoutedEventArgs e)
        {
            if (checkPassword(PassBox_passAdmin.Password))
                AdminPasswordBorder.Visibility = Visibility.Hidden;
        }
        private bool checkPassword(string text)
        {
            if (App.bl.userCorrect("manager", text, true))
                return true;
            return false;
        }

        private void PassBox_passAdmin_KeyDown(object sender, KeyEventArgs e)
        {
            PassBox_passAdmin.Visibility = Visibility.Visible;
        }
    }
}
