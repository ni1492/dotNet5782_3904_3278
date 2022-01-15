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
        #region window initialization
        IBL bl;
        public List<IGrouping<string, Parcel>> GroupingData;

        public ParcelList(IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            List < Parcel> parcels = (from parcel in bl.displayParcelList() select Converter.ParcelPO(parcel)).ToList();
            DataContext = parcels;
            statusSelector.ItemsSource = Enum.GetValues(typeof(ParcelStatus));
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            prioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));
            parcelDataGrid.Visibility = Visibility.Visible;
            parcelGroupingDataGrid.Visibility = Visibility.Hidden;
            group.Visibility = Visibility.Visible;
            ungroup.Visibility = Visibility.Hidden;
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
            prioritySelection(prioritySelector, null);
        }
        #endregion

        #region selectors
        private void statusSelection(object sender, SelectionChangedEventArgs e)
        {
            if (statusSelector.SelectedItem == null)
                statusSelector.SelectedItem=ParcelStatus.all;
            if (weightSelector.SelectedItem == null)
                weightSelector.SelectedItem = WeightCategories.all;
            if (prioritySelector.SelectedItem == null)
                prioritySelector.SelectedItem = Priorities.all;

            else if (((WeightCategories)weightSelector.SelectedItem == WeightCategories.all) && ((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all)&& ((Priorities)prioritySelector.SelectedItem == Priorities.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcelList()
                                                                                select Converter.ParcelPO(bl));

            else if (((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all)&& ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => parcel.priority == (BO.Priorities)prioritySelector.SelectedItem)
                                                                                select Converter.ParcelPO(bl));

            else if (((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all) && ((Priorities)prioritySelector.SelectedItem == Priorities.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem)
                                                                                  select Converter.ParcelPO(bl));

            else if (((Priorities)prioritySelector.SelectedItem == Priorities.all) && ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem)
                                                                                  select Converter.ParcelPO(bl));

            else if ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all)
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem) && (parcel.priority == (BO.Priorities)prioritySelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));

            else if ((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all)
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem) && (parcel.priority == (BO.Priorities)prioritySelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));

            else if ((Priorities)prioritySelector.SelectedItem == Priorities.all)
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem) && (parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));

            else
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem) && (parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem)&&(parcel.priority==(BO.Priorities)prioritySelector.SelectedItem))
                                                                                select Converter.ParcelPO(bl));
        }
        private void weightSelection(object sender, SelectionChangedEventArgs e)
        {
            if (statusSelector.SelectedItem == null)
                statusSelector.SelectedItem = ParcelStatus.all;
            if (weightSelector.SelectedItem == null)
                weightSelector.SelectedItem = WeightCategories.all;
            if (prioritySelector.SelectedItem == null)
                prioritySelector.SelectedItem = Priorities.all;

            else if (((WeightCategories)weightSelector.SelectedItem == WeightCategories.all) && ((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all) && ((Priorities)prioritySelector.SelectedItem == Priorities.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcelList()
                                                                                 select Converter.ParcelPO(bl));

            else if (((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all) && ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => parcel.priority == (BO.Priorities)prioritySelector.SelectedItem)
                                                                                 select Converter.ParcelPO(bl));

            else if (((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all) && ((Priorities)prioritySelector.SelectedItem == Priorities.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem)
                                                                                 select Converter.ParcelPO(bl));

            else if (((Priorities)prioritySelector.SelectedItem == Priorities.all) && ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem)
                                                                                 select Converter.ParcelPO(bl));

            else if ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all)
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem) && (parcel.priority == (BO.Priorities)prioritySelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));

            else if ((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all)
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem) && (parcel.priority == (BO.Priorities)prioritySelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));

            else if ((Priorities)prioritySelector.SelectedItem == Priorities.all)
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem) && (parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));

            else
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem) && (parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem) && (parcel.priority == (BO.Priorities)prioritySelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));
        }

        private void prioritySelection(object sender, SelectionChangedEventArgs e)
        {
            if (statusSelector.SelectedItem == null)
                statusSelector.SelectedItem = ParcelStatus.all;
            if (weightSelector.SelectedItem == null)
                weightSelector.SelectedItem = WeightCategories.all;
            if (prioritySelector.SelectedItem == null)
                prioritySelector.SelectedItem = Priorities.all;

            else if (((WeightCategories)weightSelector.SelectedItem == WeightCategories.all) && ((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all) && ((Priorities)prioritySelector.SelectedItem == Priorities.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcelList()
                                                                                 select Converter.ParcelPO(bl));

            else if (((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all) && ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => parcel.priority == (BO.Priorities)prioritySelector.SelectedItem)
                                                                                 select Converter.ParcelPO(bl));

            else if (((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all) && ((Priorities)prioritySelector.SelectedItem == Priorities.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem)
                                                                                 select Converter.ParcelPO(bl));

            else if (((Priorities)prioritySelector.SelectedItem == Priorities.all) && ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all))
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem)
                                                                                 select Converter.ParcelPO(bl));

            else if ((WeightCategories)weightSelector.SelectedItem == WeightCategories.all)
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem) && (parcel.priority == (BO.Priorities)prioritySelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));

            else if ((ParcelStatus)statusSelector.SelectedItem == ParcelStatus.all)
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem) && (parcel.priority == (BO.Priorities)prioritySelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));

            else if ((Priorities)prioritySelector.SelectedItem == Priorities.all)
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem) && (parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));

            else
                parcelDataGrid.ItemsSource = new ObservableCollection<PO.Parcel>(from bl in bl.displayParcels(parcel => (parcel.status == (BO.ParcelStatus)statusSelector.SelectedItem) && (parcel.weight == (BO.WeightCategories)weightSelector.SelectedItem) && (parcel.priority == (BO.Priorities)prioritySelector.SelectedItem))
                                                                                 select Converter.ParcelPO(bl));
        }
        #endregion

        #region clear selectors
        private void ClearStatusFilledComboBox_Click(object sender, RoutedEventArgs e)
        {
            statusSelector.SelectedItem = null;
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
            prioritySelection(prioritySelector, null);

        }

        private void ClearWeightFilledComboBox_Click(object sender, RoutedEventArgs e)
        {
            weightSelector.SelectedItem = null;
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
            prioritySelection(prioritySelector, null);

        }
        private void ClearPriorityFilledComboBox_Click(object sender, RoutedEventArgs e)
        {
            prioritySelector.SelectedItem = null;
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
            prioritySelection(prioritySelector, null);

        }
        #endregion

        #region clicks
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            PO.Parcel p = cell.DataContext as PO.Parcel;
            new ParcelWindow(bl, Converter.SingleParcelPO(bl.displayParcel(p.PID))).ShowDialog();
            List<Parcel> parcels = (from parcel in bl.displayParcelList() select Converter.ParcelPO(parcel)).ToList();
            DataContext = parcels;
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
            prioritySelection(prioritySelector, null);
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(bl).ShowDialog();
            DataContext=(from parcel in bl.displayParcelList() select Converter.ParcelPO(parcel)).ToList();
            statusSelection(statusSelector, null);
            weightSelection(weightSelector, null);
            prioritySelection(prioritySelector, null);
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            DataContext = (from parcel in bl.displayParcelList() select Converter.ParcelPO(parcel)).ToList();
        }
        #endregion

        #region grouping
        private void group_Click(object sender, RoutedEventArgs e)
        {
            List<Parcel> parcels = (from parcel in bl.displayParcelList() select Converter.ParcelPO(parcel)).ToList();
            GroupingData = parcels.GroupBy(x => x.SenderName).ToList();
            parcelGroupingDataGrid.DataContext = GroupingData;
            parcelDataGrid.Visibility = Visibility.Hidden;
            UpGrid.Visibility = Visibility.Hidden;
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
            UpGrid.Visibility = Visibility.Visible;

        }
        #endregion
       
    }
}
