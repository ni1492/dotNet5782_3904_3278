using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class Drone: DependencyObject
    {
        static readonly DependencyProperty DIDProperty = DependencyProperty.Register("DroneID", typeof(int), typeof(Drone));
        static readonly DependencyProperty ModelProperty = DependencyProperty.Register("Model", typeof(string), typeof(Drone));
        static readonly DependencyProperty BatteryProperty = DependencyProperty.Register("Battery", typeof(double), typeof(Drone));
        static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(DroneStatuses), typeof(Drone));
        static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("Longitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("Latitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty ParcelProperty = DependencyProperty.Register("ParcelId", typeof(int), typeof(Drone));

        public int DId { get => (int)GetValue(DIDProperty); set => SetValue(DIDProperty, value); }
        public string Model { get => (string)GetValue(ModelProperty); set => SetValue(ModelProperty, value); }
        public double Battery { get => (double)GetValue(BatteryProperty); set => SetValue(BatteryProperty, value); }
        public DroneStatuses Status { get => (DroneStatuses)GetValue(StatusProperty); set => SetValue(StatusProperty, value); }
        public string Longitude { get => (string)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }
        public string Latitude { get => (string)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
        public int Parcel { get => (int)GetValue(ParcelProperty); set => SetValue(ParcelProperty, value); }




    }
}
