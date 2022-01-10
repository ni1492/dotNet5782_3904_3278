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
using PL.SingleWindows;
using System.Media;

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
        IBL bl;

        public object Properties { get; private set; }

        public MainWindow()
        {
            
            bl = App.bl;
            InitializeComponent();
            UserPasswordBorder.Visibility = Visibility.Visible;
            window_User.Visibility = Visibility.Hidden;
            AdminPasswordBorder.Visibility = Visibility.Visible;
            window_Admin.Visibility = Visibility.Hidden;
            showPassAdmin.Visibility = Visibility.Hidden;
            showPassUser.Visibility = Visibility.Hidden;
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {

        }

        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
        }
        private void showDronesButton_click(object sender, RoutedEventArgs e)
        {
            new droneList(bl).Show();
        }

        private void showParcelsButton_click(object sender, RoutedEventArgs e)
        {
            new ParcelList(bl).Show();
        }

        private void showCustomersButton_click(object sender, RoutedEventArgs e)
        {
            new CustomerList(bl).Show();
        }

        private void showStationButton_click(object sender, RoutedEventArgs e)
        {
            new BaseStationList(bl).Show();
        }

        private void signInAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (checkPassword(PassBox_passAdmin.Password, "manager",true))
            {
                AdminPasswordBorder.Visibility = Visibility.Hidden;
                window_Admin.Visibility = Visibility.Visible;
                tryAgain.Visibility = Visibility.Hidden;
                if (!(rememberAdmin.IsChecked.Value))
                {
                    showPassAdmin.Text = "";
                    PassBox_passAdmin.Clear();
                }
            }
            else
                tryAgain.Visibility = Visibility.Visible;
        }
        private bool checkPassword(string pass,string user,bool isManager)
        {
            if (bl.userCorrect(user, pass, isManager))
                return true;
            return false;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkId(ID.Text) && checkName(USER.Text)&& checkPass(PASS.Text))
                {
                    App.bl.AddUser(Int32.Parse(ID.Text), USER.Text, EMAIL.Text, PASS.Text, MANAGER.IsChecked.Value);
                    BO.location l = new BO.location() { Latitude = double.Parse(LATITUDE.Text), Longitude = double.Parse(LONGITUDE.Text) };
                    App.bl.addCustomer(new BO.customer() { id = Int32.Parse(ID.Text), name = USER.Text, phone = PHONE.Text, location = l });
                    MessageBox.Show("signed up");
                    ID.Clear();
                    USER.Clear();
                    EMAIL.Clear();
                    PASS.Clear();
                    PHONE.Clear();
                    LONGITUDE.Clear();
                    LATITUDE.Clear();
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
        private bool checkLocation(string text)
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
        private void LONGITUDEextChanged(object sender, RoutedEventArgs e)
        {
            if (checkLocation(LONGITUDE.Text))
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
        private void LATITUDETextChanged(object sender, RoutedEventArgs e)
        {
            if (checkLocation(LATITUDE.Text))
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
        private void signInUser_Click(object sender, RoutedEventArgs e)
        {
            if (checkPassword(PassBox_user.Password, TextBox_TraineeID.Text,false))
            {
                UserPasswordBorder.Visibility = Visibility.Hidden;
                window_User.Visibility = Visibility.Visible;
                tryAgainUser.Visibility = Visibility.Hidden;
                USERNAME.Content = TextBox_TraineeID.Text;
                List<Parcel> parcels = (from parcel in bl.displayParcelList().Where(p => p.receiver == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
                parcelToCusDataGrid.DataContext = parcels;
                parcels = (from parcel in bl.displayParcelList().Where(p => p.sender == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
                parcelFromCusDataGrid.DataContext = parcels;
                if (!(rememberUser.IsChecked.Value))
                {
                    showPassUser.Text = "";
                    TextBox_TraineeID.Clear();
                    PassBox_user.Clear();
                }
            }
            else
            {
                tryAgainUser.Visibility = Visibility.Visible;
            }

        }

        private void Button_Click_LogOut(object sender, RoutedEventArgs e)
        {
            window_Admin.Visibility = Visibility.Hidden;
            tryAgain.Visibility = Visibility.Hidden;
            AdminPasswordBorder.Visibility = Visibility.Visible;
        }
        private void Button_Click_LogOutUser(object sender, RoutedEventArgs e)
        {
            window_User.Visibility = Visibility.Hidden;
            tryAgain.Visibility = Visibility.Hidden;
            UserPasswordBorder.Visibility = Visibility.Visible;
            USERNAME.Content = "";
            new RateUs();
        }
        private void DataGridCellToCus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.Parcel p = cell.DataContext as PO.Parcel;
            new ParcelWindow(bl, Converter.SingleParcelPO(bl.displayParcel(p.PID))).ShowDialog();
            List<Parcel> parcels = (from parcel in bl.displayParcelList().Where(p => p.receiver == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
            parcelToCusDataGrid.DataContext = parcels;
            parcels = (from parcel in bl.displayParcelList().Where(p => p.sender == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
            parcelFromCusDataGrid.DataContext = parcels;
        }
        private void DataGridCellFromCus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.Parcel p = cell.DataContext as PO.Parcel;
            new ParcelWindow(bl, Converter.SingleParcelPO(bl.displayParcel(p.PID))).ShowDialog();
            List<Parcel> parcels = (from parcel in bl.displayParcelList().Where(p => p.receiver == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
            parcelToCusDataGrid.DataContext = parcels;
            parcels = (from parcel in bl.displayParcelList().Where(p => p.sender == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
            parcelFromCusDataGrid.DataContext = parcels;
        }

        private void newParcel_Click(object sender, RoutedEventArgs e)
        {

            new ParcelWindow(bl, USERNAME.Content.ToString()).ShowDialog();
            List<Parcel> parcels = (from parcel in bl.displayParcelList().Where(p => p.receiver == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
            parcelToCusDataGrid.DataContext = parcels;
            parcels = (from parcel in bl.displayParcelList().Where(p => p.sender == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
            parcelFromCusDataGrid.DataContext = parcels;
        }
        private void refreshUSER_Click(object sender, RoutedEventArgs e)
        {
            List<Parcel> parcels = (from parcel in bl.displayParcelList().Where(p => p.receiver == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
            parcelToCusDataGrid.DataContext = parcels;
            parcels = (from parcel in bl.displayParcelList().Where(p => p.sender == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
            parcelFromCusDataGrid.DataContext = parcels;
        }

        private void OpenDetails_Click(object sender, RoutedEventArgs e)
        {
            bl.displayUser(USERNAME.Content.ToString());
            new UserInfoWindow(bl,Converter.UserPO(bl.displayUser(USERNAME.Content.ToString()))).Show();

        }

        private void showAdmin(object sender, RoutedEventArgs e)
        {
            if(showPassAdmin.Visibility==Visibility.Hidden)
            {
                showPassAdmin.Text = PassBox_passAdmin.Password;
                showPassAdmin.Visibility = Visibility.Visible;
                PassBox_passAdmin.Visibility = Visibility.Hidden;
            }
            else
            {
                showPassAdmin.Visibility = Visibility.Hidden;
                PassBox_passAdmin.Visibility = Visibility.Visible;
            }
        }

        private void showUser(object sender, RoutedEventArgs e)
        {
            if (showPassUser.Visibility == Visibility.Hidden)
            {
                showPassUser.Text = PassBox_user.Password;
                showPassUser.Visibility = Visibility.Visible;
                PassBox_user.Visibility = Visibility.Hidden;
            }
            else
            {
                showPassUser.Visibility = Visibility.Hidden;
                PassBox_user.Visibility = Visibility.Visible;
            }
           
        }
    }
}