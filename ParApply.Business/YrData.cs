using System;

namespace ParApply.Business
{
    public class YrData
    {
        public string From { get; set; }

        public string To { get; set; }

        public string GetPeriode()
        {
            var from = DateTime.Parse(From);
            var to = DateTime.Parse(To);
            return from.ToString("d.M HH:MM") + " - " + to.ToString("d.M HH:MM");
        }

        public string Period { get; set; }

        public string SymbolName { get; set; }
    }
}