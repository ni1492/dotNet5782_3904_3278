using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class UserForDisplay
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsManager { get; set; }
        public int Salt { get; set; }
        public string HashedPassword { get; set; }
        public override string ToString()
        {
            return string.Format("Id: {0}, User Name: {1}, Email Address: {2}", Id, UserName, Email);
        }
    }
}
