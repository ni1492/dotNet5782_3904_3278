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

        public ParcelList(IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            List < Parcel> parcels = (from parcel in bl.displayParcelList() select Converter.ParcelPO(parcel)).ToList();
            //parcelDataGrid.DataContext = parcels;
            DataContext = parcels;

        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.Parcel p = cell.DataContext as PO.Parcel;
            new ParcelWindow(bl, Converter.SingleParcelPO(bl.displayParcel(p.PID))).ShowDialog();
        }
    }
}
