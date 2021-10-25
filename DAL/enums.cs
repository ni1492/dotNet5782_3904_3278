using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public enum WeightCategories { light, medium, heavy };//סוגי משקל לחבילות ורחפנים
        public enum DroneStatuses { available, maintenance, delivery };//סוגי מצבים לרחפנים
        public enum Priorities { regular, quick, urgent };//סוגי דחיפות של חבילה
        public enum Models {Mavic,Phantom,Inspire,Air,Syma,Hanvon, Propel,FPV,DJI,PowerVision,AUTEL,Skydio,Holystone};//סוגי מודלים לרחפנים באיתחול


    }
}
