namespace AirportDistanceCalc.Domain.Models.Reports
{
    public class CityReport
    {
        public string OriginCity { get; set; }
        public string OriginCountry { get; set; }
        public string OriginAirportName { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationAirportName { get; set; }
        public int Count { get; set; }
    }
}
