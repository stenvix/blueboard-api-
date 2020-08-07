using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BlueBoard.Module.Trip.Repositories.Entities;

namespace BlueBoard.Module.Trip.Repositories
{
    internal interface ITripRepository
    {
        Task<TripEntity> GetAsync(IDbConnection dbConnection, long id);

        Task<TripEntity> CreateTripAsync(IDbConnection dbConnection, TripEntity entity);

        Task<TripEntity> UpdateTripAsync(IDbConnection dbConnection, TripEntity entity);

        Task<IList<TripEntity>> GetByUserAsync(IDbConnection dbConnection, long userId);
    }
}
