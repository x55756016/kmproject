using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class FD_Relation
    {
        //exp: { Text: "儿子", Value: "20", Generate: "子辈", Side: "本人", Requird: true, Apply: true, Sort: 4, Sex: "1" },
        public string ID { get; set; }
        public string Text{get;set;}
        public string Value{get;set;}
        public string Generate{get;set;}
        public string Side{get;set;}
        public bool Requird { get; set; }
        public bool Apply { get; set; }
        public int Sort { get; set; }
        public string Sex { get; set; }
    }
}
