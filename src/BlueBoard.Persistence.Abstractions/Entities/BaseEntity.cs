using System;

namespace BlueBoard.Persistence.Abstractions.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }
}
