using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Entities;
using BlueBoard.Persistence.Abstractions.Repositories;
using BlueBoard.Persistence.Helpers;
using Dapper;

namespace BlueBoard.Persistence.Repositories
{
    public class TripRepository : ITripRepository
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
                email_in = DbFieldHelper.GetDbString(entity.CreatedBy, Constraints.EmailLength),
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
                email_in = DbFieldHelper.GetDbString(entity.UpdatedBy, Constraints.EmailLength),
                name_in = DbFieldHelper.GetDbString(entity.Name, Constraints.NameLength),
                description_in = DbFieldHelper.GetDbString(entity.Description, Constraints.DescriptionLength),
                start_date_in = entity.StartDate,
                end_date_in = entity.EndDate
            };

            var trip = await dbConnection.QuerySingleAsync<TripEntity>("update_trip_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return trip;
        }

        public async Task<IEnumerable<TripEntity>> GetTripsByUserAsync(IDbConnection dbConnection, string email)
        {
            var parameters = new {email_in = DbFieldHelper.GetDbString(email, Constraints.EmailLength)};

            var trips = await dbConnection.QueryAsync<TripEntity>("find_trips_by_user_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return trips;
        }
    }
}
