using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class Parcel: DependencyObject
    {

        static readonly DependencyProperty PIDProperty = DependencyProperty.Register("ParcelID", typeof(int), typeof(Drone));
        static readonly DependencyProperty SIDProperty = DependencyProperty.Register("SenderID", typeof(int), typeof(Drone));
        static readonly DependencyProperty TIDProperty = DependencyProperty.Register("TargetID", typeof(int), typeof(Drone));
        static readonly DependencyProperty PWeightProperty = DependencyProperty.Register("Parcel Weight", typeof(WeightCategories), typeof(Drone));
        static readonly DependencyProperty PriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(Drone));
        static readonly DependencyProperty DroneIDProperty = DependencyProperty.Register("DroneID", typeof(int), typeof(Drone));
        static readonly DependencyProperty CreationProperty = DependencyProperty.Register("Creation", typeof(DateTime?), typeof(Drone));
        static readonly DependencyProperty MatchProperty = DependencyProperty.Register("Match", typeof(DateTime?), typeof(Drone));
        static readonly DependencyProperty PickupProperty = DependencyProperty.Register("Pickup", typeof(DateTime?), typeof(Drone));
        static readonly DependencyProperty DeliveryProperty = DependencyProperty.Register("Delivery", typeof(DateTime?), typeof(Drone));

        public int PID { get => (int)GetValue(PIDProperty); set => SetValue(PIDProperty, value); }
        public int SID { get => (int)GetValue(SIDProperty); set => SetValue(SIDProperty, value); }
        public int TID { get => (int)GetValue(TIDProperty); set => SetValue(TIDProperty, value); }
        public WeightCategories PWeight { get => (WeightCategories)GetValue(PWeightProperty); set => SetValue(PWeightProperty, value); }
        public Priorities Priority { get => (Priorities)GetValue(PriorityProperty); set => SetValue(PriorityProperty, value); }
        public int DroneID { get => (int)GetValue(DroneIDProperty); set => SetValue(DroneIDProperty, value); }
        public DateTime? Creation { get => (DateTime?)GetValue(CreationProperty); set => SetValue(CreationProperty, value); }
        public DateTime? Match { get => (DateTime?)GetValue(MatchProperty); set => SetValue(MatchProperty, value); }
        public DateTime? Pickup { get => (DateTime?)GetValue(PickupProperty); set => SetValue(PickupProperty, value); }
        public DateTime? Delivery { get => (DateTime?)GetValue(DeliveryProperty); set => SetValue(DeliveryProperty, value); }

    }
}
