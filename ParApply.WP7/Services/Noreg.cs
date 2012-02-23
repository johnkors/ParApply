using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using ParApply.Business;

namespace ParApply.Services
{
    public class Noreg
    {
        private IDictionary<GeoCoordinate, Sted> _stedsDictionary;

        public Noreg()
        {
            _stedsDictionary = new Dictionary<GeoCoordinate, Sted>();
        }

        public void AddSted(Sted sted)
        {
            var geo = sted.ToGeoCoordinate();
            if (!_stedsDictionary.ContainsKey(geo))
            {
                _stedsDictionary.Add(geo, sted);
            }
            else
            {
                string huh = "huh";
            }
        }

        public Sted FirstSted()
        {
            return _stedsDictionary.Values.First();
        }

        public Sted FindClosestSted(GeoCoordinate myLocation)
        {
            var closestDistance = double.PositiveInfinity;
            Sted closestSted = null;
            foreach (var geoCoordinate in _stedsDictionary.Keys)
            {
                var distanceToThis = myLocation.GetDistanceTo(geoCoordinate);
                if (distanceToThis <= closestDistance)
                {
                    closestDistance = distanceToThis;
                    closestSted = _stedsDictionary[geoCoordinate];
                }
            }
            if (closestSted == null)
                throw new Exception("Did not find closest sted");
            return closestSted;
        }
    }
}