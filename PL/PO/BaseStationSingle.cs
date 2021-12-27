using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class BaseStationSingle: DependencyObject //for display
    {
        static readonly DependencyProperty BaseStationIDProperty = DependencyProperty.Register("BaseStationID", typeof(int), typeof(BaseStationSingle));
        static readonly DependencyProperty BSNameProperty = DependencyProperty.Register("Station Name", typeof(string), typeof(BaseStationSingle));
        static readonly DependencyProperty BSLongitudeProperty = DependencyProperty.Register("Station Longitude", typeof(string), typeof(BaseStationSingle));
        static readonly DependencyProperty BSLatitudeProperty = DependencyProperty.Register("Station Latitude", typeof(string), typeof(BaseStationSingle));
        static readonly DependencyProperty AvailableProperty = DependencyProperty.Register("Charging Slots", typeof(int), typeof(BaseStationSingle));
        static readonly DependencyProperty DronesInChargingProperty = DependencyProperty.Register("Drones In Charging", typeof(List<DroneInCharging>), typeof(BaseStationSingle));

        public int BaseSId { get => (int)GetValue(BaseStationIDProperty); set => SetValue(BaseStationIDProperty, value); }
        public string BSName { get => (string)GetValue(BSNameProperty); set => SetValue(BSNameProperty, value); }
        public string BSLongitude { get => (string)GetValue(BSLongitudeProperty); set => SetValue(BSLongitudeProperty, value); }
        public string BSLatitude { get => (string)GetValue(BSLatitudeProperty); set => SetValue(BSLatitudeProperty, value); }
        public int ChargingSlots { get => (int)GetValue(AvailableProperty); set => SetValue(AvailableProperty, value); }
        public List<DroneInCharging> InCharging { get => (List<DroneInCharging>)GetValue(DronesInChargingProperty); set => SetValue(DronesInChargingProperty, value); }
    }
}

