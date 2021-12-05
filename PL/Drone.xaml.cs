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
        public Drone(IBL.IBL bl,IBL.BO.drone drone)//action grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Hidden;
            ID.Text = drone.id.ToString();
            MODEL.Text = drone.model;
            WEIGHT.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            WEIGHT.SelectedItem = drone.weight;
            BATTERY.Text = drone.battery.ToString();
            STATUS.Text = drone.status.ToString();
            LATITUDE.Text = drone.currentLocation.Latitude.ToString();
            LONGITUDE.Text = drone.currentLocation.Longitude.ToString();
            PARCEL.Text = drone.parcel.id.ToString();
            InitializeActionsButton(drone);
        }

        private void InitializeActionsButton(drone drone)
        {
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
