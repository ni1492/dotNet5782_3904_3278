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
            DRONES.ItemsSource = station.InCharging;
            NAME.Text = station.BSName.ToString();

        }
        public StationWindow(BlApi.IBL bl)//add grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Visible;
        }
        private void NAMETextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void SLOTSNUMTextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void LATITUDETextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void LONGITUDETextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void IDTextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void closeA_click(object sender, RoutedEventArgs e)
        {

        }
        private void updateA_click(object sender, RoutedEventArgs e)
        {

        }
        private void deleteA_click(object sender, RoutedEventArgs e)
        {

        }
        private void addStation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
