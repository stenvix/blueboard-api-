using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BlueBoard.Common.Enums;
using BlueBoard.Module.Common;
using BlueBoard.Module.Common.Helpers;
using BlueBoard.Module.Identity.Repositories.Entities;
using Dapper;

namespace BlueBoard.Module.Identity.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public UserRepository()
        {
        }

        public async Task<bool> IsUserExistsAsync(IDbConnection connection, string email)
        {
            var parameters = new
            {
                email_in = DbFieldHelper.GetDbString(email, 256)
            };

            var userExists = await connection.QueryFirstAsync<bool>("user_exists_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return userExists;
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync(IDbConnection connection, long[] usersIds)
        {
            var parameters = new
            {
                users_ids_in = usersIds
            };

            var users = await connection.QueryAsync<UserEntity>("find_users_by_ids_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return users;
        }

        public async Task<UserEntity> CreateUserAsync(IDbConnection connection, string email, long? createdBy = null)
        {
            var parameters = new
            {
                email_in = DbFieldHelper.GetDbString(email, 256),
                status_in = (byte)UserStatus.Initialized,
                created_by_in = createdBy
            };

            var user = await connection.QueryFirstAsync<UserEntity>("create_user_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return user;
        }

        public async Task<UserEntity> FindByEmailAsync(IDbConnection connection, string email)
        {
            var parameters = new
            {
                email_in = DbFieldHelper.GetDbString(email, 256)
            };

            var user = await connection.QueryFirstAsync<UserEntity>("find_user_by_email_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return user;
        }

        public async Task<UserEntity> FindById(IDbConnection connection, long id)
        {
            var parameters = new {id_in = id};

            var user = await connection.QueryFirstAsync<UserEntity>("find_user_by_id_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return user;
        }

        public async Task<UserEntity> Update(IDbConnection connection, UserEntity entity, long updatedBy)
        {
            var parameters = new
            {
                id_in = entity.Id,
                first_name_in = DbFieldHelper.GetDbString(entity.FirstName, 128),
                last_name_in = DbFieldHelper.GetDbString(entity.LastName, 128),
                username_in = DbFieldHelper.GetDbString(entity.Username, 128),
                email_in = DbFieldHelper.GetDbString(entity.Email, 256),
                phone_in = DbFieldHelper.GetDbString(entity.Phone, 16),
                updated_by_in = updatedBy
            };

            var user = await connection.QuerySingleAsync<UserEntity>("update_user_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return user;
        }

        public async Task<IEnumerable<UserEntity>> SearchByQueryAsync(IDbConnection connection, string query)
        {
            var parameters = new
            {
                query_in = DbFieldHelper.GetDbString(query, Constraints.NameLength)
            };

            var users = await connection.QueryAsync<UserEntity>("find_user_by_query_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return users;
        }
    }
}
