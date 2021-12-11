using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DAL.DalApi;
using DALObject;
using DO;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal getDal(string type)
        {
           if (type == "DalObject")
                return DALObject.DALObject.Instance;
            //  else(type=="DalXmi")
           // return new DalXml();
            else
                throw new DALException("type not found");

        }
    }
}
