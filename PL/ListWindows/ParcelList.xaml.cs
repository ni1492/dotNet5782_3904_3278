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

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    public partial class ParcelList : Window
    {
        IBL bl;

        public ParcelList(IBL bl, ObservableCollection<PO.Parcel> parcels)
        {
            this.bl = bl;
            InitializeComponent();
            parcelDataGrid.DataContext = parcels;
        }
    }
}
