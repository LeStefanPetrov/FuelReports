using System;
using AutoMapper;
using FuelReports.Models;
using FuelReports.Contracts;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using FuelReports.SFTP;
using FuelReports.Cli;
using System.Linq;

namespace FuelReports.DataAccessLayer
{
    public class DatabaseUpload
    {
        public static void InsertToDatabase(List<FuelRecordDto> petrolStations)
        {
            SqlTransaction transaction;
            var listOfDailyRecords = Mapper.Map<List<FuelRecord>>(petrolStations);
            using var connection = new SqlConnection(AppConfig.GetAppConfigValue(AppConfigKeys.ConnectionString));
            var command = connection.CreateCommand();
            connection.Open();
            transaction = connection.BeginTransaction("XmlDataTransaction");
            command.Transaction = transaction;

            try
            {
                foreach(var dailyRecord in listOfDailyRecords)
                {
                    InsertFuelRecord(dailyRecord, command);
                }
                transaction.Commit();
                Console.WriteLine("Data insertion complete!");

            } catch(Exception ex)
            {
                Console.WriteLine("Data Commit Exception Thrown : " + ex.Message);
                transaction.Rollback();
            }
        }

        public static void InsertFuelRecord(FuelRecord dailyRecord,SqlCommand command)
        {
            command.CommandText = $"INSERT INTO FuelRecords (PetrolStationID,FuelTypeID,Price,Date) VALUES (@PetrolStationID,@FuelTypeID,@Price,@Date);";
            command.Parameters.AddWithValue("@Price", dailyRecord.Price);
            command.Parameters.AddWithValue("@Date", dailyRecord.Date);
            command.Parameters.AddWithValue("@PetrolStationID", dailyRecord.PetrolStationID);
            command.Parameters.AddWithValue("@FuelTypeID", dailyRecord.FuelTypeID);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        public static void InsertFuelType(List<FuelTypeDto> fuelTypes)
        {
            MapperConfig.InitializeMapper();
            var fuelTypeEntities= Mapper.Map<List<FuelType>>(fuelTypes);
            using var connection = new SqlConnection(AppConfig.GetAppConfigValue(AppConfigKeys.ConnectionString));
            var command = connection.CreateCommand();
            connection.Open();

            foreach (var fuelTypeEntity in fuelTypeEntities) {
                command.CommandText = $"INSERT INTO FuelTypes (FuelType) SELECT (@FuelType) WHERE NOT EXISTS(SELECT FuelType FROM FuelTypes WHERE FuelType=@FuelType);";
                command.Parameters.AddWithValue("@FuelType", fuelTypeEntity.FuelType);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }
        
        public static void InsertPetrolStation(List<PetrolStationDto> petrolStations)
        {
            var petrolStationEntities= Mapper.Map<List<PetrolStation>>(petrolStations);
            using var connection = new SqlConnection(AppConfig.GetAppConfigValue(AppConfigKeys.ConnectionString));
            var command = connection.CreateCommand();
            connection.Open();

            foreach (var petrolStationEntity in petrolStationEntities) {
                command.CommandText = $"INSERT INTO PetrolStations (Name,Address,City) SELECT @Name,@Address,@City WHERE NOT EXISTS(SELECT * FROM PetrolStations" +
                          $" WHERE Address ='{petrolStationEntity.Address}' AND City = '{ petrolStationEntity.City}' AND Name = '{petrolStationEntity.Name }');";
                command.Parameters.AddWithValue("@Name", petrolStationEntity.Name);
                command.Parameters.AddWithValue("@Address", petrolStationEntity.Address);
                command.Parameters.AddWithValue("@City", petrolStationEntity.City);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }
    }
}
