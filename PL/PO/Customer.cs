using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
   public class Customer:DependencyObject //for list
    {
        static readonly DependencyProperty CIDProperty = DependencyProperty.Register("CustomerID", typeof(int), typeof(Customer));
        static readonly DependencyProperty CNameProperty = DependencyProperty.Register("Customer Name", typeof(string), typeof(Customer));
        static readonly DependencyProperty PhoneProperty = DependencyProperty.Register("Phone", typeof(string), typeof(Customer));
        static readonly DependencyProperty DeliveredProperty = DependencyProperty.Register("Delivered", typeof(int), typeof(Customer));
        static readonly DependencyProperty NotDeliveredProperty = DependencyProperty.Register("Not Delivered", typeof(int), typeof(Customer));
        static readonly DependencyProperty AcceptedProperty = DependencyProperty.Register("Accepted", typeof(int), typeof(Customer));
        static readonly DependencyProperty NotAcceptedProperty = DependencyProperty.Register("Not Accepted", typeof(int), typeof(Customer));

        public int CID { get => (int)GetValue(CIDProperty); set => SetValue(CIDProperty, value); }
        public string CName { get => (string)GetValue(CNameProperty); set => SetValue(CNameProperty, value); }
        public string Phone { get => (string)GetValue(PhoneProperty); set => SetValue(PhoneProperty, value); }
        public int Delivered { get => (int)GetValue(DeliveredProperty); set => SetValue(DeliveredProperty, value); }
        public int NDelivered { get => (int)GetValue(NotDeliveredProperty); set => SetValue(NotDeliveredProperty, value); }
        public int Accepted { get => (int)GetValue(AcceptedProperty); set => SetValue(AcceptedProperty, value); }
        public int NAccepted { get => (int)GetValue(NotAcceptedProperty); set => SetValue(NotAcceptedProperty, value); }
        public override string ToString()
        {
            return ("Customer Id: " + CID + "\nCustomer Name: " + CName + "\nPhone Number: " + Phone
                + "\nDelivered: " + Delivered + "\nNOT Delivered: "
                + NDelivered + "\nAccepted: " + Accepted + "\nNOT Accepted: "
                + NAccepted + "\n");
        }

    }
}
