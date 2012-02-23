using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ParApply.Business
{
    public class StedParser
    {
        public IEnumerable<Sted> Parse(Stream noregStream)
        {
            var steder = new List<Sted>();
            char delimiter = '\t';
            using (StreamReader sr = new StreamReader(noregStream))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var elements = line.Split(delimiter);
                    var stad = elements[1];
                    var latiude = elements[8];
                    var longitude = elements[9];
                    var yrUrl = elements[12];

                    var sted = new Sted
                                   {
                                       Navn = stad,
                                       Latitude = latiude,
                                       Longitude = longitude,
                                       YrUrl = new Uri(yrUrl)
                                   };
                    steder.Add(sted);
                }
            }
            return steder;
        }
    }
}