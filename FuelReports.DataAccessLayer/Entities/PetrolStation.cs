using System;
using System.Collections.Generic;
using System.Text;

namespace FuelReports.DataAccessLayer
{
    public class PetrolStation
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
