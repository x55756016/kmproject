using System;
using System.Collections.Generic;

namespace KMHC.CTMS.UI.Models
{
    [Serializable]
    public class CodeItem
    {
        public String Value { get; set; }

        public String Name { get; set; }

        public String Desc { get; set; }

        public String Parent { get; set; }
        public List<CodeItem> Items { get; set; }
    }
}
