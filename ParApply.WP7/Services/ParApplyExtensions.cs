using System;
using System.Device.Location;
using System.Globalization;
using ParApply.Business;

namespace ParApply.Services
{
    public static class ParApplyExtensions
    {
        public static GeoCoordinate ToGeoCoordinate (this Sted sted)
        {
            double lat = Double.Parse(sted.Latitude, CultureInfo.InvariantCulture);
            double lon = Double.Parse(sted.Longitude, CultureInfo.InvariantCulture);
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