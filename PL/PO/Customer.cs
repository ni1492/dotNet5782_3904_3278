using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
   public class Customer:DependencyObject
    {
        static readonly DependencyProperty CIDProperty = DependencyProperty.Register("CustomerID", typeof(int), typeof(Drone));
        static readonly DependencyProperty CNameProperty = DependencyProperty.Register("Customer Name", typeof(string), typeof(Drone));
        static readonly DependencyProperty PhoneProperty = DependencyProperty.Register("Phone", typeof(string), typeof(Drone));
        static readonly DependencyProperty CLongitudeProperty = DependencyProperty.Register("Customere Longitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty CLatitudeProperty = DependencyProperty.Register("Customer Latitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty DeliveredProperty = DependencyProperty.Register("Delivered", typeof(int), typeof(Drone));
        static readonly DependencyProperty NotDeliveredProperty = DependencyProperty.Register("Not Delivered", typeof(int), typeof(Drone));
        static readonly DependencyProperty AcceptedProperty = DependencyProperty.Register("Accepted", typeof(int), typeof(Drone));
        static readonly DependencyProperty NotAcceptedProperty = DependencyProperty.Register("Not Accepted", typeof(int), typeof(Drone));

        public int CID { get => (int)GetValue(CIDProperty); set => SetValue(CIDProperty, value); }
        public string CName { get => (string)GetValue(CNameProperty); set => SetValue(CNameProperty, value); }
        public string Phone { get => (string)GetValue(PhoneProperty); set => SetValue(PhoneProperty, value); }
        public string CLongitude { get => (string)GetValue(CLongitudeProperty); set => SetValue(CLongitudeProperty, value); }
        public string CLatitude { get => (string)GetValue(CLatitudeProperty); set => SetValue(CLatitudeProperty, value); }
        public int Delivered { get => (int)GetValue(DeliveredProperty); set => SetValue(DeliveredProperty, value); }
        public int NDelivered { get => (int)GetValue(NotDeliveredProperty); set => SetValue(NotDeliveredProperty, value); }
        public int Accepted { get => (int)GetValue(AcceptedProperty); set => SetValue(AcceptedProperty, value); }
        public int NAccepted { get => (int)GetValue(NotAcceptedProperty); set => SetValue(NotAcceptedProperty, value); }


    }
}
