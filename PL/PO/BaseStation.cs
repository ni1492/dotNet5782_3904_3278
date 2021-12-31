using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
   public class BaseStation: DependencyObject// for list
    {
        static readonly DependencyProperty BSIDProperty = DependencyProperty.Register("StationID", typeof(int), typeof(BaseStation));
        static readonly DependencyProperty SNameProperty = DependencyProperty.Register("Station Name", typeof(string), typeof(BaseStation));
        static readonly DependencyProperty AvailableProperty = DependencyProperty.Register("Available Charging Slots", typeof(int), typeof(BaseStation));
        static readonly DependencyProperty UsedProperty = DependencyProperty.Register("Used Charging Slots", typeof(int), typeof(BaseStation));

        public int BSId { get => (int)GetValue(BSIDProperty); set => SetValue(BSIDProperty, value); }
        public string Name { get => (string)GetValue(SNameProperty); set => SetValue(SNameProperty, value); }
        public int Available { get => (int)GetValue(AvailableProperty); set => SetValue(AvailableProperty, value); }
        public int Used { get => (int)GetValue(UsedProperty); set => SetValue(UsedProperty, value); }
        public override string ToString()
        {
            return ("Base Station Id: " + BSId + "\nBase Station Name: " + Name + "\nNumber of Available Slots: " + Available
                + "\nNumber of Slots In Usage: " + Used + "\n");
        }

    }
}
