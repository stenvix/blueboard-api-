using Dapper;

namespace BlueBoard.Persistence.Helpers
{
    public static class DbFieldHelper
    {
        public static DbString GetDbString(string value, int length)
        {
            return new DbString{ Value = value, Length = length, IsFixedLength = true};
        }
    }
}
