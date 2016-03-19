using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    [Serializable]
    [XmlRoot("request")]
    public class request
    {
        [XmlElement("userId")]
        public string userId { get; set; }

        [XmlElement("password")]
        public string password { get; set; }

        [XmlElement("templateId")]
        public string templateId { get; set; }

        [XmlElement("phone")]
        public List<string> phone { get; set; }

        [XmlElement("port")]
        public string port { get; set; }

        [XmlElement("data")]
        public XmlNode data { get; set; }

        [XmlElement("signature")]
        public string signature { get; set; }
    }

    [Serializable]
    [XmlRoot("response")]
    public class response
    {
        [XmlElement("retCode")]
        public string retCode { get; set; }
    }

    public class Questionnaire
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Label { get; set; }

        public List<input> inputs { get; set; }

        public List<select> selects { get; set; }

        public List<question> questions { get; set; }

        public List<number> numbers { get; set; }
    }

    public class number
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public double Max { get; set; }

        public double Min { get; set; }

        public double Step { get; set; }

        public string Name { get; set; }
    }

    public class input
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public string Name { get; set; }
    }

    public class select
    {
        public string Text { get; set; }

        public string Name { get; set; }
        
        public List<option> options { get; set; }
    }

    public class option
    {
        public string Text { get; set; }

        [Key]
        public string Value { get; set; }
    }

    public class question
    {
        public string Text { get; set; }

        public List<radio> radios { get; set; }

        public string SelectChange { get; set; }

        public string Name { get; set; }

        public string Show { get; set; }
    }

    public class radio
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public bool IsChecked { get; set; }
    }
}
