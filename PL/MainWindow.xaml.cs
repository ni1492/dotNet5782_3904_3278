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
                        App.bl.AddUser(Int32.Parse(ID.Text), USER.Text, EMAIL.Text, PASS.Text, false);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"כללי
תקנון זה מהווה הסכם משפטי מחייב. הוראות תקנון זה יחולו על כל שימוש שייעשה על ידי המשתמש באתר של חברת הרחפנים DRAGODRONE ועל כל רכישה שתתבצע על ידך באמצעות האתר ומהווים הסכם מחייב בינך לבין מפעילת האתר,
קרא תקנון זה במלואו ובעיון. כותרות התקנון הסעיפים הנן לצרכי נוחות בלבד. למען הסר ספק, לא ייראה כהסכם לטובת צד שלישי כלשהו.
גלישה באתר ו/או רכישת שירותים המוצעים למכירה בו, מהווה את הסכמתך לקבל ולנהוג לפי התקנון. לכן אם אינך מסכים לתנאי מתנאיו הנך מתבקש לא לעשות כל שימוש באתר.
השימוש באתר כפוף להקפדה על כללי התקנון. אחריות מפעילת האתר מוגבלת כמפורט בהסכם זה. מפעילת האתר לא תהיה אחראית לפעולות ו/או בין צדדים שלישיים. אין להפר את זכויות הקניין הרוחני של מפעילת האתר. הוראות חשובות אלה חלות, ללא הגבלה, על כל המשתמשים באתר.
באתר רשאי להשתמש כל אדם המחזיק ברשותו ועל שמו כרטיס אשראי תקף. לא תינתן האפשרות לביצוע הזמנות ללא מסירת פרטי כרטיס אשראי או כל אמצעי תשלום אחר שיאושר על ידי מפעילת האתר.
תנאי השימוש מנוסחים בלשון זכר לצורכי נוחות בלבד, אולם הם מיועדים לגברים ונשים כאחד.
השימוש באתר
הנך רשאי להשתמש בתכנים באתר בהתאם לתנאים המפורטים בתקנון זה בלבד. אין להשתמש בתכנים באתר באופן אחר מהקבוע בתנאי השימוש של האתר, אלא אם קיבלת את הסכמתה המפורשת, מראש ובכתב של מפעילת האתר ובכפוף לתנאי הסכמתה (אם תינתן).
הנך רשאי להשתמש באתר למטרות פרטיות ואישיות בלבד. אין להעתיק ו/או להשתמש, או לאפשר לאחרים להשתמש, בכל דרך אחרת בתכנים שבאתר. אין לעשות באתר כל שימוש מסחרי אם בדרך של משלוח פרסומות ו/או בכל דרך אחרת ללא קבלת הסכמת מפעילת האתר בכתב ומראש ובהתאם לתנאיה ולשיקול דעתה הבלעדי של מפעילת האתר. היה ונעשה פרסום באתר, מפעילת האתר לא תהיה אחראית לתוכן הפרסומים ו/או לנזקים (בין ישירים ובין אם עקיפים) העלולים להיגרם בשל כך.
אין להפעיל או לאפשר להפעיל כל יישום מחשב או כל אמצעי אחר, לרבות תוכנות מכל סוג שהוא, לשם חיפוש, סריקה, העתקה או אחזור אוטומטי של תכנים מתוך האתר. בכלל זה אין ליצור ואין להשתמש באמצעים כאמור לשם יצירת לקט, אוסף או מאגר שיכילו תכנים ממפעילת האתר.
שינויים בתקנון
תקנון זה ניתן לשינוי בכל עת ע”י מפעילת האתר על פי שיקול דעתה הבלעדי.
פרטיות
מפעילת האתר תעשה כמיטב יכולתה, באמצעים העומדים לרשותה לשמור על סודיות פרטי המשתמשים באתר ומניעת הגעתם לידי גורמים זרים. מפעילת האתר מפעילה מערכת אבטחה מהמתקדמות ביותר הקיימות כיום בשוק המתאימות לאתרי אינטרנט. אך על אף זאת אין ביכולתה לחסום ו/או למנוע בצורה ודאית ומוחלטת חדירה לא מורשית למאגרי המידע והמשתמש מוותר בזאת על כל תביעה ו/או טענה כנגד מפעילת האתר בעניין זה וזאת בתנאי שמפעילת האתר נקטה באמצעים סבירים למנוע חדירה לא מורשית כאמור.
בעת השימוש באתר ייאסף מידע אודות הדפים והמודעות שהוצגו בפני המשתמש, התכנים שעניינו אותו, זמן שהייתו באתר ואילו פעילויות ביצע בו. המידע אינו מזהה את המשתמש באופן אישי לכן המידע שנאסף הינו בעל אופי סטטיסטי כללי בלבד. מפעילת האתר רשאית לשמור את המידע ולהשתמש בו לצרכיה על פי מגבלות תנאי שימוש אלה והוראות כל דין. מפעילת האתר רשאית להשתמש במידע שייאגר אודות השימוש שיעשה המשתמש באתר לצורך הפקת וניתוח מידע סטטיסטי. מפעילת האתר רשאית למסור נתונים סטטיסטיים כאלה לצדדים שלישיים, כל עוד אין הנתונים מתייחסים למשתמש באופן אישי או מזהים אותו באופן אישי.
מפעילת האתר תהיה רשאית להעביר את פרטיך לצד שלישי בין היתר, במקרה שבו הפרת תנאי הסכם זה או כל הסכם נוסף עם מפעילת האתר או מי מטעמה וכן במקרה שבו נתקבל בידי מפעילת האתר או בידי צד שלישי צו מרשות מוסמכת המורה לה למסור את פרטיך לצד שלישי.
הפרות וסעדים
אין להעתיק את תכני האתר. בין היתר, ומבלי לגרוע מהוראות כל דין, אין להעתיק לשכפל, להפיץ, לפרסם או להשתמש בכל דרך אחרת בתכנים המופיעים באתר אלא אם כן מפעילת האתר נתנה את הסכמתה לכך, בכתב ומראש.
חל איסור מוחלט להעתיק ו/או לפרסם תמונות ו/או סימנים מסחריים ו/או מפרטים ו/או סרטונים מהאתר ו/או באתר ללא אישור מראש ובכתב ממפעילת האתר או בעל הזכויות המתאים. מפעילת האתר רשאית להפעיל נוהל הודעה והסרה בכל מקרה של חשד להפרת זכויות של צדדים שלישיים, לרבות זכויות קניין רוחני, סימני מסחר וכל זכות אחרת, על פי שיקול דעתה הבלעדי. כל צד יהיה אחראי למעשיו ומחדליו לרבות בקשר לתכנים המפורסמים על ידו. מפעילת האתר לא תישא באחריות להפרות של צדדים שלישיים.
הוראות התנהגות באתר
מבלי לגרוע מכל הוראה אחרת, על כל משתמש יחולו ההוראות הבאות.
המשתמש מחויב למילוי הוגן של פרטים מדויקים ונכונים. מבלי לגרוע מכלליות האמור, מובהר כי מסירת פרטים כוזבים הנה עבירה פלילית.
חל איסור מוחלט על משתמשים לעשות שימוש בשפה ו/או בתוכן פוגעניים, בוטים, מגונים ו/או שיש בהם כדי להעליב ו/או להשמיץ ו/או שעלולים להוות באופן כלשהו לשון הרע ו/או הפרה של הוראת חוק כלשהו. משתמש אשר שהפר סעיף זה, ישפה את מפעילת האתר, מייד עם דרישתה הראשונה, לרבות בגין כל דרישה, טענה או פנייה אשר תופנה אליה מצד שלישי כלשהו.
מבלי לגרוע מהאמור לעיל, מפעילת האתר רשאית, אך לא חייבת, לנקוט בכל האמצעים החוקיים העומדים לרשותה, כנגד משתמשים אשר יפרו הוראות כל דין ו/או הוראות תקנון זה. במקרים של הפרה כאמור מפעילת האתר רשאית להסיר לאלתר כל משתמש ו/או תוכן ו/או מוצר מהאתר ו/או להעביר את פרטי המשתמש לכל רשות סטטוטורית ו/או לכל גורם אחר, ו/או לנקוט בכל צעד אחר הנדרש בנסיבות העניין לפי שיקול דעתה המוחלט של מפעילת האתר.
מובהר בזאת, כי מפעילת האתר רשאית לערוך ו/או לתקן ו/או להוסיף ו/או למחוק ו/או לא לשקלל ו/או לא להציג כלל כל פריט תוכן או כל חלק ממנו, והכל מבלי שתהא מחויבת להודיע על כך מראש או בדיעבד.
מפעילת האתר רשאית לפרסם באתר, או בכל אמצעי או מדיה אחרים, את התכנים ו/או חלק מהם, לרבות נתוני משובי משתמשים או כל חלק מהם ו/או לעשות בהם שימוש לכל מטרה אחרת, לרבות פרסום האתר, ולמשתמשים לא תהא כל טענה כנגד פרסום כאמור.
הגבלת אחריות
מפעילת האתר לא תהיה אחראית לכל נזק שנגרם בקשר או בעקבות עם השימוש באתר, לרבות בגין כל מעשה ו/או מחדל של יצרן המוצר או השירות הרלוונטיים. מפעילת האתר לא תישא בכל אחריות או חבות למעט כאמור מפורשות בהסכם זה ובהתאם להוראות הדין שאין להתנות עליהם.
הסעד הבלעדי והממצה שיהיה למשתמש במקרה של הפרה יסודית של ביצוע הזמנת שירותים כלשהם באמצעות האתר יהיה ביטולם, ולא תהיינה למשתמש טענות נוספות כנגד הספק או מפעילת האתר.
נפלה טעות קולמוס חריגה וברורה על פניה בתיאור שירות כלשהו, כגון מחיר הנקוב באגורות במקום בשקלים, לא יחייב הדבר את מפעילת האתר.
מבלי לגרוע מכלליות האמור לעיל, השירות באתר ניתן ללא כל מצג או התחייבות משתמעת, כמו שהוא (As Is) לא תהיה למשתמש או למבצע הפעולה כל טענה (מכל מין ו/או סוג), תביעה (מכל מין ו/או סוג) או דרישה (מכל מין ו/או סוג) כלפי מפעילת האתר בגין תכונות השירות, מאפייניו, מגבלותיו או התאמתו לצרכיו ולדרישותיו.
קישורים
ככל שיופיעו באתר קישורים לאתרי אינטרנט שאינם של מפעילת האתר, מפעילת האתר אינה אחראית לתקינותם / אמינותם / חוקיותם / ביצועיהם של אותם אתרים ו/או לתוכן הנמצא בהם ו/או לאיכות וטיב השירותים או המוצרים המסופקים בהם ו/או באמצעותם. המעבר לאתרים אלו באמצעות הקישורים מהאתר נעשית על אחריות המשתמש בלבד, ובהתאם הוא לא יוכלל בוא למפעילת האתר בטענה כלשהי בקשר לנזקים שנגרמו לו על ידי ו/או באותם אתרים ו/או בשל הגלישה לאותם אתרים ו/או בכל הקשור לנגישות אליהם.
כמו כן, מפעילת האתר אינה מתחייבת כי כל הקישורים שימצאו באתר יהיו תקינים או עדכניים ויובילו אותך לאתר אינטרנט פעיל.
מפעילת האתר רשאית לסלק מהאתר קישורים שנכללו בו בעבר או להימנע מהוספת קישורים חדשים, הכול על פי שיקול דעתה הבלעדי.
שימוש בעוגיות (Cookies)
מפעילת האתר תהא רשאית לעשות שימוש ב”עוגיות” קבצי טקסט קטנים אשר מאוחסנים על הכונן הקשיח של מחשב המשתמש, לרבות על מנת לספק לך שירות מהיר ויעיל ולחסוך ממך את הצורך להזין את פרטיך האישיים בכל כניסה לאתר. מובהר כי קבצים אלה לא יכילו כל מידע מזהה באופן אישי ואתה יכול לכוון את תוכנת הדפדפן שלך כך שלא ייקלטו במחשב “עוגיות” או שיימחקו.
שינויים באתר, הפסקת השירות ושירותי תמיכה טכנית
מפעילת האתר שומרת לעצמה את הזכות, לפי שיקול דעתה הבלעדי ומבלי כל צורך להודיע על כך מראש, להפסיק את זמינות השימוש באתר מעת לעת לצורכי תחזוקתו ו/או ארגונו ו/או מהותו ו/או לכל צורך אחר. שינויים אילו יכולים להיות בדרך של שינוי מבנה האתר, מראהו, היקפם וזמינותם של השירותים המוצעים בו וכן שינוי בכל היבט אחר הכרוך באתר ובתפעולו. שינויים מעין אילו, עלולים ליצור בתחילה חוסר נוחות ולהיות מאופיינים בתקלות, הפסקת תקשורת וכיו”ב. לא תהיה לך כל טענה ו/או דרישה ו/או תביעה כנגד מפעילת האתר בגין ביצוע שינויים ו/או תקלות אגב ביצוען ו/או הפסקת זמינות האתר כאמור.
מפעילת האתר שומרת לעצמה את הזכות להחליט בכל עת, ולפי שיקול דעתה הבלעדי על סגירת האתר. מפעילת האתר תפרסם באתר על כוונתה לעשות כן לפחות 7 ימים מראש. מובהר בזאת כי היה ורשות מסוימת תחליט על סגירת האתר, מכל סיבה שהיא, בין אם באופן זמני ובין אם לצמיתות לא תחול כל חובה על מפעילת האתר להתריע על כך מראש ולא תהיה לך כל טענה ו/או דרישה ו/או תביעה כנגד מפעילת האתר בקשר לכך.
מפעילת האתר שומרת לעצמה את הזכות, לפי שיקול דעתה הבלעדי ובכל עת, לערוך שינויים בתנאי השימוש באתר וזאת ללא כל מתן הודעה מוקדמת על כך באתר. שינוי בתנאי השימוש יכנס לתוקפו תוך 7 ימים מיום פרסומו הראשון. לפיכך אנו ממליצים כי תעיין בהוראות תנאי השימוש בכל כניסה מחודשת לאתר על מנת לעמוד על השינויים שחלו בתנאי השימוש בו, אם חלו.
מפעילת האתר אינה מתחייבת לספק שירותי תמיכה טכנית לרבות, אך לא רק, שירות בדבר תפעול האתר ו/או השירותים הניתנים באמצעותו. לא תהיה לך כל טענה ו/או דרישה ו/או תביעה כנגד מפעילת האתר בגין היעדר שירותי תמיכה טכנית כאמור.
");
        }
    }
}