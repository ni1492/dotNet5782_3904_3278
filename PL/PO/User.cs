using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PL.PO
{
    public class User: DependencyObject
    {
        static readonly DependencyProperty UIDProperty = DependencyProperty.Register("User ID", typeof(int), typeof(User));
        static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("User Name", typeof(string), typeof(User));
        static readonly DependencyProperty EmailProperty = DependencyProperty.Register("Email Address", typeof(string), typeof(User));
        static readonly DependencyProperty IsMAnagerProperty = DependencyProperty.Register("Is Manager", typeof(bool), typeof(User));
        static readonly DependencyProperty SaltProperty = DependencyProperty.Register("Salt", typeof(int), typeof(User));
        static readonly DependencyProperty HashedPasswordProperty = DependencyProperty.Register("Hashed Password", typeof(string), typeof(User));

        public int UId { get => (int)GetValue(UIDProperty); set => SetValue(UIDProperty, value); }
        public string UName { get => (string)GetValue(UserNameProperty); set => SetValue(UserNameProperty, value); }
        public string Email { get => (string)GetValue(EmailProperty); set => SetValue(EmailProperty, value); }
        public bool IsManager { get => (bool)GetValue(IsMAnagerProperty); set => SetValue(IsMAnagerProperty, value); }
        public int Salt { get => (int)GetValue(SaltProperty); set => SetValue(SaltProperty, value); }
        public string HashedPassword { get => (string)GetValue(HashedPasswordProperty); set => SetValue(HashedPasswordProperty, value); }

        public override string ToString()
        {
            return string.Format("Id: {0}, User Name: {1}, Email Address: {2}", UId, UName, Email);
        }
    }
}
