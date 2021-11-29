﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using IDAL.DO;
using DAL.IDAL;
using IBL.BO;
namespace IBL
{
    public partial class BL:IBL
    {
        public IDal dl;
        public List<droneForList> drones;
        public double availablePK;
        public double lightPK;
        public double mediumPK;
        public double heavyPK;
        public double chargingPH;
        public BL()
        {
            dl = new DALObject.DALObject();
            drones = new List<droneForList>();
            double[] powerUse = dl.powerUse();
            availablePK = powerUse[0];
            lightPK = powerUse[1];
            mediumPK = powerUse[2];
            heavyPK = powerUse[3];
            chargingPH = powerUse[4];
            
        }
        private void initializeDrone()
        {
            foreach (var item in dl.PrintAllDrone())
            {
                drones.Add(new droneForList
                {
                    id = item.Id,
                    model = item.Model,
                    weight = (WeightCategories)item.MaxWeight
                });
            }
            foreach (var item in drones)
            {
                if(item.)
            }

        }
    }
}
