using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DAL.DalApi;
using DALObject;
using DO;
using DALXML;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal getDal(bool useXML)
        {
            if (useXML)
                return DALXML.DALXML.Instance;
            else
                return DALObject.DALObject.Instance;

        }
    }
}
