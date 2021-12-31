using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class ParcelAtCustomer:DependencyObject//at customer
    {
        static readonly DependencyProperty PCIDProperty = DependencyProperty.Register("Parcel_ID", typeof(int), typeof(ParcelAtCustomer));
        static readonly DependencyProperty PCWeightProperty = DependencyProperty.Register("Parcel Weight", typeof(WeightCategories), typeof(ParcelAtCustomer));
        static readonly DependencyProperty PCPriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(ParcelAtCustomer));
        static readonly DependencyProperty PCParcelStatusProperty = DependencyProperty.Register("Parcel_Status", typeof(ParcelStatus), typeof(ParcelAtCustomer));
        static readonly DependencyProperty OtherCus = DependencyProperty.Register("Other Customer", typeof(CustomerForParcel), typeof(ParcelAtCustomer));

        public int PCID { get => (int)GetValue(PCIDProperty); set => SetValue(PCIDProperty, value); }
         public WeightCategories PCWeight { get => (WeightCategories)GetValue(PCWeightProperty); set => SetValue(PCWeightProperty, value); }
        public Priorities PCPriority { get => (Priorities)GetValue(PCPriorityProperty); set => SetValue(PCPriorityProperty, value); }
        public ParcelStatus PCStatus { get => (ParcelStatus)GetValue(PCParcelStatusProperty); set => SetValue(PCParcelStatusProperty, value); }
        public CustomerForParcel OtherC { get => (CustomerForParcel)GetValue(OtherCus); set => SetValue(OtherCus, value); }
        public override string ToString()//custom print function for parcel 
        {
            return ("Parcel Id: " + PCID + ", Weight of the parcel: " + PCWeight +
                ", Priority: " + PCPriority + ", Drone Status: " + PCStatus + ", Sender/receiver: " + OtherC + "");
        }
    }
}
