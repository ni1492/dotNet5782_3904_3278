using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    class ParcelInDelivery:DependencyObject //in delivery
    {
        static readonly DependencyProperty PDIDProperty = DependencyProperty.Register("Parcel_ID", typeof(int), typeof(ParcelInDelivery));
        static readonly DependencyProperty SenderProperty = DependencyProperty.Register("Sender", typeof(CustomerForParcel), typeof(ParcelInDelivery));
        static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(CustomerForParcel), typeof(ParcelInDelivery));
        static readonly DependencyProperty PDarcelWeightProperty = DependencyProperty.Register("Parcel Weight", typeof(WeightCategories), typeof(ParcelInDelivery));
        static readonly DependencyProperty PDPriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(ParcelInDelivery));
        static readonly DependencyProperty PDParcelStatusProperty = DependencyProperty.Register("Parcel_Status", typeof(ParcelStatus), typeof(ParcelInDelivery));

        static readonly DependencyProperty PLongitude = DependencyProperty.Register("PickUp Longitude", typeof(int), typeof(ParcelInDelivery));
        static readonly DependencyProperty PLatitude = DependencyProperty.Register("PickUp Latitude", typeof(DateTime?), typeof(ParcelInDelivery));
        static readonly DependencyProperty DesLongitude = DependencyProperty.Register("Destination Longitude", typeof(DateTime?), typeof(ParcelInDelivery));
        static readonly DependencyProperty DesLatitude = DependencyProperty.Register("Destination Latitude", typeof(DateTime?), typeof(ParcelInDelivery));

        public int PDID { get => (int)GetValue(PDIDProperty); set => SetValue(PDIDProperty, value); }
        public string PDSenderName { get => (string)GetValue(SenderProperty); set => SetValue(SenderProperty, value); }
        public string PDTargetName { get => (string)GetValue(TargetProperty); set => SetValue(TargetProperty, value); }
        public WeightCategories PDWeight { get => (WeightCategories)GetValue(PDarcelWeightProperty); set => SetValue(PDarcelWeightProperty, value); }
        public Priorities PDPriority { get => (Priorities)GetValue(PDPriorityProperty); set => SetValue(PDPriorityProperty, value); }
        public ParcelStatus PDStatus { get => (ParcelStatus)GetValue(PDParcelStatusProperty); set => SetValue(PDParcelStatusProperty, value); }

        public int PDDrone_ID { get => (int)GetValue(PLongitude); set => SetValue(PLongitude, value); }
        public DateTime? PDCreation { get => (DateTime?)GetValue(PLatitude); set => SetValue(PLatitude, value); }
        public DateTime? PDMatch { get => (DateTime?)GetValue(DesLongitude); set => SetValue(DesLongitude, value); }
        public DateTime? PDPickup { get => (DateTime?)GetValue(DesLatitude); set => SetValue(DesLatitude, value); }

    }
}
