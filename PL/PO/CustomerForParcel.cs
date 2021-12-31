using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class CustomerForParcel:DependencyObject //for parcel
    {
        static readonly DependencyProperty CPIDProperty = DependencyProperty.Register("CustomerID", typeof(int), typeof(CustomerForParcel));

        static readonly DependencyProperty CPNameProperty = DependencyProperty.Register("Customer Name", typeof(string), typeof(CustomerForParcel));
       
        public int CPID { get => (int)GetValue(CPIDProperty); set => SetValue(CPIDProperty, value); }
        public string CPName { get => (string)GetValue(CPNameProperty); set => SetValue(CPNameProperty, value); }
        public override string ToString()
        {
            return ("Customer Id: " + CPID + "\nCustomer Name: " + CPName + "\n");
        }
    }
}
