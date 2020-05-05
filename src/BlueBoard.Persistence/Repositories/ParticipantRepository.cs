using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BlueBoard.Persistence.Abstractions.Entities;
using BlueBoard.Persistence.Abstractions.Repositories;
using Dapper;

namespace BlueBoard.Persistence.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        public async Task<bool> IsExists(IDbConnection connection, long tripId, long userId)
        {
            var parameters = new
            {
                trip_id_in = tripId,
                user_id_in = userId
            };

            var exists = await connection.QuerySingleAsync<bool>("participant_exists_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return exists;
        }

        public async Task<IEnumerable<ParticipantEntity>> GetByTripAsync(IDbConnection connection, long tripId)
        {
            var parameters = new
            {
                trip_id_in = tripId
            };

            var participants = await connection.QueryAsync<ParticipantEntity>("find_participant_by_trip_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return participants;
        }

        public async Task<ParticipantEntity> GetAsync(IDbConnection connection, long tripId, long userId)
        {
            var parameters = new
            {
                trip_id_in = tripId,
                user_id_in = userId
            };

            var participant = await connection.QueryFirstOrDefaultAsync<ParticipantEntity>("find_participant_by_trip_and_user_v1", parameters, commandType: CommandType.StoredProcedure);

            return participant;
        }

        public async Task<ParticipantEntity> CreateAsync(IDbConnection connection, ParticipantEntity entity)
        {
            var parameters = new
            {
                user_id_in = entity.UserId,
                trip_id_in = entity.TripId,
                created_by_in = entity.CreatedBy
            };

            var participant = await connection.QuerySingleAsync<ParticipantEntity>("create_participant_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return participant;
        }

        public async Task RemoveAsync(IDbConnection connection, long id)
        {
            var parameters = new
            {
                id_in = id
            };

            await connection.ExecuteAsync("remove_participant_v1", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
