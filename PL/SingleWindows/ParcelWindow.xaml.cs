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

namespace PL.SingleWindows
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        BlApi.IBL bl;

        public ParcelWindow(BlApi.IBL bl, BO.parcel parcel)//action grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Hidden;
        }
        public ParcelWindow(BlApi.IBL bl)//add grid
        {
            this.bl = bl;
            InitializeComponent();
            Actions.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Visible;
        }
        private void IDTextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void SENDERTextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void TARGETTextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void CTextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void MTextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void PTextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void DTextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void addParcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void closeA_click(object sender, RoutedEventArgs e)
        {

        }
        private void updateA_click(object sender, RoutedEventArgs e)
        {

        }
        private void deleteA_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
