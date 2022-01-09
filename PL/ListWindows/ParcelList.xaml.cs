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
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    public partial class ParcelList : Window
    {
        IBL bl;
        public List<IGrouping<string, Parcel>> GroupingData;

        public ParcelList(IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            List < Parcel> parcels = (from parcel in bl.displayParcelList() select Converter.ParcelPO(parcel)).ToList();
            DataContext = parcels;
            parcelDataGrid.Visibility = Visibility.Visible;
            parcelGroupingDataGrid.Visibility = Visibility.Hidden;
            group.Visibility = Visibility.Visible;
            ungroup.Visibility = Visibility.Hidden;

        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.Parcel p = cell.DataContext as PO.Parcel;
            new ParcelWindow(bl, Converter.SingleParcelPO(bl.displayParcel(p.PID))).ShowDialog();
            List<Parcel> parcels = (from parcel in bl.displayParcelList() select Converter.ParcelPO(parcel)).ToList();
            DataContext = parcels;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(bl).ShowDialog();
            DataContext=(from parcel in bl.displayParcelList() select Converter.ParcelPO(parcel)).ToList();
        }
        private void group_Click(object sender, RoutedEventArgs e)
        {
            List<Parcel> parcels = (from parcel in bl.displayParcelList() select Converter.ParcelPO(parcel)).ToList();
            GroupingData = parcels.GroupBy(x => x.SenderName).ToList();
            parcelGroupingDataGrid.DataContext = GroupingData;
            parcelDataGrid.Visibility = Visibility.Hidden;
            parcelGroupingDataGrid.Visibility = Visibility.Visible;
            group.Visibility = Visibility.Hidden;
            ungroup.Visibility = Visibility.Visible;
        }

        private void ungroup_Click(object sender, RoutedEventArgs e)
        {
            parcelGroupingDataGrid.Visibility = Visibility.Hidden;
            parcelDataGrid.Visibility = Visibility.Visible;
            group.Visibility = Visibility.Visible;
            ungroup.Visibility = Visibility.Hidden;
        }
    }
}
