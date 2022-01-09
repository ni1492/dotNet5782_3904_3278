using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PL.PO;
using PL.SingleWindows;

namespace PL
{
    /// <summary>
    /// Interaction logic for droneList.xaml
    /// </summary>
    public partial class droneList : Window
    {
         IBL bl;
        public List<IGrouping<DroneStatuses, Drone>> GroupingData;
        public droneList( IBL bl)
        {
            this.bl = bl;
                InitializeComponent();
            List<Drone> drones = (from drone in bl.displayDroneList() select Converter.DronePO(drone)).ToList();
            DataContext = drones;
            droneDataGrid.Visibility = Visibility.Visible;
            groupingDataGrid.Visibility = Visibility.Hidden;
            statusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
            group.Visibility = Visibility.Visible;
            ungroup.Visibility = Visibility.Hidden;
        }

        private void statusSelection(object sender, SelectionChangedEventArgs e)
        {
            //if ((sender as ComboBox).SelectedIndex == 0)
            //    return;
            //else if(weightSelector==null || weightSelector.SelectedIndex==0)
            //       droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => drone.status == (BO.DroneStatuses)statusSelector.SelectedItem)
            //                                                                        select Converter.DronePO(bl)));
            //else
            //    droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => (drone.status == (BO.DroneStatuses)statusSelector.SelectedItem) && (drone.weight == (BO.WeightCategories)weightSelector.SelectedItem))
            //                                                                       select Converter.DronePO(bl)));
            if (statusSelector.SelectedItem == null)
                statusSelector.SelectedItem=DroneStatuses.all;
            if (weightSelector.SelectedItem == null)
                weightSelector.SelectedItem = WeightCategories.all;

            else if (((DroneStatuses)weightSelector.SelectedItem == DroneStatuses.all) && ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all))
                droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDroneList()
                                                                                select Converter.DronePO(bl)));

            else if ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all)
                droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => drone.weight == (BO.WeightCategories)weightSelector.SelectedItem)
                                                                                select Converter.DronePO(bl)));

            else if ((DroneStatuses)weightSelector.SelectedItem == DroneStatuses.all)
                droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => drone.status == (BO.DroneStatuses)statusSelector.SelectedItem)
                                                                                select Converter.DronePO(bl)));
            else
                droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => (drone.status == (BO.DroneStatuses)statusSelector.SelectedItem) && (drone.weight == (BO.WeightCategories)weightSelector.SelectedItem))
                                                                                select Converter.DronePO(bl)));

        }
        private void weightSelection(object sender, SelectionChangedEventArgs e)
        {
            //if ((sender as ComboBox).SelectedIndex == 0)
            //    return;
            //else if (statusSelector == null || statusSelector.SelectedIndex == 0)
            //    droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => drone.weight == (BO.WeightCategories)weightSelector.SelectedItem)
            //                                                                   select Converter.DronePO(bl)));
            //else
            //    droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => (drone.status == (BO.DroneStatuses)statusSelector.SelectedItem) && (drone.weight == (BO.WeightCategories)weightSelector.SelectedItem))
            //                                                                    select Converter.DronePO(bl)));
            if (statusSelector.SelectedItem == null)
                statusSelector.SelectedItem = DroneStatuses.all;
            if (weightSelector.SelectedItem == null)
                weightSelector.SelectedItem = WeightCategories.all;

            else if (((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all) && ((DroneStatuses)weightSelector.SelectedItem == DroneStatuses.all))
                droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDroneList()
                                                                                select Converter.DronePO(bl)));
            else if ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all)
                droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => drone.status == (BO.DroneStatuses)statusSelector.SelectedItem)
                                                                                select Converter.DronePO(bl)));
            else if ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all)
                droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => drone.weight == (BO.WeightCategories)weightSelector.SelectedItem)
                                                                                select Converter.DronePO(bl)));

            else
                droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => (drone.status == (BO.DroneStatuses)statusSelector.SelectedItem) && (drone.weight == (BO.WeightCategories)weightSelector.SelectedItem))
                                                                                select Converter.DronePO(bl)));
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.Drone d = cell.DataContext as PO.Drone;
            new DroneWindow(bl, Converter.SingleDronePO(bl.displayDrone(d.DId))).ShowDialog();
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
           
        }
        private void DataGridCell_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (!((sender as DataGridCell).DataContext is Drone))
                return;
            DataGridCell cell = sender as DataGridCell;
            PO.Drone d = cell.DataContext as PO.Drone;
            new DroneWindow(bl, Converter.SingleDronePO(bl.displayDrone(d.DId))).ShowDialog();
            DataContext = (from drone in bl.displayDroneList() select Converter.DronePO(drone)).ToList();
            GroupingData = (DataContext as List<Drone>).GroupBy(s => s.Status).ToList();
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);

        }
        
        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).ShowDialog();
            droneDataGrid.ItemsSource = bl.displayDroneList();
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
        }

        //private void openDrone_DoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    int id = (( BO.droneForList)(sender as ListView).SelectedItem).id;
        //    new Drone(bl, bl.displayDrone(id)).ShowDialog();
        //    droneDataGrid.ItemsSource = bl.displayDroneList();
        //    statusSelection(statusSelector, null);
        //    weightSelection(weightSelector, null);

        //}

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearStatusFilledComboBox_Click(object sender, RoutedEventArgs e)
        {
            statusSelector.SelectedItem = null;
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
            statusSelector.SelectedIndex=0;

        }

        private void ClearWeightFilledComboBox_Click(object sender, RoutedEventArgs e)
        {
            weightSelector.SelectedItem = null;
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
            weightSelector.Text = "";

        }

        private void group_Click(object sender, RoutedEventArgs e)
        {
            List<Drone> drones = (from drone in bl.displayDroneList() select Converter.DronePO(drone)).ToList();
            GroupingData = drones.GroupBy(x => x.Status).ToList();
            groupingDataGrid.DataContext = GroupingData;
            droneDataGrid.Visibility = Visibility.Hidden;
            groupingDataGrid.Visibility = Visibility.Visible;
            UpGrid.Visibility = Visibility.Hidden;
            group.Visibility = Visibility.Hidden;
            ungroup.Visibility = Visibility.Visible;
        }

        private void ungroup_Click(object sender, RoutedEventArgs e)
        {
            groupingDataGrid.Visibility = Visibility.Hidden;
            droneDataGrid.Visibility = Visibility.Visible;
            UpGrid.Visibility = Visibility.Visible;
            group.Visibility = Visibility.Visible;
            ungroup.Visibility = Visibility.Hidden;
        }
    }
    


}
