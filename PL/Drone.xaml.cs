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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        IBL.IBL bl;
        public Drone(IBL.IBL bl, IBL.BO.drone drone)//action grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Hidden;
            viewID.Text = drone.id.ToString();
            MODEL.Text = drone.model;
            viewWEIGHT.Text = drone.weight.ToString();
            BATTERY.Text = drone.battery.ToString();
            STATUS.Text = drone.status.ToString();
            LATITUDE.Text = drone.currentLocation.Latitude.ToString();
            LONGITUDE.Text = drone.currentLocation.Longitude.ToString();
            if (drone.parcel == null)
                PARCEL.Text = "0";
            else
                PARCEL.Text = drone.parcel.id.ToString();
            InitializeActionsButton(drone);
        }

        private void InitializeActionsButton(drone drone)
        {

            if (drone == null)
                throw new ArgumentNullException("No drone");

            if ((DroneStatuses)drone.status == DroneStatuses.available)
            {
                time.Visibility = Visibility.Hidden;
                action1.Visibility = Visibility.Visible;
                action1.Content = "Charge";
                action1.Click += chargeA_Click;

                action2.Visibility = Visibility.Visible;
                action2.Content = "Send to delivery";
                action2.Click += sendToDeliveryA_Click;

            }
            else if ((DroneStatuses)drone.status == DroneStatuses.maintenance)
            {
                time.Visibility = Visibility.Visible;
                action1.Visibility = Visibility.Visible;
                action1.Content = "Release charge";
                action1.Click += releaseChargeA_Click;
                action2.Visibility = Visibility.Hidden;

            }
            else if (bl.getStatus(drone.parcel.id) == ParcelStatus.PickedUp)
            {
                time.Visibility = Visibility.Hidden;
                action1.Visibility = Visibility.Visible;
                action2.Visibility = Visibility.Hidden;
                action1.Content = "Deliver parcel";
                action1.Click += deliverA_Click;
            }
            else
            {
                time.Visibility = Visibility.Hidden;
                action1.Visibility = Visibility.Visible;
                action2.Visibility = Visibility.Hidden;
                action1.Content = "Pick up parcel";
                action1.Click += pickupA_Click;
            }
        }
        private void chargeA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id;
                Int32.TryParse(viewID.Text, out id);
                bl.sendDroneToCharge(id);
                MessageBox.Show("sent to charge");
                this.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void sendToDeliveryA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id;
                Int32.TryParse(viewID.Text, out id);
                bl.matchParcelToDrone(id);
                MessageBox.Show("drone matched");
                this.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void deliverA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id;
                Int32.TryParse(viewID.Text, out id);
                bl.deliverParcel(id);
                MessageBox.Show("delivered");
                this.Close();
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void pickupA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id;
                Int32.TryParse(viewID.Text, out id);
                bl.pickupParcel(id);
                MessageBox.Show("picked up");
                this.Close();
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void releaseChargeA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkHour(Hour.Text) && checkMinute(Minute.Text))
                {
                    int h;
                    Int32.TryParse(Hour.Text, out h);
                    int m;
                    Int32.TryParse(Minute.Text, out m);
                    DateTime dateTime = new DateTime(1, 1, 1, h, m, 0);
                    int id;
                    Int32.TryParse(viewID.Text, out id);
                    bl.releaseDroneFromCharge(id, dateTime);
                    time.Visibility = Visibility.Hidden;

                    MessageBox.Show("released drone");
                    this.Close();
                    return;
                }
                else
                    MessageBox.Show("incorrect input - release drone failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        public Drone(IBL.IBL bl)//add grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Visible;
            WEIGHT.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            //"enter: id, model, max weight, station id";
        }

        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkId(ID.Text) && checkModel(MODEL.Text) && checkStationId(STATION.Text) && WEIGHT.SelectedItem != null)
                {
                    int x;
                    Int32.TryParse(ID.Text, out x);
                    IBL.BO.droneForList d = new IBL.BO.droneForList
                    {
                        id = x,
                        model = MODEL.Text,
                        weight = (IBL.BO.WeightCategories)WEIGHT.SelectedItem

                    };
                    Int32.TryParse(STATION.Text.ToString(), out x);
                    bl.addDrone(d, x);
                    MessageBox.Show("Added");
                    this.Close();
                    return;
                }
                else
                    MessageBox.Show("incorrect input - add drone failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closeA_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void updateA_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkModel(MODEL.Text))
                {
                    int id;
                    Int32.TryParse(viewID.Text, out id);
                    bl.updateDrone(id, MODEL.Text);
                    MessageBox.Show("updated");
                    this.Close();
                    return;
                }
                else
                    MessageBox.Show("incorrect input - update drone failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private bool checkModel(string text)
        {
            if ((text != null) && (text != ""))
                return true;
            return false;
        }

        private bool checkId(string text)
        {
            try
            {
                if (text == null)
                    return false;
                if (!int.TryParse(text, out int id))
                    return false;
                if (id <= 0)
                    return false;
                if (bl.displayDrone(id) != null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }

        private bool checkStationId(string text)
        {
            try
            {
                if (text == null)
                    return false;
                if (!int.TryParse(text, out int id))
                    return false;
                if (id <= 0)
                    return false;
                if (bl.displayStation(id) == null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private bool checkHour(string hour)
        {
            if ((hour == null) || (!int.TryParse(hour, out int h)) || ((h > 23) || (h <= 0)))
                return false;
            return true;
        }
        private bool checkMinute(string min)
        {
            if ((min == null) || (!int.TryParse(min, out int m)) || ((m <= 0) || (m >= 60)))
                return false;
            return true;
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

        private void MODELTextChanged(object sender, RoutedEventArgs e)
        {
            if (checkModel(MODEL.Text))
            {
                MODEL.BorderBrush = Brushes.GreenYellow;
                MODEL.Background = Brushes.White;
            }
            else
            {
                MODEL.BorderBrush = Brushes.DarkRed;
                MODEL.Background = Brushes.Red;
            }

        }

        private void STATIONTextChanged(object sender, RoutedEventArgs e)
        {
            if (checkStationId(STATION.Text))
            {
                STATION.BorderBrush = Brushes.GreenYellow;
                STATION.Background = Brushes.White;
            }
            else
            {
                STATION.BorderBrush = Brushes.DarkRed;
                STATION.Background = Brushes.Red;
            }
        }

        private void HOURTextChanged(object sender, RoutedEventArgs e)
        {
            if (checkHour(Hour.Text))
            {
                Hour.BorderBrush = Brushes.GreenYellow;
                Hour.Background = Brushes.White;
            }
            else
            {
                Hour.BorderBrush = Brushes.DarkRed;
                Hour.Background = Brushes.Red;
            }
        }

        private void MINUTETextChanged(object sender, RoutedEventArgs e)
        {
            if (checkMinute(Minute.Text))
            {
                Minute.BorderBrush = Brushes.GreenYellow;
                Minute.Background = Brushes.White;
            }
            else
            {
                Minute.BorderBrush = Brushes.DarkRed;
                Minute.Background = Brushes.Red;
            }
        }


    }
}