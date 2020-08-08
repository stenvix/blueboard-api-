using System.Data;
using System.Threading.Tasks;
using BlueBoard.Module.Trip.Repositories.Entities;

namespace BlueBoard.Module.Trip.Repositories
{
    public interface ITripListRepository
    {
        Task<TripListEntity> CreateAsync(IDbConnection connection, TripListEntity entity);
    }
}
