using BlueBoard.Persistence.Abstractions;
using Dawn;
using Microsoft.Extensions.Configuration;

namespace BlueBoard.Persistence
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string masterConnectionName;
        private readonly IConfiguration configuration;

        public ConnectionStringProvider(string masterConnectionName, IConfiguration configuration)
        {
            Guard.Argument(masterConnectionName).NotNull().NotEmpty();
            Guard.Argument(configuration).NotNull();

            this.masterConnectionName = masterConnectionName;
            this.configuration = configuration;
        }

        public string GetMasterConnectionString()
        {
            return this.configuration.GetConnectionString(this.masterConnectionName);
        }
    }
}
