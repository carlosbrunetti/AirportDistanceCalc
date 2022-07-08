using AirportDistanceCalc.Domain.Models.Interfaces;

namespace AirportDistanceCalc.Domain.Models
{
    public class Location //: BaseModel
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Airport> Aiports { get; set; }
    }
}
