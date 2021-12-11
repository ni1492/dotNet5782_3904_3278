using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class location
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string convertLa(double lattitude)
        {
            string La;
            int angle;
            int minutes;
            double seconds;
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
                La = angle + "°" + minutes + "'" + seconds + "'' W";
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
                La = angle + "°" + minutes + "'" + seconds + "'' E";
            }
            return La;
        }
        public string convertLo(double longitude)
        {
            string Lo;
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
                Lo = angle + "°" + minutes + "'" + seconds + "'' S";
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

                Lo = angle + "°" + minutes + "'" + seconds + "'' N";
            }
            return Lo;
        }
        public override string ToString()
        {
            return ("Longitude: " + convertLo(Longitude) + "\nLattitude: " + convertLa(Latitude));
        }
    }
}
