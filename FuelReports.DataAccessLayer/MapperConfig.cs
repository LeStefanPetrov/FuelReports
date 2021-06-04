using AutoMapper;
using FuelReports.Contracts;
using FuelReports.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuelReports.Models
{
    public class MapperConfig : Profile
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<FuelRecordDto,FuelRecord>();
                cfg.CreateMap<FuelTypeDto, FuelType>();
                cfg.CreateMap<PetrolStationDto,PetrolStation>();
            });
        }

    }
}
