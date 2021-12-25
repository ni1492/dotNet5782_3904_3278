//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace PL.PO
//{
//    class BaseStationForDisplay
//    {
//        static readonly DependencyProperty BaseStationIDProperty = DependencyProperty.Register("BaseStationID", typeof(int), typeof(BaseStation));
//        static readonly DependencyProperty BSNameProperty = DependencyProperty.Register("Station Name", typeof(string), typeof(BaseStation));
//        static readonly DependencyProperty BSLongitudeProperty = DependencyProperty.Register("Station Longitude", typeof(string), typeof(BaseStation));
//        static readonly DependencyProperty BSLatitudeProperty = DependencyProperty.Register("Station Latitude", typeof(string), typeof(BaseStation));
//        static readonly DependencyProperty AvailableProperty = DependencyProperty.Register("Available Charging Slots", typeof(int), typeof(BaseStation));
//        static readonly DependencyProperty DronesInChargingProperty = DependencyProperty.Register("Used Charging Slots", typeof(List<DroneInCharging>), typeof(BaseStation));

//        public int BSId { get => (int)GetValue(BaseStationIDProperty); set => SetValue(BaseStationIDProperty, value); }
//        public string Name { get => (string)GetValue(BSNameProperty); set => SetValue(BSNameProperty, value); }
//        public string SLongitude { get => (string)GetValue(BSLongitudeProperty); set => SetValue(BSLongitudeProperty, value); }
//        public string SLatitude { get => (string)GetValue(BSLatitudeProperty); set => SetValue(BSLatitudeProperty, value); }
//        public int Available { get => (int)GetValue(AvailableProperty); set => SetValue(AvailableProperty, value); }
//        public int Used { get => (int)GetValue(UsedProperty); set => SetValue(UsedProperty, value); }
//    }
//}
//}
