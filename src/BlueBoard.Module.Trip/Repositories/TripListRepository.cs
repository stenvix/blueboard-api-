using System;
using System.Data;
using System.Threading.Tasks;
using BlueBoard.Module.Trip.Repositories.Entities;

namespace BlueBoard.Module.Trip.Repositories
{
    public class TripListRepository : ITripListRepository
    {
        public Task<TripListEntity> CreateAsync(IDbConnection connection, TripListEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
