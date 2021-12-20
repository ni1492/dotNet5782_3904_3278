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

namespace PL
{
    /// <summary>
    /// Interaction logic for signInWindow.xaml
    /// </summary>
    public partial class signInWindow : Window
    {
        public signInWindow(BlApi.IBL bl,bool x)
        {
            if(x)

            InitializeComponent();
        }

        private void enterUser(object sender, RoutedEventArgs e)
        {

        }

        private void enterAdmin(object sender, RoutedEventArgs e)
        {

        }
    }
}
