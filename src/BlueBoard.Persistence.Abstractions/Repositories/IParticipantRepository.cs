using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.Persistence.Abstractions.Repositories
{
    public interface IParticipantRepository
    {
        Task<bool> IsExists(IDbConnection connection, long tripId, long userId);
        Task<IEnumerable<ParticipantEntity>> GetByTripAsync(IDbConnection connection, long tripId);
        Task<ParticipantEntity> GetAsync(IDbConnection connection, long tripId, long userId);
        Task<ParticipantEntity> CreateAsync(IDbConnection connection, ParticipantEntity entity);
        Task RemoveAsync(IDbConnection connection, long id);
    }
}
