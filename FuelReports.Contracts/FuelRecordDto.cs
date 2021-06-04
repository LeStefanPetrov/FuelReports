using System;
using System.Collections.Generic;
using System.Text;

namespace FuelReports.Contracts
{
    public class FuelRecordDto
    {
        public Guid ID { get; set; }
        public Guid PetrolStationID { get; set; }
        public Guid FuelTypeID { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
    }
}
