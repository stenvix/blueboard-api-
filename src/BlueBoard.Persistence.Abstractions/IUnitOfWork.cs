using System;
using System.Data;
using System.Threading.Tasks;

namespace BlueBoard.Persistence.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        
        void Commit();
    }
}