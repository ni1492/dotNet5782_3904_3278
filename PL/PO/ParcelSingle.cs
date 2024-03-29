﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PL.PO
{
    public class ParcelSingle:DependencyObject//for display
    {
        static readonly DependencyProperty ParcelIDProperty = DependencyProperty.Register("Parcel_ID", typeof(string), typeof(ParcelSingle));
        static readonly DependencyProperty SenderNProperty = DependencyProperty.Register("Sender_Name", typeof(CustomerForParcel), typeof(ParcelSingle));
        static readonly DependencyProperty TargetNProperty = DependencyProperty.Register("Target_Name", typeof(CustomerForParcel), typeof(ParcelSingle));
        static readonly DependencyProperty ParcelWeightProperty = DependencyProperty.Register("Parcel Weight", typeof(WeightCategories), typeof(ParcelSingle));
        static readonly DependencyProperty PPriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(ParcelSingle));
        //static readonly DependencyProperty ParcelStatusProperty = DependencyProperty.Register("Parcel_Status", typeof(ParcelStatus), typeof(ParcelSingle));

        static readonly DependencyProperty DroneIDProperty = DependencyProperty.Register("Drone ID", typeof(int), typeof(ParcelSingle));
        static readonly DependencyProperty CreationProperty = DependencyProperty.Register("Creation", typeof(DateTime?), typeof(ParcelSingle));
        static readonly DependencyProperty MatchProperty = DependencyProperty.Register("Match", typeof(DateTime?), typeof(ParcelSingle));
        static readonly DependencyProperty PickupProperty = DependencyProperty.Register("Pickup", typeof(DateTime?), typeof(ParcelSingle));
        static readonly DependencyProperty DeliveryProperty = DependencyProperty.Register("Delivery", typeof(DateTime?), typeof(ParcelSingle));

        public string PSID { get => (string)GetValue(ParcelIDProperty); set =>SetValue(ParcelIDProperty, value); } 
        public CustomerForParcel PSSender { get => (CustomerForParcel)GetValue(SenderNProperty); set => SetValue(SenderNProperty, value); }
        public CustomerForParcel PSTarget { get => (CustomerForParcel)GetValue(TargetNProperty); set => SetValue(TargetNProperty, value); }
        public WeightCategories PSWeight { get => (WeightCategories)GetValue(ParcelWeightProperty); set => SetValue(ParcelWeightProperty, value); }
        public Priorities PSPriority { get => (Priorities)GetValue(PPriorityProperty); set => SetValue(PPriorityProperty, value); }

        public int PSDrone_ID { get => (int)GetValue(DroneIDProperty); set => SetValue(DroneIDProperty, value); } //add if that says- no drone matched - when id is 0 or -1
        public DateTime? PSCreation { get => (DateTime?)GetValue(CreationProperty); set => SetValue(CreationProperty, value); }
        public DateTime? PSMatch { get => (DateTime?)GetValue(MatchProperty); set => SetValue(MatchProperty, value); }
        public DateTime? PSPickup { get => (DateTime?)GetValue(PickupProperty); set => SetValue(PickupProperty, value); }
        public DateTime? PSDelivery { get => (DateTime?)GetValue(DeliveryProperty); set => SetValue(DeliveryProperty, value); }
        public override string ToString()//custom print function for parcel 
        {
            return ("Parcel Id: " + PSID + "\nSender: " + PSSender + "\nReceiver: " + PSTarget + "\nWeight of the parcel: " + PSWeight +
                "\nPriority: " + PSPriority + "\nDrone: " + PSDrone_ID + "\nCreation Time: " + PSCreation + "\nMatch Time: " + PSMatch +
                "\nPickedUp Time:" + PSPickup + "\nDelivered Time: " + PSDelivery + "\n");
        }
    }
}
