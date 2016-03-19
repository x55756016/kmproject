using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    public class Chart
    {

        public Chart()
        {
            labels = new List<string>();
            datasets = new List<ChartData>();
        }

        public string title { get; set; }

        public List<string> labels { get; set; }

        public List<ChartData> datasets { get; set; }
    }

    public class ChartData
    {

        public ChartData()
        {
            data = new List<double>();
        }

        public string label { get; set; }

        public string fillColor { get; set; }

        public string strokeColor { get; set; }

        public string pointColor { get; set; }

        public string pointStrokeColor { get; set; }

        public string pointHighlightFill { get; set; }

        public string pointHighlightStroke { get; set; }

        public List<double> data { get; set; }
    }
}
