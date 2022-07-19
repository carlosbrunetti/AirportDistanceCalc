using AirportDistanceCalc.Domain.Enum;

namespace AirportDistanceCalc.Domain.Models.Request
{
    public class AirportCalcRequest
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public UnitOfMeasureEnum UnitOfMeasure { get; set; }
    }
}
