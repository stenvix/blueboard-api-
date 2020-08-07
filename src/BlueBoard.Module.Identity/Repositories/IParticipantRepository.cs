using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BlueBoard.Module.Identity.Repositories.Entities;

namespace BlueBoard.Module.Identity.Repositories
{
    internal interface IParticipantRepository
    {
        Task<bool> IsExists(IDbConnection connection, long tripId, long userId);
        Task<IEnumerable<ParticipantEntity>> GetByTripAsync(IDbConnection connection, long tripId);
        Task<ParticipantEntity> GetAsync(IDbConnection connection, long tripId, long userId);
        Task<ParticipantEntity> CreateAsync(IDbConnection connection, ParticipantEntity entity);
        Task RemoveAsync(IDbConnection connection, long id);
    }
}
