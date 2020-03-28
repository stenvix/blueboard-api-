using System.Data;
using System.Threading.Tasks;
using BlueBoard.Persistence.Abstractions.Entities;
using BlueBoard.Persistence.Abstractions.Enums;
using BlueBoard.Persistence.Abstractions.Repositories;
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
                email_in = new DbString {Value = email, IsFixedLength = true, Length = 256, IsAnsi = true}
            };

            var userExists = await connection.QueryFirstAsync<bool>("user_exists_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return userExists;
        }

        public async Task<UserEntity> CreateUserAsync(IDbConnection connection, string email)
        {
            // var p = new DynamicParameters();
            // p.Add("email_in", email, DbType.StringFixedLength, ParameterDirection.Input, size:256);
            // p.Add("status_in", (byte)UserStatus.Initialized, DbType.Byte, ParameterDirection.Input);
            var parameters = new
            {
                email_in = new DbString {Value = email, IsFixedLength = true, Length = 256, IsAnsi = true},
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
                email_in = new DbString {Value = email, IsFixedLength = true, Length = 256, IsAnsi = true},
            };

            var user = await connection.QueryFirstAsync<UserEntity>("find_user_by_email_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return user;
        }
    }
}
