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
using PL.SingleWindows;
using PL.PO;


namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationList.xaml
    /// </summary>
    public partial class BaseStationList : Window
    {
        #region window initialization
        IBL bl;
        public List<IGrouping<int, BaseStation>> GroupingData;

        public BaseStationList(IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            List<BaseStation> stations = (from station in bl.displayStationList() select Converter.StationPO(station)).ToList();
            DataContext = stations;
            baseStationDataGrid.Visibility = Visibility.Visible;
            baseStationGroupingDataGrid.Visibility = Visibility.Hidden;
            group.Visibility = Visibility.Visible;
            ungroup.Visibility = Visibility.Hidden;
        }
        #endregion

        #region clicks
        /// <summary>
        ///open single station window 
        /// </summary>
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           DataGridCell cell = sender as DataGridCell;
            PO.BaseStation s = cell.DataContext as PO.BaseStation;
            if(s!=null)
              new StationWindow(bl, Converter.SingleStationPO(bl.displayStation(s.BSId))).ShowDialog();
            DataContext = (from station in bl.displayStationList() select Converter.StationPO(station)).ToList();

        }
        /// <summary>
        ///close the window
        /// </summary>
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        ///open adding station window
        /// </summary>
        private void addStation_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow(bl).ShowDialog();
            DataContext = (from station in bl.displayStationList() select Converter.StationPO(station)).ToList();
        }
        /// <summary>
        ///refreshing the page
        /// </summary>
        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            DataContext = (from station in bl.displayStationList() select Converter.StationPO(station)).ToList();
        }
        #endregion

        #region grouping
        /// <summary>
        ///groping the list of stations
        /// </summary>
        private void group_Click(object sender, RoutedEventArgs e)
        {
            List<BaseStation> stations = (from station in bl.displayStationList() select Converter.StationPO(station)).ToList();
            GroupingData = stations.GroupBy(x => x.Available).ToList();
            baseStationGroupingDataGrid.DataContext = GroupingData;
            baseStationDataGrid.Visibility = Visibility.Hidden;
            baseStationGroupingDataGrid.Visibility = Visibility.Visible;
            group.Visibility = Visibility.Hidden;
            ungroup.Visibility = Visibility.Visible;
        }
        /// <summary>
        ///ungroping the list of stations
        /// </summary>
        private void ungroup_Click(object sender, RoutedEventArgs e)
        {
            baseStationGroupingDataGrid.Visibility = Visibility.Hidden;
            baseStationDataGrid.Visibility = Visibility.Visible;
            group.Visibility = Visibility.Visible;
            ungroup.Visibility = Visibility.Hidden;
        }
        /// <summary>
        ///open single station window 
        /// </summary>
        private void GrupingCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.BaseStation s = cell.DataContext as PO.BaseStation;
          if(s!=null)
            new StationWindow(bl, Converter.SingleStationPO(bl.displayStation(s.BSId))).ShowDialog();
            DataContext = (from station in bl.displayStationList() select Converter.StationPO(station)).ToList();
        }
        #endregion
       
    }
}
