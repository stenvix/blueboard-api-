using System.Data;
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

        public bool IsUserExists(IDbConnection connection, string email)
        {
            var userExists = connection.QueryFirst<bool>("user_exists_v1", new {email = email},
                commandType: CommandType.StoredProcedure);
            return userExists;
        }

        public UserEntity CreateUser(IDbConnection connection, string email)
        {
            // var p = new DynamicParameters();
            // p.Add("email_in", email, DbType.StringFixedLength, ParameterDirection.Input, size:256);
            // p.Add("status_in", (byte)UserStatus.Initialized, DbType.Byte, ParameterDirection.Input);
            var parameters = new
            {
                email_in = new DbString {Value = email, IsFixedLength = true, Length = 256, IsAnsi = true},
                status_in = (byte)UserStatus.Initialized
            };
            var user = connection.QueryFirst<UserEntity>("create_user_v1", parameters,
                commandType: CommandType.StoredProcedure);

            return user;
        }
    }
}
