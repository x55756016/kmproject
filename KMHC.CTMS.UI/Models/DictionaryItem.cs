using System;
using System.Collections.Generic;

namespace KMHC.CTMS.UI.Models
{
    [Serializable]
    public class DictionaryItem
    {
        public String Id { get; set; }

        public String Name { get; set; }

        public String Desc { get; set; }

        public String DataName { get; set; }

        public List<CodeItem> Items { get; set; }
    }
}
