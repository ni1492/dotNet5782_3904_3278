using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    class DroneForParcel:DependencyObject//for parcel
    {
        static readonly DependencyProperty DPIDProperty = DependencyProperty.Register("DroneID", typeof(int), typeof(DroneForParcel));
        static readonly DependencyProperty DPBatteryProperty = DependencyProperty.Register("Battery", typeof(double), typeof(DroneForParcel));
        static readonly DependencyProperty DPLongitudeProperty = DependencyProperty.Register("Drone Longitude", typeof(string), typeof(DroneForParcel));
        static readonly DependencyProperty DPLatitudeProperty = DependencyProperty.Register("Drone Latitude", typeof(string), typeof(DroneForParcel));

        public int DPId { get => (int)GetValue(DPIDProperty); set => SetValue(DPIDProperty, value); }
        public double DPBattery { get => (double)GetValue(DPBatteryProperty); set => SetValue(DPBatteryProperty, value); }
        public string DPLongitude { get => (string)GetValue(DPLongitudeProperty); set => SetValue(DPLongitudeProperty, value); }
        public string DPLatitude { get => (string)GetValue(DPLatitudeProperty); set => SetValue(DPLatitudeProperty, value); }

    }
}
