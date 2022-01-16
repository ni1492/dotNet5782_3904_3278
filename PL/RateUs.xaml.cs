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
    /// Interaction logic for RateUs.xaml
    /// </summary>
    public partial class RateUs : Window
    {
        public RateUs()
        {
            InitializeComponent();
            rate.Visibility = Visibility.Visible;
        }
        #region clicks
        /// <summary>
        ///thank theuser and close the rating window 
        /// </summary>
        public void Rate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("thank you");
            this.Close();
        }
        /// <summary>
        ///close the rating window 
        /// </summary>
        private void NotNow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
