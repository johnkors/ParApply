using System;
using System.IO;

namespace ParApply
{
    public class NorgeParser
    {
        public Noreg Parse(Stream noregStream)
        {
            var norge = new Noreg();
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
                    norge.AddSted(sted);
                }
            }
            return norge;
        }
    }
}