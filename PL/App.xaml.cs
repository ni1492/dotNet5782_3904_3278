using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BlApi;
using System.Globalization;
using System.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IBL bl = BlFactory.GetBl();
        public App()
        {
            //CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            //ci.DateTimeFormat.ShortDatePattern = "DD/MM/YYYY HH:MM:SS";
            //Thread.CurrentThread.CurrentCulture = ci;
        }

    }
    public enum WeightCategories { all, light = 1, medium, heavy };//enum of various types of weight: light, medium, heavy
    public enum DroneStatuses { all, available = 1, maintenance, delivery };//enum of various options for drone status: available, maintenance, delivery
    public enum Priorities { regular = 1, quick, urgent };//enum of various options for priority: regular, quick, urgent
    public enum ParcelStatus { Requested=1 , Scheduled, PickedUp, Delivered }; //enum of various options for parcel status: created, matched, picked up or delivered
}
