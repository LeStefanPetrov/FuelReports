using FuelReports.Contracts;
using FuelReports.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuelReports.Services
{
    public class FuelTypeService
    {
        public static List<FuelTypeDto> MapFuelTypes(IEnumerable<string> fuelTypes)
        {
            var fuelTypesDto = new List<FuelTypeDto>();

            foreach (var fuelType in fuelTypes)
            {
                var fuelTypeDto= new FuelTypeDto();

                fuelTypeDto.ID = Guid.NewGuid();
                fuelTypeDto.FuelType = fuelType;
                fuelTypesDto.Add(fuelTypeDto);
            }
            return fuelTypesDto;
        }
    }
}
