using System.Transactions;
using BlueBoard.Persistence.Abstractions;

namespace BlueBoard.Persistence
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConnectionFactory connectionFactory;

        public UnitOfWorkFactory(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IUnitOfWork Create(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var connection = this.connectionFactory.Create();
            return new UnitOfWork(connection);
        }
    }
}
