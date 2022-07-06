namespace AirportDistanceCalc.Domain.Models.Response
{
    public class Response
    {
        public int StatusCode { get; set; }
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}
