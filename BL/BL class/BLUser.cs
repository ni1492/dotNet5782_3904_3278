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
        public UserForDisplay displayUser(string userN)
        {
            try
            {
                User user = dl.displayUsers(user => user.UserName == userN).FirstOrDefault();
                if (user == null)
                    return null;
                return new UserForDisplay()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsManager = user.IsManager,
                    Salt = user.Salt,
                    HashedPassword = user.HashedPassword
                };
            }
            catch (Exception ex) //throw - if the customer doesnt exist
            {

                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }

        }
        public IEnumerable<UserForDisplay> displayUsersList()
        {
            foreach (User user in dl.displayUsers(user=>true))
            {
                yield return new UserForDisplay()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsManager = user.IsManager,
                    Salt = user.Salt,
                    HashedPassword = user.HashedPassword
                };
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
        public void changePass(string userN, string password)
        {
            try
            {
                DO.User tempDL = dl.displayUsers(user => user.UserName == userN).FirstOrDefault();

                dl.deleteUser(tempDL.Id);
                dl.AddUser(tempDL.Id, tempDL.UserName, tempDL.Email, password, tempDL.IsManager);
            }
            catch (Exception ex) //throw - if the customer doesnt exist
            {

                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
    }
}
