using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    class CustomerSingle:DependencyObject //for display
    {
        static readonly DependencyProperty CusIDProperty = DependencyProperty.Register("Customer ID", typeof(int), typeof(CustomerSingle));
        static readonly DependencyProperty CusNameProperty = DependencyProperty.Register("Customer Name", typeof(string), typeof(CustomerSingle));
        static readonly DependencyProperty CusPhoneProperty = DependencyProperty.Register("Phone", typeof(string), typeof(CustomerSingle));
        static readonly DependencyProperty CLongitudeProperty = DependencyProperty.Register("Customere Longitude", typeof(string), typeof(CustomerSingle));
        static readonly DependencyProperty CLatitudeProperty = DependencyProperty.Register("Customer Latitude", typeof(string), typeof(CustomerSingle));
        static readonly DependencyProperty FromCus = DependencyProperty.Register("Parcels From Customer", typeof(List<ParcelAtCustomer>), typeof(CustomerSingle));
        static readonly DependencyProperty ToCus = DependencyProperty.Register("Parcels To Customer", typeof(List<ParcelAtCustomer>), typeof(CustomerSingle));
        
        public int CusID { get => (int)GetValue(CusIDProperty); set => SetValue(CusIDProperty, value); }
        public string CusName { get => (string)GetValue(CusNameProperty); set => SetValue(CusNameProperty, value); }
        public string CPhone { get => (string)GetValue(CusPhoneProperty); set => SetValue(CusPhoneProperty, value); }
        public string CLongitude { get => (string)GetValue(CLongitudeProperty); set => SetValue(CLongitudeProperty, value); }
        public string CLatitude { get => (string)GetValue(CLatitudeProperty); set => SetValue(CLatitudeProperty, value); }
        public List<ParcelAtCustomer> FromC { get => (List<ParcelAtCustomer>)GetValue(FromCus); set => SetValue(FromCus, value); }
        public List<ParcelAtCustomer> ToC { get => (List<ParcelAtCustomer>)GetValue(ToCus); set => SetValue(ToCus, value); }
       
    }
}
