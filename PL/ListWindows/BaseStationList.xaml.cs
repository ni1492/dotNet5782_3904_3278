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
        IBL bl;

        public BaseStationList(IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            List<BaseStation> stations = (from station in bl.displayStationList() select Converter.StationPO(station)).ToList();
            DataContext = stations;

        }

        //private void openStation_DoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    int id = ((BO.baseStationForList)(sender as ListView).SelectedItem).id;

        //    baseStationDataGrid.ItemsSource = bl.displayStationList();


        //}
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.BaseStation s = cell.DataContext as PO.BaseStation;
            new StationWindow(bl, Converter.SingleStationPO(bl.displayStation(s.BSId))).ShowDialog();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addStation_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow(bl).ShowDialog();
            baseStationDataGrid.ItemsSource = bl.displayStationList();
        }
    }
}
