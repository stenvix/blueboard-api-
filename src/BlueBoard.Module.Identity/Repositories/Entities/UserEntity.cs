using BlueBoard.Common.Enums;
using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.Module.Identity.Repositories.Entities
{
    internal class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public UserStatus Status { get; set; }
    }
}
