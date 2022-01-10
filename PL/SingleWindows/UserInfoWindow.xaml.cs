using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.SingleWindows
{
    /// <summary>
    /// Interaction logic for UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        BlApi.IBL bl;
        
            public UserInfoWindow(BlApi.IBL bl, PO.User user)
        {
            this.bl = bl;
            InitializeComponent();
            ID.Text = user.UId.ToString();
            NAME.Text = user.UName.ToString();
            EMALE.Text = user.Email.ToString();
            PHONE.Text = bl.displayCustomer(Int32.Parse(user.UId.ToString())).phone;
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
