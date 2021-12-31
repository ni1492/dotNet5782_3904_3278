using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class DroneSingle:DependencyObject //for display
    {
        static readonly DependencyProperty DroneIDProperty = DependencyProperty.Register("DroneID", typeof(int), typeof(DroneSingle));
        static readonly DependencyProperty DModelProperty = DependencyProperty.Register("Model", typeof(string), typeof(DroneSingle));
        static readonly DependencyProperty DBatteryProperty = DependencyProperty.Register("Battery", typeof(double), typeof(DroneSingle));
        static readonly DependencyProperty DMaxWeightProperty = DependencyProperty.Register("Max Weight", typeof(WeightCategories), typeof(DroneSingle));
        static readonly DependencyProperty DStatusProperty = DependencyProperty.Register("Status", typeof(DroneStatuses), typeof(DroneSingle));
        static readonly DependencyProperty DroneLongitudeProperty = DependencyProperty.Register("Drone Longitude", typeof(string), typeof(DroneSingle));
        static readonly DependencyProperty DroneLatitudeProperty = DependencyProperty.Register("Drone Latitude", typeof(string), typeof(DroneSingle));
        static readonly DependencyProperty DParcelProperty = DependencyProperty.Register("Parcel", typeof(ParcelInDelivery), typeof(DroneSingle));

        public int DId { get => (int)GetValue(DroneIDProperty); set => SetValue(DroneIDProperty, value); }
        public string Model { get => (string)GetValue(DModelProperty); set => SetValue(DModelProperty, value); }
        public double Battery { get => (double)GetValue(DBatteryProperty); set => SetValue(DBatteryProperty, value); }
        public WeightCategories Weight { get => (WeightCategories)GetValue(DMaxWeightProperty); set => SetValue(DMaxWeightProperty, value); }
        public DroneStatuses Status { get => (DroneStatuses)GetValue(DStatusProperty); set => SetValue(DStatusProperty, value); }
        public string DLongitude { get => (string)GetValue(DroneLongitudeProperty); set => SetValue(DroneLongitudeProperty, value); }
        public string DLatitude { get => (string)GetValue(DroneLatitudeProperty); set => SetValue(DroneLatitudeProperty, value); }
        public ParcelInDelivery Parcel { get => (ParcelInDelivery)GetValue(DParcelProperty); set => SetValue(DParcelProperty, value); }
        public override string ToString()//custom print function for drone 
        {
            return ("Drone Id: " + DId + "\nDrone Model: " + Model + "\nWeight: " + Weight
            + "\nDrone Battery:" + Battery + "\nDrone status: " + Status + "\nParcel:" + Parcel
            + "\nCurrent Location:" + DLongitude+","+ DLatitude + "\n");
        }

    }
}
