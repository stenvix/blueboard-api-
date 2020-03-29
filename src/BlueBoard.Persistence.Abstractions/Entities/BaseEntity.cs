using System;

namespace BlueBoard.Persistence.Abstractions.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
