using System.Data;

namespace BlueBoard.Persistence.Abstractions
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}