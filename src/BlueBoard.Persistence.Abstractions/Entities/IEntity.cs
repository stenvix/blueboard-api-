using System;

namespace BlueBoard.Persistence.Abstractions.Entities
{
    public interface IEntity
    {
        long Id { get; set; }
        DateTime Created { get; set; }
        DateTime? Updated { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }
}
