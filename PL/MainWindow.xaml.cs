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
            logOut.Visibility = Visibility.Visible;
        }
        private bool checkPassword(string text)
        {
            if (App.bl.userCorrect("manager", text, true))
                return true;
            return false;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkId(ID.Text) && checkName(USER.Text)&& checkPass(PASS.Text))
                {
                    App.bl.AddUser(Int32.Parse(ID.Text), USER.Text, EMAIL.Text, PASS.Text, MANAGER.IsEnabled);
                    MessageBox.Show("signed up");
                    ID.Clear();
                    USER.Clear();
                    EMAIL.Clear();
                    PASS.Clear();
                    MANAGER.ClearValue(IconProperty);
                }
                else
                    MessageBox.Show("incorrect input - signing up failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private bool checkId(string text)
        {
            try
            {
                if (text == null)
                    return false;
                if (!int.TryParse(text, out int id))
                    return false;
                if (id <= 0 || id < 100000000  || id > 1000000000)
                    return false;
                if (App.bl.displayUsersList().Any(user => user.Id == id))
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        private bool checkName(string text)
        {
            try
            {
                if (text == null)
                    return false;
                if (App.bl.displayUser(text) != null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        private bool checkPass(string text)
        {
            try
            {
                if (text == null)
                    return false;
                if (text.Length < 8)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        private bool checkEmail(string text)
        {
            try
            {
                if (text.Length==0)
                    return false;
                if (App.bl.displayUsersList().Any(user => user.Email.Equals(text)))
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
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
        private void NameTextChanged(object sender, RoutedEventArgs e)
        {
            if (checkName(USER.Text))
            {
                USER.BorderBrush = Brushes.GreenYellow;
                USER.Background = Brushes.White;
            }
            else
            {
                USER.BorderBrush = Brushes.DarkRed;
                USER.Background = Brushes.Red;
            }
        }
        private void PassTextChanged(object sender, RoutedEventArgs e)
        {
            if (checkPass(PASS.Text))
            {
                PASS.BorderBrush = Brushes.GreenYellow;
                PASS.Background = Brushes.White;
            }
            else
            {
                PASS.BorderBrush = Brushes.DarkRed;
                PASS.Background = Brushes.Red;
            }
        }
        private void EmailTextChanged(object sender, RoutedEventArgs e)
        {
            if (checkEmail(EMAIL.Text))
            {
                EMAIL.BorderBrush = Brushes.GreenYellow;
                EMAIL.Background = Brushes.White;
            }
            else
            {
                EMAIL.BorderBrush = Brushes.DarkRed;
                EMAIL.Background = Brushes.Red;
            }
        }
    
    }
}