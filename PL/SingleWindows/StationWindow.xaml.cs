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
using PL.SingleWindows;
using BO;
using PL.PO;

namespace PL.SingleWindows
{
    /// <summary>
    /// Interaction logic for Station.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        BlApi.IBL bl;

        public StationWindow(BlApi.IBL bl, PO.BaseStationSingle station)//action grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Hidden;
            viewID.Text = station.BaseSId.ToString();
            viewLONG.Text = station.BSLongitude.ToString();
            viewLAT.Text = station.BSLatitude.ToString();
            viewSLOTS.Text = station.ChargingSlots.ToString();
            NAME.Text = station.BSName.ToString();
            droneInChargingDataGrid.ItemsSource = station.InCharging;
        }
        public StationWindow(BlApi.IBL bl)//add grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Visible;
        }
        #region text changed
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
        private void NAMETextChanged(object sender, RoutedEventArgs e)
        {
            if (checkName(NAME.Text))
            {
                NAME.BorderBrush = Brushes.GreenYellow;
                NAME.Background = Brushes.White;
            }
            else
            {
                NAME.BorderBrush = Brushes.DarkRed;
                NAME.Background = Brushes.Red;
            }
        }
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
        private void SLOTSNUMTextChanged(object sender, RoutedEventArgs e)
        {
            if (checkPosNum(SLOTS.Text))
            {
                SLOTS.BorderBrush = Brushes.GreenYellow;
                SLOTS.Background = Brushes.White;
            }
            else
            {
                SLOTS.BorderBrush = Brushes.DarkRed;
                SLOTS.Background = Brushes.Red;
            }
        }
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
        private void LATITUDETextChanged(object sender, RoutedEventArgs e)
        {
            if (checkNUM(LATITUDE.Text))
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
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
        private void LONGITUDETextChanged(object sender, RoutedEventArgs e)
        {
            if (checkNUM(LONGITUDE.Text))
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



        #endregion
        #region clicks
        /// <summary>
        ///open drone window
        /// </summary>
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.DroneInCharging d = cell.DataContext as PO.DroneInCharging;
            if (d != null)
                new DroneWindow(bl, Converter.SingleDronePO(bl.displayDrone(d.DCId))).ShowDialog();
            droneInChargingDataGrid.DataContext = bl.displayStation(Int32.Parse(viewID.Text)).dronesInCharging;

        }
        /// <summary>
        ///close window
        /// </summary>
        private void closeA_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        ///update model name and slots number
        /// </summary>
        private void updateA_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkName(NAME.Text)&& checkPosNum(SLOTS.Text))
                {
                    int id;
                    Int32.TryParse(viewID.Text, out id);
                    int slots;
                    Int32.TryParse(SLOTS.Text, out slots);
                    bl.updateStation(id, NAME.Text, slots);
                    MessageBox.Show("updated");
                    this.Close();
                    return;
                }
                else
                    MessageBox.Show("incorrect input - Base Station update failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        ///add new station
        /// </summary>
        private void addStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkPosNum(ID.Text) && checkName(NAME.Text) && checkNUM(LONGITUDE.Text) && checkNUM(LATITUDE.Text)&&checkPosNum(SLOTS.Text))
                {
                    int n,s;
                    Int32.TryParse(ID.Text, out n);
                    Int32.TryParse(SLOTS.Text, out s);
                    BO.baseStation bs = new BO.baseStation
                    {
                        id = n,
                        name = NAME.Text,
                        chargingSlots=s,
                        location = new()
                        {
                            Longitude=Double.Parse(LONGITUDE.Text),
                            Latitude=Double.Parse(LATITUDE.Text)
                        }
                    };
                    bl.addStation(bs);
                    MessageBox.Show("Added");
                    this.Close();
                    return;
                }
                else
                    MessageBox.Show("incorrect input - add base station failed");
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
        ///check if the slots number is possible
        /// </summary>
        private bool checkPosNum(string text)
        {
            try
            {
                if (text == null)
                    return false;
                if (!int.TryParse(text, out int num))
                    return false;
                if (num <= 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        /// <summary>
        ///check if the name is possible
        /// </summary>
        private bool checkName(string text)
        {
            if ((text != null) && (text != ""))
                return true;
            return false;
        }
        /// <summary>
        ///check if the location is possible
        /// </summary>
        private bool checkNUM(string text)
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
        ///check if the name is possible
        /// </summary>
        private bool checkId(string text)
        {
            int id;
            try
            {
                if (text == null)
                    return false;
                if (!int.TryParse(text, out id))
                    return false;
                if (id <= 0)
                    return false;
                if (bl.displayStation(id).id != 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        #endregion
      
    }
}
