using BlueBoard.Persistence.Abstractions.Enums;

namespace BlueBoard.Contract.Identity.Models
{
    public class ProfileModel: SlimProfileModel
    {
        public UserStatus Status { get; set; }
    }
}
