using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
  public  class DroneInCharging:DependencyObject//for charging
    {
        static readonly DependencyProperty DCIDProperty = DependencyProperty.Register("DroneID", typeof(int), typeof(DroneInCharging));
        static readonly DependencyProperty DCBatteryProperty = DependencyProperty.Register("Battery", typeof(double), typeof(DroneInCharging));
       
        public int DCId { get => (int)GetValue(DCIDProperty); set => SetValue(DCIDProperty, value); }
        public double DCBattery { get => (double)GetValue(DCBatteryProperty); set => SetValue(DCBatteryProperty, value); }
       
    }
}
