﻿using System;
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
        #region window initialization
        IBL bl;
        public List<IGrouping<DroneStatuses, Drone>> GroupingData;
        public droneList(IBL bl)
        {
            lock (bl)
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
        }
        #endregion

        #region selectors
       /// <summary>
       /// show only the drones that their status equal to the selection
       /// </summary>
        private void statusSelection(object sender, SelectionChangedEventArgs e)
        {

            lock(bl)
            {
                if (statusSelector.SelectedItem == null)
                    statusSelector.SelectedItem = DroneStatuses.all;
                if (weightSelector.SelectedItem == null)
                    weightSelector.SelectedItem = WeightCategories.all;

                else if (((WeightCategories)weightSelector.SelectedItem == WeightCategories.all) && ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all))
                    droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDroneList()
                                                                                    select Converter.DronePO(bl)));

                else if ((DroneStatuses)statusSelector.SelectedItem == DroneStatuses.all)
                    droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => drone.weight == (BO.WeightCategories)weightSelector.SelectedItem)
                                                                                    select Converter.DronePO(bl)));

                else if ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all)
                    droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => drone.status == (BO.DroneStatuses)statusSelector.SelectedItem)
                                                                                    select Converter.DronePO(bl)));
                else
                    droneDataGrid.ItemsSource = new ObservableCollection<PO.Drone>((from bl in bl.displayDrones(drone => (drone.status == (BO.DroneStatuses)statusSelector.SelectedItem) && (drone.weight == (BO.WeightCategories)weightSelector.SelectedItem))
                                                                                    select Converter.DronePO(bl)));
            }
            
        }
        /// <summary>
        /// show only the drones that their weight equal to the selection
        /// </summary>
        private void weightSelection(object sender, SelectionChangedEventArgs e)
        {
            lock(bl)
            {
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
            
        }
        #endregion

        #region clicks
        /// <summary>
        ///open single drone window 
        /// </summary>
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lock (bl)
            {
                droneDataGrid.DataContext = bl.displayDroneList();

                DataGridCell cell = sender as DataGridCell;
                PO.Drone d = cell.DataContext as PO.Drone;
                if (d != null)
                {
                    new DroneWindow(bl, Converter.SingleDronePO(bl.displayDrone(d.DId))).Show();
                }
                statusSelection(statusSelector, null);
                weightSelection(weightSelector, null);
                droneDataGrid.DataContext = bl.displayDroneList();

            }
        }
        
        /// <summary>
        ///open adding drone window
        /// </summary>
        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                new DroneWindow(bl).ShowDialog();
                droneDataGrid.ItemsSource = bl.displayDroneList();
                statusSelection(statusSelector, null);
                weightSelection(weightSelector, null);
            }
        }
        /// <summary>
        ///refreshing the page
        /// </summary>
        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            droneDataGrid.ItemsSource = bl.displayDroneList();
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
        }
        /// <summary>
        ///close the window
        /// </summary>
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region clear selectors
        /// <summary>
        /// clear the status selection
        /// </summary>
        private void ClearStatusFilledComboBox_Click(object sender, RoutedEventArgs e)
        {
            statusSelector.SelectedItem = null;
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
        }
        /// <summary>
        /// clear the weight selection
        /// </summary>
        private void ClearWeightFilledComboBox_Click(object sender, RoutedEventArgs e)
        {
            weightSelector.SelectedItem = null;
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
        }
        #endregion

        #region grouping
        /// <summary>
        ///groping the list of drones
        /// </summary>
        private void group_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
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
        }
        /// <summary>
        ///ungroping the list of drones
        /// </summary>
        private void ungroup_Click(object sender, RoutedEventArgs e)
        {
            groupingDataGrid.Visibility = Visibility.Hidden;
            droneDataGrid.Visibility = Visibility.Visible;
            UpGrid.Visibility = Visibility.Visible;
            group.Visibility = Visibility.Visible;
            ungroup.Visibility = Visibility.Hidden;
        }
        #endregion
        
    }
}
