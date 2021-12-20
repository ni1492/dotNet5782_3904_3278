using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
   public class BaseStation: DependencyObject
    {
        static readonly DependencyProperty SIDProperty = DependencyProperty.Register("StationID", typeof(int), typeof(BaseStation));
        static readonly DependencyProperty SNameProperty = DependencyProperty.Register("Station Name", typeof(string), typeof(BaseStation));
        static readonly DependencyProperty SLongitudeProperty = DependencyProperty.Register("Station Longitude", typeof(string), typeof(BaseStation));
        static readonly DependencyProperty SLatitudeProperty = DependencyProperty.Register("Station Latitude", typeof(string), typeof(BaseStation));
        static readonly DependencyProperty AvailableProperty = DependencyProperty.Register("Available Charging Slots", typeof(int), typeof(BaseStation));
        static readonly DependencyProperty UsedProperty = DependencyProperty.Register("Used Charging Slots", typeof(int), typeof(BaseStation));
        //do we need to add a list of the drones in charge?????

        public int SId { get => (int)GetValue(SIDProperty); set => SetValue(SIDProperty, value); }
        public string Name { get => (string)GetValue(SNameProperty); set => SetValue(SNameProperty, value); }
        public string SLongitude { get => (string)GetValue(SLongitudeProperty); set => SetValue(SLongitudeProperty, value); }
        public string SLatitude { get => (string)GetValue(SLatitudeProperty); set => SetValue(SLatitudeProperty, value); }
        public int Available { get => (int)GetValue(AvailableProperty); set => SetValue(AvailableProperty, value); }
        public int Used { get => (int)GetValue(UsedProperty); set => SetValue(UsedProperty, value); }
    }
}
