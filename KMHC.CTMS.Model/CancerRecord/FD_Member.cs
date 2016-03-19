using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class FD_Member
    {
        public FD_Relation RelationShip { get; set; }
        public string ID { get; set; }
        public string UserID { get; set; }

        public string RelationID { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public bool IsTwins { get; set; }

        public bool IsIdentical { get; set; }

        public bool IsAlive { get; set; }

        public string DeadReason { get; set; }

        public List<FD_Disease> Diseases { get; set; }

    }
}
