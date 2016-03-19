using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class Score
    {
        public ScoreTemplate template{get;set;}

        public int TotalScore { get; set; }

        public string Grade { get; set; }

        public string Description { get; set; }

        public string UserID { get; set; }

    }
}
