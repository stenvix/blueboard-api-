using System;
using System.Data;
using System.Threading.Tasks;
using BlueBoard.Module.Trip.Repositories.Entities;

namespace BlueBoard.Module.Trip.Repositories
{
    internal class TripListRepository : ITripListRepository
    {
        public Task<TripListEntity> CreateAsync(IDbConnection connection, TripListEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TripListEntity> GetAsync(IDbConnection connection, long id)
        {
            throw new NotImplementedException();
        }

        public Task<TripListEntity> UpdateAsync(IDbConnection connection, TripListEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
