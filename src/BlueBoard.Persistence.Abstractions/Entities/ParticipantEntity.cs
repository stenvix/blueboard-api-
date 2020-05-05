namespace BlueBoard.Persistence.Abstractions.Entities
{
    public class ParticipantEntity : BaseEntity
    {
        public long UserId { get; set; }
        public long TripId { get; set; }
    }
}
