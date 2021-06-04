using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FuelReports.Models
{

    public class FuelXml
    {
        [XmlAttribute("type")]
        public string FuelType { get; set; }  
        [XmlElement("price")]
        public string Price { get; set; }
    }
}
