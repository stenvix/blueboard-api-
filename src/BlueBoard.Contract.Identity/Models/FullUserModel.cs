using BlueBoard.Common.Enums;

namespace BlueBoard.Contract.Identity.Models
{
    public class FullUserModel : UserModel
    {
        public UserStatus Status { get; set; }
    }
}
