using System.Data;
using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.Persistence.Abstractions.Repositories
{
    public interface IUserRepository
    {
        bool IsUserExists(IDbConnection connection, string email);
        UserEntity CreateUser(IDbConnection connection, string email);
    }
}
