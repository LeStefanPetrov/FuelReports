using FuelReports.Contracts;
using FuelReports.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuelReports.Services
{
    public class PetrolStationService
    {
        public static List<PetrolStationDto> MapPetrolStations(IEnumerable<PetrolStationXml> petrolStations)
        {
            var petrolStationsDto = new List<PetrolStationDto>();

            foreach(var petrolStation in petrolStations)
            {
                var petrolStationDto = new PetrolStationDto();

                petrolStationDto.ID = Guid.NewGuid();
                petrolStationDto.Name = petrolStation.Name;
                petrolStationDto.Address = petrolStation.Address;
                petrolStationDto.City = petrolStation.City;
                petrolStationsDto.Add(petrolStationDto);
            }
            return petrolStationsDto;
        }
    }
}
