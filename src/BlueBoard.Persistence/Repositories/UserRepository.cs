using System.Data;
using System.Threading.Tasks;
using BlueBoard.Persistence.Abstractions.Entities;
using BlueBoard.Persistence.Abstractions.Enums;
using BlueBoard.Persistence.Abstractions.Repositories;
using BlueBoard.Persistence.Helpers;
using Dapper;

namespace BlueBoard.Persistence.Repositories
{
    public class UserRepository : IUserRepository
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

        public async Task<UserEntity> CreateUserAsync(IDbConnection connection, string email)
        {
            var parameters = new
            {
                email_in = DbFieldHelper.GetDbString(email, 256),
                status_in = (byte)UserStatus.Initialized
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

        public async Task<UserEntity> FindById(IDbConnection connection, int id)
        {
            var parameters = new {id_in = (long)id};

            var user = await connection.QueryFirstAsync<UserEntity>("find_user_by_id_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return user;
        }

        public async Task<UserEntity> Update(IDbConnection connection, UserEntity entity)
        {
            var parameters = new
            {
                id_in = entity.Id,
                first_name_in = DbFieldHelper.GetDbString(entity.FirstName, 128),
                last_name_in = DbFieldHelper.GetDbString(entity.LastName, 128),
                username_in = DbFieldHelper.GetDbString(entity.Username, 128),
                email_in = DbFieldHelper.GetDbString(entity.Email, 256),
                phone_in = DbFieldHelper.GetDbString(entity.Phone, 16)
            };

            var user = await connection.QueryFirstAsync<UserEntity>("update_user_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return user;
        }
    }
}
