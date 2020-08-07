using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.Module.Identity.Repositories.Entities
{
    internal class ParticipantEntity : BaseEntity
    {
        public long UserId { get; set; }
        public long TripId { get; set; }
    }
}
