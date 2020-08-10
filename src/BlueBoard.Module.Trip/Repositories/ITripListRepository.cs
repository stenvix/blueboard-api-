using System.Data;
using System.Threading.Tasks;
using BlueBoard.Module.Trip.Repositories.Entities;

namespace BlueBoard.Module.Trip.Repositories
{
    internal interface ITripListRepository
    {
        Task<TripListEntity> CreateAsync(IDbConnection connection, TripListEntity entity);
        Task<TripListEntity> GetAsync(IDbConnection connection, long id);
        Task<TripListEntity> UpdateAsync(IDbConnection connection, TripListEntity entity);
    }
}
