using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.Persistence.Abstractions.Repositories
{
    public interface ITripRepository
    {
        Task<TripEntity> GetAsync(IDbConnection dbConnection, long id);

        Task<TripEntity> CreateTripAsync(IDbConnection dbConnection, TripEntity entity);

        Task<TripEntity> UpdateTripAsync(IDbConnection dbConnection, TripEntity entity);

        Task<IEnumerable<TripEntity>> GetTripsByUserAsync(IDbConnection dbConnection, string email);
    }
}
