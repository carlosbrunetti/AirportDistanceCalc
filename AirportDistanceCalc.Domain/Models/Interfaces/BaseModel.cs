namespace AirportDistanceCalc.Domain.Models.Interfaces
{
    public class BaseModel
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
