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
using IBL;
namespace PL
{
    /// <summary>
    /// Interaction logic for droneList.xaml
    /// </summary>
    public partial class droneList : Window
    {
        IBL.IBL bl;
        public droneList(IBL.IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            
            DroneListView.ItemsSource = bl.displayDroneList();
            statusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void statusSelection(object sender, SelectionChangedEventArgs e )
        {
            if (((weightSelector.SelectedItem == null) && (statusSelector.SelectedItem == null)))
                                DroneListView.ItemsSource = bl.displayDroneList();

            else if (((weightSelector.SelectedItem == null) || ((DroneStatuses)weightSelector.SelectedItem == DroneStatuses.all)) && ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all))
                DroneListView.ItemsSource = bl.displayDroneList();
            else if ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all)
                DroneListView.ItemsSource = bl.displayDrones(drone => drone.weight == (IBL.BO.WeightCategories)weightSelector.SelectedItem);
            else if ((weightSelector.SelectedItem == null) || ((DroneStatuses)weightSelector.SelectedItem == DroneStatuses.all))
                DroneListView.ItemsSource = bl.displayDrones(drone => drone.status == (IBL.BO.DroneStatuses)statusSelector.SelectedItem);
            else
                DroneListView.ItemsSource = bl.displayDrones(drone => (drone.status == (IBL.BO.DroneStatuses)statusSelector.SelectedItem) && (drone.weight == (IBL.BO.WeightCategories)weightSelector.SelectedItem));

        }

        private void weightSelection(object sender, SelectionChangedEventArgs e)
        {
            if (((weightSelector.SelectedItem == null) && (statusSelector.SelectedItem == null)))
                DroneListView.ItemsSource = bl.displayDroneList();
            else if (((statusSelector.SelectedItem == null) || ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all)) && ((DroneStatuses)weightSelector.SelectedItem == DroneStatuses.all))
                DroneListView.ItemsSource = bl.displayDroneList();
            else if ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all)
                DroneListView.ItemsSource = bl.displayDrones(drone => drone.status == (IBL.BO.DroneStatuses)statusSelector.SelectedItem);
            else if ((statusSelector.SelectedItem == null) || ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all))
                DroneListView.ItemsSource = bl.displayDrones(drone => drone.weight == (IBL.BO.WeightCategories)weightSelector.SelectedItem);
            else
                DroneListView.ItemsSource = bl.displayDrones(drone => (drone.status == (IBL.BO.DroneStatuses)statusSelector.SelectedItem) && (drone.weight == (IBL.BO.WeightCategories)weightSelector.SelectedItem));

        }

        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            new Drone(bl).ShowDialog();
            DroneListView.ItemsSource = bl.displayDroneList();
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
        }

        private void openDrone_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((IBL.BO.droneForList)(sender as ListView).SelectedItem).id;
            new Drone(bl, bl.displayDrone(id)).ShowDialog();
            DroneListView.ItemsSource = bl.displayDroneList();
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
}
