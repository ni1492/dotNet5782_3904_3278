﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BlApi;
using System.Globalization;
using System.Threading;
using System.Media;

namespace PL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SoundPlayer music = new SoundPlayer(@"music.wav");

        public static IBL bl = BlFactory.GetBl();
        public App()
        {
        }

    }
    #region enum
    public enum WeightCategories { all, light = 1, medium, heavy };//enum of various types of weight: light, medium, heavy
    public enum DroneStatuses { all, available = 1, maintenance, delivery };//enum of various options for drone status: available, maintenance, delivery
    public enum Priorities { all, regular = 1, quick, urgent };//enum of various options for priority: regular, quick, urgent
    public enum ParcelStatus {all, Requested=1 , Scheduled, PickedUp, Delivered }; //enum of various options for parcel status: created, matched, picked up or delivered
    #endregion
}
