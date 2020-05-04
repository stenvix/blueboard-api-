using System;

namespace BlueBoard.Persistence.Abstractions.Entities
{
    public interface IEntity
    {
        long Id { get; set; }
        DateTime Created { get; set; }
        DateTime? Updated { get; set; }
        long CreatedBy { get; set; }
        long UpdatedBy { get; set; }
    }
}
