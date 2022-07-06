using AirportDistanceCalc.Domain.Data;
using AirportDistanceCalc.Domain.Models;
using AutoMapper;

namespace AirportDistanceCalc.Api.Config.AutoMapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AirportVO,Airport>().ReverseMap();
                config.CreateMap<LocationVO,Location>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
