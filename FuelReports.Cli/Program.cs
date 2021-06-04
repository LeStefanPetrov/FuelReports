using FuelReports.Deserialization;
using FuelReports.SFTP;
using System;
using System.IO;
using System.Xml.Serialization;
using FuelReports.Models;
using System.Reflection;
using System.Configuration;
using FuelReports.DataAccessLayer;
using Commander.NET;
using Commander.NET.Exceptions;
using FuelReports.Services;
namespace FuelReports.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] argss = { "fuel-reports","config","--data-dir","asd"};

                var parser = new CommanderParser<FuelReports>();
                var options = parser.Add(argss).Parse();

                if (options!=null) 
                    Console.WriteLine(options.fuelReports.config.DataDirPath);
            }
            catch (ParameterMissingException ex)
            {
                Console.WriteLine("Missing parameter: " + ex.ParameterName);
            }
           // string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
           // SftpDownloader.SftpDownload(path + AppConfig.GetAppConfigValue(AppConfigKeys.XmlProjectPath));
           // DatabaseUpload.InsertToDatabase(FuelRecordService.MapFuelRecords(Deserializer<ListOfStationsXml>.Deserialize(path + AppConfig.GetAppConfigValue(AppConfigKeys.XmlProjectPath)))); 

        }
    }
}
