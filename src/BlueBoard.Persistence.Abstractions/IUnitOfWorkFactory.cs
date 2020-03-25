using System.Transactions;

namespace BlueBoard.Persistence.Abstractions
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
