using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    [Serializable]
    [XmlRoot("request")]
    public class SMSRequest
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
}
