namespace BlueBoard.Module.Common
{
    public interface ICurrentUserProvider
    {
        long UserId { get; }

        string Email { get; }
    }
}
