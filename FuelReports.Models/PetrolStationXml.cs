using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FuelReports.Models
{

    public class PetrolStationXml
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("address")]
        public string Address { get; set; }
        [XmlAttribute("city")]
        public string City { get; set; }
        [XmlArrayItem("fuel")]
        [XmlArray("fuels")]
        public List<FuelXml> Fuels { get; set; }

    }   
}
