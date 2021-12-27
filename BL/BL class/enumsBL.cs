using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace BO
    {
        public enum WeightCategories { light=1, medium, heavy };//enum of various types of weight: light, medium, heavy
        public enum Priorities { regular=1, quick, urgent };//enum of various options for priority: regular, quick, urgent
        public enum DroneStatuses { available=1, maintenance, delivery };//enum of various options for drone status: available, maintenance, delivery
        public enum ParcelStatus { Requested=1, Scheduled, PickedUp, Delivered }; //enum of various options for parcel status: created, matched, picked up or delivered
    }

