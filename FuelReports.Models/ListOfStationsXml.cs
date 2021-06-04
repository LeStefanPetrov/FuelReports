using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FuelReports.Models
{

    [XmlRoot("petrolStations")]
    public class ListOfStationsXml
    {
        [XmlAttribute("date")]
        public DateTime Date { get; set; }

        [XmlElement("petrolStation")]
        public List<PetrolStationXml> ListOfPetrolStations { get; set; }

    }
}
