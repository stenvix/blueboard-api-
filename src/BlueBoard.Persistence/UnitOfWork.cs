using System;
using System.Data;
using BlueBoard.Common;
using BlueBoard.Common.Enums;
using BlueBoard.Persistence.Abstractions;
using Dawn;

namespace BlueBoard.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed;
        private readonly IDbTransaction transaction;

        public UnitOfWork(IDbConnection connection)
        {
            Guard.Argument(connection, nameof(connection)).NotNull();

            this.Connection = connection;
            this.transaction = connection.BeginTransaction();
        }

        public IDbConnection Connection { get; }

        public void Commit()
        {
            try
            {
                if (this.transaction != null && this.transaction.Connection.State == ConnectionState.Open)
                {
                    this.transaction.Commit();
                }
            }
            catch (Exception exception)
            {
                this.transaction?.Rollback();

                throw new BlueBoardException(ResponseCode.DatabaseError, "Database error", exception);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                this.transaction?.Dispose();
                this.disposed = true;
            }
        }
    }
}
