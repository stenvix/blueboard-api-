using BlueBoard.API.Contracts.Base;
using BlueBoard.Common.Enums;

namespace BlueBoard.API.Contracts.Profile.Base
{
    public abstract class ProfileResponse : ApiResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public UserStatus Status { get; set; }
    }
}
