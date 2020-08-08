using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.Module.Trip.Repositories.Entities
{
    public class TripListEntity : BaseEntity
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}
