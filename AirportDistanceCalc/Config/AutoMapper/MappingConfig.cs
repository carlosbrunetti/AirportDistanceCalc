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
                config.CreateMap<Airport, AirportVO>()
                    .ForMember(
                        dest => dest.iata,
                        opt => opt.MapFrom(src => src.Iata))
                    .ForMember(
                        dest => dest.country,
                        opt => opt.MapFrom(src => src.Country))
                    .ForMember(
                        dest => dest.country_iata,
                        opt => opt.MapFrom(src => src.CountryIata))
                    .ForMember(
                        dest => dest.city_iata,
                        opt => opt.MapFrom(src => src.CityIata))
                    .ForMember(
                        dest => dest.city,
                        opt => opt.MapFrom(src => src.City))
                    .ForMember(
                        dest => dest.hubs,
                        opt => opt.MapFrom(src => src.Hubs))
                    .ForMember(
                        dest => dest.location,
                        opt => opt.MapFrom(src => src.Location))
                    .ForMember(
                        dest => dest.name,
                        opt => opt.MapFrom(src => src.Name))
                    .ForMember(
                        dest => dest.rating,
                        opt => opt.MapFrom(src => src.Rating))
                    .ForMember(
                        dest => dest.timezone_region_name,
                        opt => opt.MapFrom(src => src.TimeZoneRegionName))
                    .ForMember(
                        dest => dest.type,
                        opt => opt.MapFrom(src => src.Type))
                    .ForMember(
                        dest => dest.AirportDestination,
                        opt => opt.MapFrom(src => src.AirportDestination))
                    .ReverseMap();

                config.CreateMap<Location, LocationVO>()
                    .ForMember(
                        dest => dest.lat,
                        opt => opt.MapFrom(src => src.Latitude))
                    .ForMember(
                        dest => dest.lon,
                        opt => opt.MapFrom(src => src.Longitude))
                    .ReverseMap();
            });

            return mappingConfig;
        }
    }
}
