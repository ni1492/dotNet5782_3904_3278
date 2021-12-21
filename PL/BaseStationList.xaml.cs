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
    /// Interaction logic for BaseStationList.xaml
    /// </summary>
    public partial class BaseStationList : Window
    {
        IBL bl;

        public BaseStationList(IBL bl, ObservableCollection<PO.BaseStation> stations)
        {
            this.bl = bl;
            InitializeComponent();
            baseStationDataGrid.DataContext = stations;
        }
    }
}
