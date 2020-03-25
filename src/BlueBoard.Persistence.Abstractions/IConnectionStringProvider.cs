namespace BlueBoard.Persistence.Abstractions
{
    public interface IConnectionStringProvider
    {
        string GetMasterConnectionString();
    }
}