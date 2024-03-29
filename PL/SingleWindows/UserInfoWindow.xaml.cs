﻿using System;
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
using System.Net.Mail;

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
            lock (bl)
            {
                this.bl = bl;
                InitializeComponent();
                info.Visibility = Visibility.Visible;
                Actions.Visibility = Visibility.Hidden;
                passWarning.Visibility = Visibility.Hidden;
                ID.Text = user.UId.ToString();
                NAME.Text = user.UName.ToString();
                EMAIL.Text = user.Email.ToString();
                PHONE.Text = bl.displayCustomer(Int32.Parse(user.UId.ToString())).phone;
            }
        }
        #region clicks
        /// <summary>
        /// close the window
        /// </summary>
        private void close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// allow to change the password
        /// </summary>
        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            info.Visibility = Visibility.Hidden;
            Actions.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// changing password
        /// </summary>
        private void newPass_Click(object sender, RoutedEventArgs e)
        {
            if (checkPass(newPass.Text))
            {
                passWarning.Visibility = Visibility.Hidden;

                try
                {
                    lock (bl)
                    {
                        bl.changePass(NAME.Text, newPass.Text);
                    }
                    MessageBox.Show("password changed- you'll receive an email about the change");
                   
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                    this.Close();

                }

                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(EMAIL.Text);
                    mail.From = new MailAddress("DragoDroneDelivery@gmail.com");
                    mail.Subject = "password changed";
                    mail.Body = @"<p>Your password has been changed.<br />Your new password is:</p>
<p><strong>" + newPass.Text + @"</strong></p>
<p>If this wasn't you, we advise you to go and reset your password!</p>";
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Credentials = new System.Net.NetworkCredential("DragoDroneDelivery@gmail.com", "DRAGODRONE");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();

                }
            }
            else
            {
                passWarning.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// check if the password is right
        /// </summary>
        private bool checkPass(string text)
        {
            try
            {
                if (text == null)
                    return false;
                if (text.Length < 8)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        #endregion
    }
}
