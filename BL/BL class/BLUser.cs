using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BlApi
{
    public partial class BL : IBL
    {
        public void AddUser(int id, string userN, string email, string password, bool isManager)
        {
            try
            {
                dl.AddUser(id, userN, email, password, isManager);
            }
            catch (Exception ex) //catches if the ID already exists
            {
                throw new BO.exceptions.ExistException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
        public User displayUser(string userN)
        {
            try
            {
                return dl.displayUser(userN);
            }
            catch (Exception ex) //throw - if the customer doesnt exist
            {

                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }

        }
        public bool userCorrect(string userN, string password, bool isManager)
        {
            try
            {
                return dl.userCorrect(userN, password, isManager);
            }
            catch (Exception ex) //throw - if the customer doesnt exist
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
    }
}
