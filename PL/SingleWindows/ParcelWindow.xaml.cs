﻿using System;
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
using BO;
using PL.PO;

namespace PL.SingleWindows
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        #region initialization
        BlApi.IBL bl;

        public ParcelWindow(BlApi.IBL bl)//add grid
        {
            lock (bl)
            {
                this.bl = bl;
                InitializeComponent();
                Actions.Visibility = Visibility.Hidden;
                Add.Visibility = Visibility.Visible;
                sender.Visibility = Visibility.Visible;
                entersender.Visibility = Visibility.Visible;
                WEIGHT.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
                PRIORITY.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            }
        }
        public ParcelWindow(BlApi.IBL bl, string name)//add grid for user
        {
            lock (bl)
            {
                this.bl = bl;
                InitializeComponent();
                Actions.Visibility = Visibility.Hidden;
                Add.Visibility = Visibility.Visible;
                sender.Visibility = Visibility.Hidden;
                entersender.Visibility = Visibility.Hidden;
                SENDER.Text = name;
                WEIGHT.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
                PRIORITY.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            }
        }
        public ParcelWindow(BlApi.IBL bl, PO.ParcelSingle parcel)//action grid
        {
            lock (bl)
            {
                this.bl = bl;
                InitializeComponent();
                Actions.Visibility = Visibility.Visible;
                Add.Visibility = Visibility.Hidden;
                viewID.Text = parcel.PSID.ToString();
                viewSENDER.Text = parcel.PSSender.CPName;
                viewTARGET.Text = parcel.PSTarget.CPName;
                viewWEIGHT.Text = parcel.PSWeight.ToString();
                viewPRIOR.Text = parcel.PSPriority.ToString();
                viewCREATE.Text = parcel.PSCreation.ToString();
                viewMATCH.Text = parcel.PSMatch.ToString();
                viewPICK.Text = parcel.PSPickup.ToString();
                viewDELIV.Text = parcel.PSDelivery.ToString();
                InitializeActionsButton(parcel);
                if (parcel.PSDrone_ID == 0)
                {
                    viewDRONE.Text = "no drone matched";
                    OPDrone.Visibility = Visibility.Hidden;
                }
                else
                {
                    viewDRONE.Text = parcel.PSDrone_ID.ToString();
                    OPDrone.Visibility = Visibility.Visible;
                }
                if (viewDELIV.Text != "")
                {
                    OPCus1.Visibility = Visibility.Hidden;
                    OPCus2.Visibility = Visibility.Hidden;
                    OPDrone.Visibility = Visibility.Hidden;
                }
                else
                {
                    OPCus1.Visibility = Visibility.Visible;
                    OPCus2.Visibility = Visibility.Visible;
                }
            }
        }
        private void InitializeActionsButton(PO.ParcelSingle parcel)
        {

            if (parcel == null)
                throw new ArgumentNullException("No drone");

            if (parcel.PSDelivery!=null)
            {
                UPDATE.Visibility = Visibility.Hidden;
            }
            else if (parcel.PSPickup!=null)
            {
                UPDATE.Visibility = Visibility.Visible;
                UPDATE.Content = "deliverd";
                UPDATE.Click += isDelivered_Click;
            }
            else if (parcel.PSMatch != null)
            {
                UPDATE.Visibility = Visibility.Visible;
                UPDATE.Content = "picked-up";
                UPDATE.Click += isPickedup_Click;
            }
            else
            {
                UPDATE.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region clicks
        /// <summary>
        ///update pick up time
        /// </summary>
        public void isPickedup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock (bl)
                {
                    bl.pickupParcel(Int32.Parse(viewDRONE.Text));
                }
                this.Close();
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        ///update delivery time
        /// </summary>
        public void isDelivered_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock (bl)
                {
                    bl.deliverParcel(Int32.Parse(viewDRONE.Text));
                }
                this.Close();
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        ///add the parcel
        /// </summary>
        private void addParcel_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (checkName(SENDER.Text) && checkName(TARGET.Text) && PRIORITY.SelectedItem != null && WEIGHT.SelectedItem != null)
                {
                    BO.parcelForList p = new BO.parcelForList
                    {
                        id = 0,
                        sender = SENDER.Text,
                        receiver = TARGET.Text,
                        weight = (BO.WeightCategories)WEIGHT.SelectedItem,
                        priority = (BO.Priorities)PRIORITY.SelectedItem,
                        status = BO.ParcelStatus.Requested
                    };
                    lock (bl)
                    {
                        bl.addParcel(p);
                    }
                    MessageBox.Show("Added");
                    this.Close();
                    return;
                }
                else
                    MessageBox.Show("incorrect input - add parcel failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }
        /// <summary>
        ///close the window without doing anything
        /// </summary>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        ///delete parcel
        /// </summary>
        private void deleteA_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id;
                Int32.TryParse(viewID.Text, out id);
                lock (bl)
                {
                    bl.deleteParcel(id);
                }
                MessageBox.Show("parcel deleted");
                this.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        ///open sender window
        /// </summary>
        private void openSender_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                foreach (var c in bl.displayCustomerList())
                {

                    if (c.name == viewSENDER.Text)
                    {
                        new CustomerWindow(bl, Converter.SingleCustomerPO(bl.displayCustomer(c.id))).ShowDialog();
                        break;
                    }
                }
            }

        }
        /// <summary>
        ///open target window
        /// </summary>
        private void openTarget_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                foreach (var c in bl.displayCustomerList())
                {

                    if (c.name == viewTARGET.Text)
                    {
                        new CustomerWindow(bl, Converter.SingleCustomerPO(bl.displayCustomer(c.id))).ShowDialog();
                        break;
                    }
                }
            }
        }
        /// <summary>
        ///open drone window
        /// </summary>
        private void openDrone_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                int id;
                Int32.TryParse(viewDRONE.Text, out id);
                new DroneWindow(bl, Converter.SingleDronePO(bl.displayDrone(id))).ShowDialog();
            }
        }
        #endregion

        #region text changed
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
        private void SENDERTextChanged(object sender, RoutedEventArgs e)
        {
            if (checkName(SENDER.Text))
            {
                SENDER.BorderBrush = Brushes.GreenYellow;
                SENDER.Background = Brushes.White;
            }
            else
            {
                SENDER.BorderBrush = Brushes.DarkRed;
                SENDER.Background = Brushes.Red;
            }
        }
        /// <summary>
        ///if the input isnt possible, the box become red
        /// </summary>
        private void TARGETTextChanged(object sender, RoutedEventArgs e)
        {
            if (checkName(TARGET.Text))
            {
                TARGET.BorderBrush = Brushes.GreenYellow;
                TARGET.Background = Brushes.White;
            }
            else
            {
                TARGET.BorderBrush = Brushes.DarkRed;
                TARGET.Background = Brushes.Red;
            }
        }
        #endregion

        #region check
        /// <summary>
        ///check if the name is possible
        /// </summary>
        private bool checkName(string text)
        {
            lock (bl)
            {
                if ((text == null) || (text == ""))
                    return false;
                if (bl.displayCustomerList().FirstOrDefault(c => c.name == text) == null)
                    return false;
                return true;
            }
        }
        /// <summary>
        ///check if the id is possible
        /// </summary>
        private bool checkId(string text)
        {
            try
            {
                lock (bl)
                {
                    if (text == null)
                        return false;
                    if (!int.TryParse(text, out int id))
                        return false;
                    if (id <= 0)
                        return false;
                    if (bl.displayParcelList().FirstOrDefault(p => p.id == id) != null)
                        return false;
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
        #endregion
       
    }
}
