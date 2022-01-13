using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
//using System.Diagnostics;
using BO;


namespace BlApi
{
     class Simulation
    {
        static double availablePK;
        static double lightPK;
        static double mediumPK;
        static double heavyPK;
        public double chargingPH;
        BackgroundWorker timer = new BackgroundWorker();
        
        public Simulation(BlApi.BL bl,int dId, Action updateDisplay, Func<bool> stop)
        {
            availablePK = bl.availablePK;
            lightPK = bl.lightPK;
            mediumPK = bl.mediumPK;
            heavyPK = bl.heavyPK;
            chargingPH = bl.chargingPH;
        }
    }
}
