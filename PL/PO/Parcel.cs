using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class Parcel: DependencyObject //for list
    {

        static readonly DependencyProperty PIDProperty = DependencyProperty.Register("Parcel_ID", typeof(int), typeof(Drone));
        static readonly DependencyProperty SNProperty = DependencyProperty.Register("Sender_Name", typeof(string), typeof(Drone));
        static readonly DependencyProperty TNProperty = DependencyProperty.Register("Target_Name", typeof(string), typeof(Drone));
        static readonly DependencyProperty PWeightProperty = DependencyProperty.Register("Parcel Weight", typeof(WeightCategories), typeof(Drone));
        static readonly DependencyProperty PriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(Drone));
        static readonly DependencyProperty PStatusProperty = DependencyProperty.Register("Parcel_Status", typeof(ParcelStatus), typeof(Drone));

        //static readonly DependencyProperty DroneIDProperty = DependencyProperty.Register("Drone ID", typeof(int), typeof(Drone));
        //static readonly DependencyProperty CreationProperty = DependencyProperty.Register("Creation", typeof(DateTime?), typeof(Drone));
        //static readonly DependencyProperty MatchProperty = DependencyProperty.Register("Match", typeof(DateTime?), typeof(Drone));
        //static readonly DependencyProperty PickupProperty = DependencyProperty.Register("Pickup", typeof(DateTime?), typeof(Drone));
        //static readonly DependencyProperty DeliveryProperty = DependencyProperty.Register("Delivery", typeof(DateTime?), typeof(Drone));

        public int PID { get => (int)GetValue(PIDProperty); set => SetValue(PIDProperty, value); }
        public string SenderName { get => (string)GetValue(SNProperty); set => SetValue(SNProperty, value); }
        public string TargetName { get => (string)GetValue(TNProperty); set => SetValue(TNProperty, value); }
        public WeightCategories PWeight { get => (WeightCategories)GetValue(PWeightProperty); set => SetValue(PWeightProperty, value); }
        public Priorities Priority { get => (Priorities)GetValue(PriorityProperty); set => SetValue(PriorityProperty, value); }
        public ParcelStatus PStatus { get => (ParcelStatus)GetValue(PStatusProperty); set => SetValue(PStatusProperty, value); }

        //public int Drone_ID { get => (int)GetValue(DroneIDProperty); set => SetValue(DroneIDProperty, value); }
        //public DateTime? Creation { get => (DateTime?)GetValue(CreationProperty); set => SetValue(CreationProperty, value); }
        //public DateTime? Match { get => (DateTime?)GetValue(MatchProperty); set => SetValue(MatchProperty, value); }
        //public DateTime? Pickup { get => (DateTime?)GetValue(PickupProperty); set => SetValue(PickupProperty, value); }
        //public DateTime? Delivery { get => (DateTime?)GetValue(DeliveryProperty); set => SetValue(DeliveryProperty, value); }

    }
}
