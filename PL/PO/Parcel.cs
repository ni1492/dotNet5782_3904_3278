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

        static readonly DependencyProperty PIDProperty = DependencyProperty.Register("Parcel_ID", typeof(int), typeof(Parcel));
        static readonly DependencyProperty SNProperty = DependencyProperty.Register("Sender_Name", typeof(string), typeof(Parcel));
        static readonly DependencyProperty TNProperty = DependencyProperty.Register("Target_Name", typeof(string), typeof(Parcel));
        static readonly DependencyProperty PWeightProperty = DependencyProperty.Register("Parcel Weight", typeof(WeightCategories), typeof(Parcel));
        static readonly DependencyProperty PriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(Parcel));
        static readonly DependencyProperty PStatusProperty = DependencyProperty.Register("Parcel_Status", typeof(ParcelStatus), typeof(Parcel));

      
        public int PID { get => (int)GetValue(PIDProperty); set => SetValue(PIDProperty, value); }
        public string SenderName { get => (string)GetValue(SNProperty); set => SetValue(SNProperty, value); }
        public string TargetName { get => (string)GetValue(TNProperty); set => SetValue(TNProperty, value); }
        public WeightCategories PWeight { get => (WeightCategories)GetValue(PWeightProperty); set => SetValue(PWeightProperty, value); }
        public Priorities Priority { get => (Priorities)GetValue(PriorityProperty); set => SetValue(PriorityProperty, value); }
        public ParcelStatus PStatus { get => (ParcelStatus)GetValue(PStatusProperty); set => SetValue(PStatusProperty, value); }

        
    }
}
