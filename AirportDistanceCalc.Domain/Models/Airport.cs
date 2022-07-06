using AirportDistanceCalc.Domain.Models.Interfaces;

namespace AirportDistanceCalc.Domain.Models
{
    public class Airport : BaseModel
    {
        public string Country { get; set; }
        public string CityIata { get; set; }
        public string Iata { get; set; }
        public string City { get; set; }
        public string TimeZoneRegionName { get; set; }
        public string CountryIata { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Hubs { get; set; }
        public Location Location { get; set; }
        public DateTime CreatedIn { get; } = DateTime.Now;
    }
}
