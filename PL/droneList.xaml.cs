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
using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for droneList.xaml
    /// </summary>
    public partial class droneList : Window
    {
         IBL bl;
        public droneList( IBL bl)
        {
            this.bl = bl;
            InitializeComponent();

            droneDataGrid.ItemsSource = bl.displayDroneList();
            statusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void statusSelection(object sender, SelectionChangedEventArgs e )
        {
            if (statusSelector.SelectedItem == null)
                statusSelector.SelectedItem = DroneStatuses.all;
            if (weightSelector.SelectedItem == null)
                weightSelector.SelectedItem = WeightCategories.all;

            else if (((DroneStatuses)weightSelector.SelectedItem == DroneStatuses.all) && ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all))
                droneDataGrid.ItemsSource = bl.displayDroneList();
            else if ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all)
                droneDataGrid.ItemsSource = bl.displayDrones(drone => drone.weight == ( BO.WeightCategories)weightSelector.SelectedItem);
            else if ((DroneStatuses)weightSelector.SelectedItem == DroneStatuses.all)
                droneDataGrid.ItemsSource = bl.displayDrones(drone => drone.status == ( BO.DroneStatuses)statusSelector.SelectedItem);
            else
                droneDataGrid.ItemsSource = bl.displayDrones(drone => (drone.status == ( BO.DroneStatuses)statusSelector.SelectedItem) && (drone.weight == ( BO.WeightCategories)weightSelector.SelectedItem));

        }

        private void weightSelection(object sender, SelectionChangedEventArgs e)
        {
            if (statusSelector.SelectedItem == null)
                statusSelector.SelectedItem = DroneStatuses.all;
            if (weightSelector.SelectedItem == null)
                weightSelector.SelectedItem = WeightCategories.all;
            
            else if (((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all) && ((DroneStatuses)weightSelector.SelectedItem == DroneStatuses.all))
                droneDataGrid.ItemsSource = bl.displayDroneList();
            else if ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all)
                droneDataGrid.ItemsSource = bl.displayDrones(drone => drone.status == ( BO.DroneStatuses)statusSelector.SelectedItem);
            else if ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all)
                droneDataGrid.ItemsSource = bl.displayDrones(drone => drone.weight == ( BO.WeightCategories)weightSelector.SelectedItem);
            else
                droneDataGrid.ItemsSource = bl.displayDrones(drone => (drone.status == ( BO.DroneStatuses)statusSelector.SelectedItem) && (drone.weight == ( BO.WeightCategories)weightSelector.SelectedItem));

        }

        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            new Drone(bl).ShowDialog();
            droneDataGrid.ItemsSource = bl.displayDroneList();
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
        }

        private void openDrone_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = (( BO.droneForList)(sender as ListView).SelectedItem).id;
            new Drone(bl, bl.displayDrone(id)).ShowDialog();
            droneDataGrid.ItemsSource = bl.displayDroneList();
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    public enum WeightCategories { all, light = 1, medium, heavy };//enum of various types of weight: light, medium, heavy
    public enum DroneStatuses { all, available = 1, maintenance, delivery };//enum of various options for drone status: available, maintenance, delivery
    public enum Priorities { regular = 1, quick, urgent };//enum of various options for priority: regular, quick, urgent

}
