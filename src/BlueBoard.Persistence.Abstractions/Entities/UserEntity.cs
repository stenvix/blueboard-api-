using BlueBoard.Common.Enums;

namespace BlueBoard.Persistence.Abstractions.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public UserStatus Status { get; set; }
    }
}
