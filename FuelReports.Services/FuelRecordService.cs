using FuelReports.Cli;
using FuelReports.Contracts;
using FuelReports.DataAccessLayer;
using FuelReports.DataAccessLayer.Repositories;
using FuelReports.Models;
using FuelReports.SFTP;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FuelReports.Services
{
    public class FuelRecordService
    {
        private readonly FuelTypeRepository fuelType;
        private readonly FuelRecordRepository fuelRecord;
        private readonly PetrolStationRepository petrolStation;
        public FuelRecordService()
        {
            fuelType = new FuelTypeRepository();
            fuelRecord = new FuelRecordRepository();
            petrolStation = new PetrolStationRepository();
        }
        public static List<FuelRecordDto> MapFuelRecords (List<ListOfStationsXml> petrolStations)
        {
            var fuelTypes = petrolStations.SelectMany(x => x.ListOfPetrolStations.SelectMany(ft => ft.Fuels.Select(f => f.FuelType))).Distinct();
            DatabaseUpload.InsertFuelType(FuelTypeService.MapFuelTypes(fuelTypes));
            DatabaseUpload.InsertPetrolStation(PetrolStationService.MapPetrolStations(petrolStations[0].ListOfPetrolStations));


            var petrolStations2 = petrolStations.SelectMany(ps => ps.ListOfPetrolStations);
            Console.WriteLine("count of petrolStations: " + petrolStations2.Count());
            PetrolStationService.MapPetrolStations(petrolStations[0].ListOfPetrolStations);

            var fuelRecordsDto = new List<FuelRecordDto>();
            
            foreach (var listOfStations in petrolStations)
            {
                foreach(var petrolStation in listOfStations.ListOfPetrolStations)
                {
                    Guid petrolStationId = GetPetrolStationID(petrolStation.Name,petrolStation.Address,petrolStation.City);

                    foreach (var fuel in petrolStation.Fuels) {
                        var fuelRecordDto = new FuelRecordDto();

                        fuelRecordDto.ID = Guid.NewGuid();
                        fuelRecordDto.Date = listOfStations.Date;
                        fuelRecordDto.Price = float.Parse(fuel.Price, NumberStyles.Currency);
                        fuelRecordDto.FuelTypeID = GetFuelTypeID(fuel.FuelType);
                        fuelRecordDto.PetrolStationID = petrolStationId;
                        fuelRecordsDto.Add(fuelRecordDto);
                    }
                }
            }
            return fuelRecordsDto;
        }

        public static Guid GetPetrolStationID(string name, string address, string city)
        {
            Guid petrolStationID = default;
            using var connection = new SqlConnection(AppConfig.GetAppConfigValue(AppConfigKeys.ConnectionString));
            var command = connection.CreateCommand();
            connection.Open();
            command.CommandText = $"SELECT ID FROM PetrolStations WHERE Name='{name}' AND Address='{address}' AND City='{city}';";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    petrolStationID = (Guid)reader[0];
            }
            return petrolStationID;
        }

        public static Guid GetFuelTypeID(string fuelType)
        {
            Guid fuelTypeID = default;
            using var connection = new SqlConnection(AppConfig.GetAppConfigValue(AppConfigKeys.ConnectionString));
            var command = connection.CreateCommand();
            connection.Open();
            command.CommandText = $"SELECT ID FROM FuelTypes WHERE FuelType='{fuelType}';";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    fuelTypeID = (Guid)reader[0];
            }
            return fuelTypeID;
        }
    }
}
