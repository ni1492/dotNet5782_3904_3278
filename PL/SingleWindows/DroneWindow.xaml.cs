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
using BO;
using PL.PO;


namespace PL.SingleWindows
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        BlApi.IBL bl;
        public DroneWindow(BlApi.IBL bl, PO.DroneSingle drone)//action grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Hidden;
            viewID.Text = drone.DId.ToString();
            MODEL.Text = drone.Model;
            viewWEIGHT.Text = drone.Weight.ToString();
            BATTERY.Text = drone.Battery.ToString();
            STATUS.Text = drone.Status.ToString();
            LATITUDE.Text = drone.DLatitude.ToString();
            LONGITUDE.Text = drone.DLongitude.ToString();
            if (drone.Parcel == null)
                PARCEL.Text = "0";
            else
                PARCEL.Text = drone.Parcel.PDID.ToString();
            InitializeActionsButton(drone);
        }
        public DroneWindow(BlApi.IBL bl)//add grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Visible;
            WEIGHT.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            STATION.ItemsSource = stationAvailable(bl.displayStationListSlotsAvailable());

            //"enter: id, model, max weight, station id";
        }

        private void InitializeActionsButton(DroneSingle drone)
        {

            if (drone == null)
                throw new ArgumentNullException("No drone");

            if ((DroneStatuses)drone.Status == DroneStatuses.available)
            {
                //time.Visibility = Visibility.Hidden;
                action1.Visibility = Visibility.Visible;
                action1.Content = "Charge";
                action1.Click += chargeA_Click;

                action2.Visibility = Visibility.Visible;
                action2.Content = "Send to delivery";
                action2.Click += sendToDeliveryA_Click;

            }
            else if ((DroneStatuses)drone.Status == DroneStatuses.maintenance)
            {
                //time.Visibility = Visibility.Visible;
                action1.Visibility = Visibility.Visible;
                action1.Content = "Release charge";
                action1.Click += releaseChargeA_Click;
                action2.Visibility = Visibility.Hidden;

            }
            else if (bl.getStatus(drone.Parcel.PDID) == (BO.ParcelStatus)ParcelStatus.PickedUp)
            {
                // time.Visibility = Visibility.Hidden;
                action1.Visibility = Visibility.Visible;
                action2.Visibility = Visibility.Hidden;
                action1.Content = "Deliver parcel";
                action1.Click += deliverA_Click;
            }
            else
            {
                // time.Visibility = Visibility.Hidden;
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
                int id;
                Int32.TryParse(viewID.Text, out id);
                bl.releaseDroneFromCharge(id);
                // time.Visibility = Visibility.Hidden;

                MessageBox.Show("released drone");
                this.Close();
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkId(ID.Text) && checkModel(MODEL.Text) && STATION.SelectedItem != null && WEIGHT.SelectedItem != null)
                {
                    int x;
                    Int32.TryParse(ID.Text, out x);
                    BO.droneForList d = new BO.droneForList
                    {
                        id = x,
                        model = MODEL.Text,
                        weight = (BO.WeightCategories)WEIGHT.SelectedItem

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
        private void close_click(object sender, RoutedEventArgs e)
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
                if (bl.displayDrones(drone => drone.id == id).FirstOrDefault() != null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        //private bool checkStationId(string text)
        //{
        //    try
        //    {
        //        if (text == null)
        //            return false;
        //        if (!int.TryParse(text, out int id))
        //            return false;
        //        if (id <= 0)
        //            return false;
        //        if (bl.displayStation(id) == null)
        //            return false;
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //}
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
        //private void STATIONTextChanged(object sender, RoutedEventArgs e)
        //{
        //    if (checkStationId(STATION.Text))
        //    {
        //        STATION.BorderBrush = Brushes.GreenYellow;
        //        STATION.Background = Brushes.White;
        //    }
        //    else
        //    {
        //        STATION.BorderBrush = Brushes.DarkRed;
        //        STATION.Background = Brushes.Red;
        //    }
        //}
        public IEnumerable<int> stationAvailable(IEnumerable<baseStationForList> displayStationListSlotsAvailable)
        {
            foreach (var item in displayStationListSlotsAvailable)
            {
                yield return item.id;
            }
        }

        //private void HOURTextChanged(object sender, RoutedEventArgs e)
        //{
        //    if (checkHour(Hour.Text))
        //    {
        //        Hour.BorderBrush = Brushes.GreenYellow;
        //        Hour.Background = Brushes.White;
        //    }
        //    else
        //    {
        //        Hour.BorderBrush = Brushes.DarkRed;
        //        Hour.Background = Brushes.Red;
        //    }
        //}

        //private void MINUTETextChanged(object sender, RoutedEventArgs e)
        //{
        //    if (checkMinute(Minute.Text))
        //    {
        //        Minute.BorderBrush = Brushes.GreenYellow;
        //        Minute.Background = Brushes.White;
        //    }
        //    else
        //    {
        //        Minute.BorderBrush = Brushes.DarkRed;
        //        Minute.Background = Brushes.Red;
        //    }
        //}
        //private bool checkHour(string hour)
        //{
        //    if ((hour == null) || (!int.TryParse(hour, out int h)) || ((h > 23) || (h <= 0)))
        //        return false;
        //    return true;
        //}
        //private bool checkMinute(string min)
        //{
        //    if ((min == null) || (!int.TryParse(min, out int m)) || ((m <= 0) || (m >= 60)))
        //        return false;
        //    return true;
        //}
    }
}