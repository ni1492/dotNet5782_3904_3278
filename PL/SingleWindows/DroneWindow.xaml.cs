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
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;

namespace PL.SingleWindows
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        #region initialization
        BlApi.IBL bl;
        BackgroundWorker worker;
        PO.DroneSingle DRONE;
        private bool stop() => worker.CancellationPending;
        public DroneWindow(BlApi.IBL bl, PO.DroneSingle drone)//action grid
        {  lock (bl)
            {
                DRONE = drone;
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
                {
                    OPENPARCEL.Visibility = Visibility.Hidden;
                    PARCEL.Text = "no parcel matched";
                }
                else
                {
                    PARCEL.Text = drone.Parcel.PDID.ToString();
                    OPENPARCEL.Visibility = Visibility.Visible;

                }
                InitializeActionsButton(drone);
            }
        }
        public DroneWindow(BlApi.IBL bl)//add grid
        {
        
                this.bl = bl;
                InitializeComponent();
                Actions.Visibility = Visibility.Hidden;
                Add.Visibility = Visibility.Visible;
                OpenParcel.Visibility = Visibility.Hidden;

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
                    action1.Visibility = Visibility.Visible;
                    action1.Content = "Charge";
                    action1.Click += chargeA_Click;

                    action2.Visibility = Visibility.Visible;
                    action2.Content = "Send to delivery";
                    action2.Click += sendToDeliveryA_Click;
                    OpenParcel.Visibility = Visibility.Hidden;

                }
                else if ((DroneStatuses)drone.Status == DroneStatuses.maintenance)
                {
                    action1.Visibility = Visibility.Visible;
                    action1.Content = "Release charge";
                    action1.Click += releaseChargeA_Click;
                    action2.Visibility = Visibility.Hidden;
                    OpenParcel.Visibility = Visibility.Hidden;

                }
                else if (bl.getStatus(drone.Parcel.PDID) == (BO.ParcelStatus)ParcelStatus.PickedUp)
                {
                    action1.Visibility = Visibility.Visible;
                    action2.Visibility = Visibility.Hidden;
                    action1.Content = "Deliver parcel";
                    action1.Click += deliverA_Click;
                    OpenParcel.Visibility = Visibility.Visible;



                }
                else
                {
                    action1.Visibility = Visibility.Visible;
                    action2.Visibility = Visibility.Hidden;
                    action1.Content = "Pick up parcel";
                    action1.Click += pickupA_Click;
                    OpenParcel.Visibility = Visibility.Visible;

                }
            
        }
        #endregion

        #region clicks
        /// <summary>
        ///charging drone
        /// </summary>
        private void chargeA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock(bl)
                {
                    int id;
                    Int32.TryParse(viewID.Text, out id);
                    bl.sendDroneToCharge(id);
                    MessageBox.Show("sent to charge");
                    this.Close();
                    return;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        ///send drone to delivery
        /// </summary>
        private void sendToDeliveryA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock(bl)
                {
                    int id;
                    Int32.TryParse(viewID.Text, out id);
                    bl.matchParcelToDrone(id);
                    MessageBox.Show("drone matched");
                    this.Close();
                    return;
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        ///update delivery time
        /// </summary>
        private void deliverA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock(bl)
                {
                    int id;
                    Int32.TryParse(viewID.Text, out id);
                    bl.deliverParcel(id);
                    MessageBox.Show("delivered");
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        ///update pickup time
        /// </summary>
        private void pickupA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock(bl)
                {
                    int id;
                    Int32.TryParse(viewID.Text, out id);
                    bl.pickupParcel(id);
                    MessageBox.Show("picked up");
                    this.Close();
                    return;
                }
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        ///relese drone
        /// </summary>
        private void releaseChargeA_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                lock(bl)
            {
                int id;
                Int32.TryParse(viewID.Text, out id);
                bl.releaseDroneFromCharge(id);

                MessageBox.Show("released drone");
                this.Close();
                return;
            }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        ///add new drone
        /// </summary>
        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock(bl)
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
            }
               
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }
        /// <summary>
        ///return the id of all the available stations
        /// </summary>
        public IEnumerable<int> stationAvailable(IEnumerable<baseStationForList> displayStationListSlotsAvailable)
        {
            foreach (var item in displayStationListSlotsAvailable)
            {
                yield return item.id;
            }
        }
        /// <summary>
        ///open parcel window
        /// </summary>
        private void OpenParcel_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                int id;
                Int32.TryParse(PARCEL.Text, out id);
                new ParcelWindow(bl, Converter.SingleParcelPO(bl.displayParcel(id))).Show();
            }
        }
        /// <summary>
        ///close window
        /// </summary>
        private void close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        ///update model name
        /// </summary>
        private void updateA_click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock(bl)
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
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region check
        /// <summary>
        ///check if the model is possible
        /// </summary>
        private bool checkModel(string text)
        {
            if ((text != null) && (text != ""))
                return true;
            return false;
        }
        /// <summary>
        ///check if the id is possible
        /// </summary>
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
                lock(bl)
                {
                    if (bl.displayDrones(drone => drone.id == id).FirstOrDefault() != null)
                        return false;
                }
              
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        #endregion

        #region text changed
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
        #endregion

        #region simulation
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(Int32.Parse(viewID.Text));
        }
       
        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
           
                //this.Dispatcher.Invoke(() =>
                //{
                bl.startSimulation((int)e.Argument, () => worker.ReportProgress(0), () => worker.CancellationPending);
                // });
            
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            refresh();
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            worker = null;
            //if the window need to be closed - boolean variable, that is true if the user want to close the window in the middle of auto mode
        }
        #endregion
        private void refresh()
        {
           
            lock (bl)
            {
                drone drone;
                try
                {
                    drone = bl.displayDrone(DRONE.DId);
                }
                catch (Exception)
                {
                    MessageBox.Show("refresh");
                    return;                }
                MODEL.Text = drone.model;
                viewWEIGHT.Text = drone.weight.ToString();
                BATTERY.Text = drone.battery.ToString();
                STATUS.Text = drone.status.ToString();
                LATITUDE.Text = bl.LatitudeToString(drone.currentLocation.Latitude);
                LONGITUDE.Text = bl.LongitudeToString(drone.currentLocation.Longitude);
                if (drone.parcel == null)
                {
                    OPENPARCEL.Visibility = Visibility.Hidden;
                    PARCEL.Text = "no parcel matched";
                }
                else
                {
                    PARCEL.Text = drone.parcel.id.ToString();
                    OPENPARCEL.Visibility = Visibility.Visible;

                }
                InitializeActionsButton(Converter.SingleDronePO(drone));
            }
           
        }
    }
}