using Commander.NET.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelReports.Cli
{
    class FuelReports
    {
        [Command("fuel-reports")]
        public Options fuelReports;
    }
    class Options
    {

        [Command("config")]
        public Config config;

        [Command("report")]
        public Report report;
    }
    class Config
    {
        [Parameter("--data-dir", Required = Required.Yes)]
        public string DataDirPath;
    }
    class Report
    {
        [Parameter("--period", Required = Required.Yes)]
        public DateTime period;

        [Parameter("--fuel-type", Required = Required.No)]
        public string fuelType;

        [Parameter("--petrol-station", Required = Required.No)]
        public string petrolStation;

        [Parameter("--city", Required = Required.No)]
        public string city;
    }
}
