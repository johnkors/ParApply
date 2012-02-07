using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ParApply
{
    public class YrService : RestService<YrData>
    {
        public void GetYrData(Sted sted, Action<Result<IEnumerable<YrData>>> OnYrDataFetched)
        {
            Get(sted.YrUrl, OnYrDataFetched);
        }

        public override IEnumerable<YrData> ParseXml(XmlReader reader)
        {
            var xml = XDocument.Load(reader);

            var twoWeeksWorthOfYrData = from weatherdata in xml.Elements("weatherdata")
                                        from forecast in weatherdata.Elements("forecast")
                                        from tabular in forecast.Elements("tabular")
                                        from time in tabular.Elements("time")
                                        select new YrData()
                                                   {
                                                       From = time.Attribute("from").Value,
                                                       To = time.Attribute("to").Value,
                                                       Period = time.Attribute("period").Value,
                                                       SymbolName = time.Elements("symbol").First().Attribute("name").Value
                                                   };
            return twoWeeksWorthOfYrData.ToList();
        }

    }
}