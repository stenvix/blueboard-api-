using System;
using System.Data;
using BlueBoard.Common;
using BlueBoard.Common.Enums;
using BlueBoard.Persistence.Abstractions;
using Dawn;
using Npgsql;

namespace BlueBoard.Persistence.Postgres
{
    public class PostgresConnectionFactory : IConnectionFactory
    {
        private readonly IConnectionStringProvider connectionStringProvider;

        public PostgresConnectionFactory(IConnectionStringProvider connectionStringProvider)
        {
            Guard.Argument(connectionStringProvider, nameof(connectionStringProvider)).NotNull();

            this.connectionStringProvider = connectionStringProvider;
        }

        public IDbConnection Create()
        {
            return this.CreateDbConnection(this.connectionStringProvider.GetMasterConnectionString());
        }

        private IDbConnection CreateDbConnection(string connectionString)
        {
            Guard.Argument(connectionString, nameof(connectionString)).NotNull().NotEmpty();
            NpgsqlConnection connection = null;
            try
            {
                var fullConnectionString = this.GetValidConnectionString(connectionString);
                connection = new NpgsqlConnection(fullConnectionString);
                connection.Open();
                return connection;
            }
            catch (Exception exception)
            {
                connection?.Close();
                Console.WriteLine(exception);
                throw new BlueBoardException(ResponseCode.DatabaseError, "Can't create database connection", exception);
            }
        }


        private string GetValidConnectionString(string connectionString)
        {
            try
            {
                var builder = new NpgsqlConnectionStringBuilder(connectionString);

                return builder.ConnectionString;
            }
            catch (Exception exception)
            {
                throw new BlueBoardException(ResponseCode.ConfigurationError, innerException: exception);
            }
        }
    }
}
