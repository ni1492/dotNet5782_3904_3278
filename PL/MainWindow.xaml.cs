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
using System.Net.Mail;

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
        string codeForResetPass="";
        public object Properties { get; private set; }

        public MainWindow()//constractor
        {
            bl = App.bl;
            InitializeComponent();
            UserPasswordBorder.Visibility = Visibility.Visible;//sign in window- user- to sign in
            window_User.Visibility = Visibility.Hidden;//user window-need to sign in before
            AdminPasswordBorder.Visibility = Visibility.Visible;//sign in window- admin- to sign in
            window_Admin.Visibility = Visibility.Hidden;//admin window-need to sign in before
            showPassAdmin.Visibility = Visibility.Hidden;//show the dots and not the password- admin
            showPassUser.Visibility = Visibility.Hidden;//show the dots and not the password- user
            forgotPassBorder1.Visibility = Visibility.Hidden;//forgot window 1-ask for username and email and send a code
            forgotPassBorder2.Visibility = Visibility.Hidden;//forfot window 2- ask for the code
            resetPassBorder.Visibility = Visibility.Hidden;//forgot window 3- allow to reset the password
            cancel.Visibility = Visibility.Hidden;//allow to go back to the passwordBorder any time in the forget pssword prosses
            App.music.PlayLooping();//activate the backgound music
        }


        #region manager
        #region manager view
        /// <summary>
        /// log out
        /// </summary>
        private void Button_Click_LogOut(object sender, RoutedEventArgs e)
        {
            window_Admin.Visibility = Visibility.Hidden;
            tryAgain.Visibility = Visibility.Hidden;
            AdminPasswordBorder.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// open the drones list window
        /// </summary>
        private void showDronesButton_click(object sender, RoutedEventArgs e)
        {
            lock(bl)
            {
                new droneList(bl).Show();
            }
        }
        /// <summary>
        /// open the parcels list window
        /// </summary>
        private void showParcelsButton_click(object sender, RoutedEventArgs e)
        {
            lock(bl)
            {
                new ParcelList(bl).Show();
            }
        }
        /// <summary>
        /// open the customers list window
        /// </summary>
        private void showCustomersButton_click(object sender, RoutedEventArgs e)
        {
            lock(bl)
            {
                new CustomerList(bl).Show();
            }
        }
        /// <summary>
        /// open the stations list window
        /// </summary>
        private void showStationButton_click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                new BaseStationList(bl).Show();
            }
        }
        #endregion
        #region signing
        /// <summary>
        /// log in with the password
        /// </summary>
        private void signInAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (checkPassword(PassBox_passAdmin.Password, "manager", true))
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
        /// <summary>
        ///show password(the eye) of the admin window
        /// </summary>
        private void showAdmin(object sender, RoutedEventArgs e)
        {
            if (showPassAdmin.Visibility == Visibility.Hidden)
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
        #endregion
        #endregion

        #region checkes
        /// <summary>
        ///check if the password is the right assword
        /// </summary>
        private bool checkPassword(string pass, string user, bool isManager)
        {
            lock (bl)
            {
                if (bl.userCorrect(user, pass, isManager))
                    return true;
                return false;
            }
        }
        /// <summary>
        ///check if the id is possible
        /// </summary>
        private bool checkId(string text)
        {
            try
            {
                lock (bl)
                {
                    if (text == null)
                    return false;
                if (!int.TryParse(text, out int id))
                    return false;
                if (id <= 0 || id < 100000000 || id > 1000000000)
                    return false;
                if (App.bl.displayUsersList().Any(user => user.Id == id))
                    return false;
                    else
                        return true;
                }
            }
            catch (Exception)
            {
                return true;
            }

        }
        /// <summary>
        ///check if the name is possible
        /// </summary>
        private bool checkName(string text)//signing up check
        {
            try
            {
                lock (bl)
                {
                    if (text == null)
                        return false;
                    if (App.bl.displayUser(text) != null)
                        return false;
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }

        }
        /// <summary>
        ///check if the name is exist- for the forgot password option
        /// </summary>
        private bool checkName2(string text)//forgot pass check
        {
            try
            {
                lock (bl)
                {
                    if (text == null)
                        return false;
                    if (App.bl.displayUser(text) == null)
                        return false;
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }

        }
        /// <summary>
        ///check if the password is possible
        /// </summary>
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
        /// <summary>
        ///check if the email is possible
        /// </summary>
        private bool checkEmail(string text)
        {
            try
            {
                lock (bl)
                {
                    if (text.Length == 0)
                        return false;
                    if (App.bl.displayUsersList().Any(user => user.Email.Equals(text)))
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception)
            {
                return false;
            }


        }
        /// <summary>
        ///check if the phone is possible
        /// </summary>
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
        /// <summary>
        ///check if the location is possible
        /// </summary>
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
        /// <summary>
        ///check if the code is the right code
        /// </summary>
        private bool checkCode(string pass)
        {
            return pass.Equals(codeForResetPass);
        }

        #endregion

        #region red boxes
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
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
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
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
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
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
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
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
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
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
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
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
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
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
        #endregion

        #region user
        #region signing
        /// <summary>
        /// log in with username and password
        /// </summary>
        private void signInUser_Click(object sender, RoutedEventArgs e)
        {
            if (checkPassword(PassBox_user.Password, TextBox_TraineeID.Text, false))
            {
                lock (bl)
                {
                    UserPasswordBorder.Visibility = Visibility.Hidden;
                    cancel.Visibility = Visibility.Hidden;
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
            }
            else
            {
                tryAgainUser.Visibility = Visibility.Visible;
            }

        }
        /// <summary>
        ///show password(the eye) of the user window
        /// </summary>
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
        #endregion
        #region actions
        /// <summary>
        /// log out
        /// </summary>
        private void Button_Click_LogOutUser(object sender, RoutedEventArgs e)
        {
            window_User.Visibility = Visibility.Hidden;
            tryAgain.Visibility = Visibility.Hidden;
            UserPasswordBorder.Visibility = Visibility.Visible;
            cancel.Visibility = Visibility.Hidden;
            USERNAME.Content = "";
            new RateUs().ShowDialog();
        }
        /// <summary>
        ///double click for the list of parcels that send to the customer
        /// </summary>
        private void DataGridCellToCus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lock (bl)
            {
                DataGridCell cell = sender as DataGridCell;
                PO.Parcel p = cell.DataContext as PO.Parcel;
                if (p != null)
                    new ParcelWindow(bl, Converter.SingleParcelPO(bl.displayParcel(p.PID))).ShowDialog();
                List<Parcel> parcels = (from parcel in bl.displayParcelList().Where(p => p.receiver == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
                parcelToCusDataGrid.DataContext = parcels;
                parcels = (from parcel in bl.displayParcelList().Where(p => p.sender == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
                parcelFromCusDataGrid.DataContext = parcels;
            }
        }
        /// <summary>
        ///double click for the list of parcels that the customer send
        /// </summary>
        private void DataGridCellFromCus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lock (bl)
            {
                DataGridCell cell = sender as DataGridCell;
                PO.Parcel p = cell.DataContext as PO.Parcel;
                if (p != null)
                    new ParcelWindow(bl, Converter.SingleParcelPO(bl.displayParcel(p.PID))).ShowDialog();
                List<Parcel> parcels = (from parcel in bl.displayParcelList().Where(p => p.receiver == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
                parcelToCusDataGrid.DataContext = parcels;
                parcels = (from parcel in bl.displayParcelList().Where(p => p.sender == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
                parcelFromCusDataGrid.DataContext = parcels;
            }
        }
        /// <summary>
        ///send new parcel from the customer 
        /// </summary>
        private void newParcel_Click(object sender, RoutedEventArgs e)//send new parcel from the customer
        {
            lock (bl)
            {
                new ParcelWindow(bl, USERNAME.Content.ToString()).ShowDialog();
                List<Parcel> parcels = (from parcel in bl.displayParcelList().Where(p => p.receiver == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
                parcelToCusDataGrid.DataContext = parcels;
                parcels = (from parcel in bl.displayParcelList().Where(p => p.sender == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
                parcelFromCusDataGrid.DataContext = parcels;
            }
        }
        /// <summary>
        ///refresh the page-not used yet
        /// </summary>
        private void refreshUSER_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                List<Parcel> parcels = (from parcel in bl.displayParcelList().Where(p => p.receiver == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
                parcelToCusDataGrid.DataContext = parcels;
                parcels = (from parcel in bl.displayParcelList().Where(p => p.sender == USERNAME.Content.ToString()) select Converter.ParcelPO(parcel)).ToList();
                parcelFromCusDataGrid.DataContext = parcels;
            }
        }
        /// <summary>
        ///open a window with the personal details 
        /// </summary>
        private void OpenDetails_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                bl.displayUser(USERNAME.Content.ToString());
                new UserInfoWindow(bl, Converter.UserPO(bl.displayUser(USERNAME.Content.ToString()))).Show();
            }
        }

        #endregion
        #region forgot password
        /// <summary>
        /// if the user forget the password, he need to enter his username and email to 
        /// </summary>
        private void forgotPass_Click(object sender, RoutedEventArgs e)//no1
        {
            try
            {

                UserPasswordBorder.Visibility = Visibility.Hidden;
                cancel.Visibility = Visibility.Visible;
                forgotPassBorder1.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        /// <summary>
        ///click to send an email to the user
        /// </summary>
        private void sendEmail_Click(object sender, RoutedEventArgs e)//no2
        {
            try
            {
                lock (bl)
                {
                    if (checkName2(userName.Text) && userEmail.Text.Equals(bl.displayUser(userName.Text).Email))
                    {
                        Random x = new Random();
                        string code = "";
                        code += (char)x.Next(65, 91);
                        code += (char)x.Next(65, 91);
                        code += (char)x.Next(65, 91);
                        code += (char)x.Next(65, 91);
                        codeForResetPass = code;
                        MailMessage mail = new MailMessage();
                        mail.To.Add(userEmail.Text);
                        mail.From = new MailAddress("DragoDroneDelivery@gmail.com");
                        mail.Subject = "new password";
                        mail.Body = @"<p>Forgot your password?!<br />No problem!<br />Just use the following code to reset your password:</p>
<p><strong>" + code + @"</strong></p>
<p>In the app you'll be able to enter and confirm your new password.</p>";
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Credentials = new System.Net.NetworkCredential("DragoDroneDelivery@gmail.com", "DRAGODRONE");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        forgotPassBorder1.Visibility = Visibility.Hidden;
                        forgotPassBorder2.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("user name or email are not correct");

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        /// <summary>
        ///the user need to enter the code he got to enable the changing password
        /// </summary>
        private void resetPass_Click(object sender, RoutedEventArgs e)//no3
        {
            if (checkCode(codeBox.Text))
            {
                forgotPassBorder1.Visibility = Visibility.Hidden;
                forgotPassBorder2.Visibility = Visibility.Hidden;
                resetPassBorder.Visibility = Visibility.Visible;
                passWarning.Visibility = Visibility.Hidden;

            }
            else
            {
                passWarning.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        ///open the window to change the password if the code was right
        /// </summary>
        private void newPass_Click(object sender, RoutedEventArgs e)//no4
        {
            if (checkPass(newPass.Text))
            {
                lock (bl)
                {
                    forgotPassBorder1.Visibility = Visibility.Hidden;
                    forgotPassBorder2.Visibility = Visibility.Hidden;
                    resetPassBorder.Visibility = Visibility.Hidden;
                    passWarning.Visibility = Visibility.Hidden;

                    bl.changePass(userName.Text, newPass.Text);
                    UserPasswordBorder.Visibility = Visibility.Visible;
                    cancel.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                passWarning.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// at any time while trying to change the password, if the user want to go back without changing the password, he click on this and get back to the ender window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {

            forgotPassBorder1.Visibility = Visibility.Hidden;
            forgotPassBorder2.Visibility = Visibility.Hidden;
            resetPassBorder.Visibility = Visibility.Hidden;
            UserPasswordBorder.Visibility = Visibility.Visible;
            cancel.Visibility = Visibility.Hidden;
        }
        #endregion
        #endregion

        #region sign up
        /// <summary>
        /// after puting all the data, the use click to create an account, and the system send him a welcom email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock (bl)
                {
                    if (checkId(ID.Text) && checkName(USER.Text) && checkPass(PASS.Text))
                    {
                        MailMessage mail = new MailMessage();
                        mail.To.Add(EMAIL.Text);
                        mail.From = new MailAddress("DragoDroneDelivery@gmail.com");
                        mail.Subject = "wlecome to the D.D.D family";
                        mail.Body = @"<p>Dear " + USER.Text + @",</p>
<p>We are very happy to welcome you to our delivery services!</p>
<p>Your account is now set and activated. With your username and password you can easily log in and start sending and receiving parcels.</p>
<p>Have a wonderful day:)</p>
<p>D.D.D DragoDroneDelivery.</p>
<p>&nbsp;</p>";
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Credentials = new System.Net.NetworkCredential("DragoDroneDelivery@gmail.com", "DRAGODRONE");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
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
            }
            catch (Exception ex)
            {
                if (ex.Message == "The specified string is not in the form required for an e-mail address.")
                {

                    EMAIL.BorderBrush = Brushes.DarkRed;
                    EMAIL.Background = Brushes.Red;
                }
                else
                    MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region music
        /// <summary>
        /// stop the music
        /// </summary>
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            Stop.Visibility = Visibility.Hidden;
            Play.Visibility = Visibility.Visible;
            App.music.Stop();
        }
        /// <summary>
        /// restart the music
        /// </summary>
        private void play_ClicK(object sender, RoutedEventArgs e)
        {
            Stop.Visibility = Visibility.Visible;
            Play.Visibility = Visibility.Hidden;
            App.music.PlayLooping();

        }
        #endregion

      
    }
}