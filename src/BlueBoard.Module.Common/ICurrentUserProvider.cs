namespace BlueBoard.Module.Common
{
    public interface ICurrentUserProvider
    {
        int UserId { get; }
        string Email { get; }
    }
}
