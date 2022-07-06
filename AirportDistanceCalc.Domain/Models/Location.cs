using AirportDistanceCalc.Domain.Models.Interfaces;

namespace AirportDistanceCalc.Domain.Models
{
    public class Location : BaseModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
