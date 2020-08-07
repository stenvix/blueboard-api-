using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BlueBoard.Module.Common;
using BlueBoard.Module.Common.Helpers;
using BlueBoard.Module.Trip.Repositories.Entities;
using Dapper;

namespace BlueBoard.Module.Trip.Repositories
{
    internal class TripRepository : ITripRepository
    {
        public async Task<TripEntity> GetAsync(IDbConnection dbConnection, long id)
        {
            var parameters = new {id_in = id};

            var trip = await dbConnection.QuerySingleAsync<TripEntity>("find_trip_by_id_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return trip;
        }

        public async Task<TripEntity> CreateTripAsync(IDbConnection dbConnection, TripEntity entity)
        {
            var parameters = new
            {
                user_id_in = entity.CreatedBy,
                name_in = DbFieldHelper.GetDbString(entity.Name, Constraints.NameLength),
                description_in = DbFieldHelper.GetDbString(entity.Description, Constraints.DescriptionLength),
                start_date_in = entity.StartDate,
                end_date_in = entity.EndDate,
                status_in = (byte)entity.Status,
            };

            var trip = await dbConnection.QuerySingleAsync<TripEntity>("create_trip_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return trip;
        }

        public async Task<TripEntity> UpdateTripAsync(IDbConnection dbConnection, TripEntity entity)
        {
            var parameters = new
            {
                id_in = entity.Id,
                user_id_in = entity.UpdatedBy,
                name_in = DbFieldHelper.GetDbString(entity.Name, Constraints.NameLength),
                description_in = DbFieldHelper.GetDbString(entity.Description, Constraints.DescriptionLength),
                start_date_in = entity.StartDate,
                end_date_in = entity.EndDate
            };

            var trip = await dbConnection.QuerySingleAsync<TripEntity>("update_trip_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return trip;
        }

        public async Task<IList<TripEntity>> GetByUserAsync(IDbConnection dbConnection, long userId)
        {
            var parameters = new {user_id_in = userId};

            var trips = await dbConnection.QueryAsync<TripEntity>("find_trips_by_user_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return trips.ToList();
        }
    }
}
