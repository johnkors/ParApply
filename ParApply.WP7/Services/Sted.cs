using System;
using System.Device.Location;
using System.Globalization;

namespace ParApply
{
    public class Sted
    {
        public string Navn { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public Uri YrUrl { get; set; }

        public GeoCoordinate GetCoordinates()
        {
            double lat = Double.Parse(Latitude, CultureInfo.InvariantCulture);
            double lon = Double.Parse(Longitude, CultureInfo.InvariantCulture);
            GeoCoordinate geo;
            try
            {
                geo = new GeoCoordinate(lat, lon);
            }
            catch (ArgumentException e)
            {
                return new GeoCoordinate(0, 0); // :)
            }
            return geo;
        }
    }
}