using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace DO
    {
        public class Sexagesimal
        {
            public string Longitude;
            public string Lattitude;
            public Sexagesimal(double longitude, double lattitude)
            {
                int angle;
                int minutes;
                double seconds;
                if (longitude < 0)
                {
                    longitude *= -1;
                    angle = (int)longitude;
                    longitude -= angle;
                    longitude *= 60;
                    minutes = ((int)longitude);
                    longitude -= minutes;
                    longitude *= 60;
                    seconds = longitude;
                    Longitude = angle + "°" + minutes + "'" + seconds + "'' S";
                }
                else
                {
                    angle = (int)longitude;
                    longitude -= angle;
                    longitude *= 60;
                    minutes = ((int)longitude);
                    longitude -= minutes;
                    longitude *= 60;
                    seconds = longitude;

                    Longitude = angle + "°" + minutes + "'" + seconds + "'' N";
                }
                if (lattitude < 0)
                {
                    lattitude *= -1;
                    angle = (int)lattitude;
                    lattitude -= angle;
                    lattitude *= 60;
                    minutes = ((int)lattitude);
                    lattitude -= minutes;
                    lattitude *= 60;
                    seconds = lattitude;
                    Lattitude = angle + "°" + minutes + "'" + seconds + "'' W";
                }
                else
                {
                    angle = (int)lattitude;
                    lattitude -= angle;
                    lattitude *= 60;
                    minutes = ((int)lattitude);
                    lattitude -= minutes;
                    lattitude *= 60;
                    seconds = lattitude;
                    Lattitude = angle + "°" + minutes + "'" + seconds + "'' E";
                }
            }
            public override string ToString()
            {
                return ("Longitude: " + Longitude + "\nLattitude: " + Lattitude);
            }
        }
    }
